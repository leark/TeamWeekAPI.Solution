using TeamWeekAPI.Models.DTO.Responses;
using TeamWeekAPI.Models.DTO.Requests;
using TeamWeekAPI.Models;
using TeamWeekAPI.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace TeamWeekAPI.Controllers
{
  [Route("api/[controller]")] // api/authManagement
  [ApiController]
  [ApiExplorerSettings(IgnoreApi = true)]

  public class AuthManagementController : ControllerBase
  {
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfig _jwtConfig;
    private readonly TeamWeekAPIContext _db;

    public AuthManagementController(
        UserManager<IdentityUser> userManager,
        IOptionsMonitor<JwtConfig> optionsMonitor,
        TokenValidationParameters tokenValidationParameters,
        TeamWeekAPIContext db)
    {
      _userManager = userManager;
      _jwtConfig = optionsMonitor.CurrentValue;
      _tokenValidationParameters = tokenValidationParameters;
      _db = db;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto user)
    {
      if (ModelState.IsValid)
      {
        // We can utilise the model
        var existingUser = await _userManager.FindByEmailAsync(user.Email);

        if (existingUser != null)
        {
          return BadRequest(new RegistrationResponse()
          {
            Errors = new List<string>() {
                                "Email already in use"
                            },
            Success = false
          });
        }

        var newUser = new IdentityUser() { Email = user.Email, UserName = user.Name };
        var isCreated = await _userManager.CreateAsync(newUser, user.Password);
        if (isCreated.Succeeded)
        {
          var jwtToken = await GenerateJwtToken(newUser);

          return Ok(jwtToken);

          // return Ok(new RegistrationResponse()
          // {
          //   Success = true,
          //   Token = jwtToken
          // });
        }
        else
        {
          return BadRequest(new RegistrationResponse()
          {
            Errors = isCreated.Errors.Select(x => x.Description).ToList(),
            Success = false
          });
        }
      }

      return BadRequest(new RegistrationResponse()
      {
        Errors = new List<string>() {
                        "Invalid payload"
                    },
        Success = false
      });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
    {
      if (ModelState.IsValid)
      {
        var existingUser = await _userManager.FindByEmailAsync(user.Email);

        if (existingUser == null)
        {
          return BadRequest(new RegistrationResponse()
          {
            Errors = new List<string>() {
                                "Invalid login request"
                            },
            Success = false
          });
        }

        var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

        if (!isCorrect)
        {
          return BadRequest(new RegistrationResponse()
          {
            Errors = new List<string>() {
                                "Invalid login request"
                            },
            Success = false
          });
        }

        var jwtToken = await GenerateJwtToken(existingUser);

        return Ok(jwtToken);
        // return Ok(new RegistrationResponse()
        // {
        //   Success = true,
        //   Token = jwtToken
        // });
      }

      return BadRequest(new RegistrationResponse()
      {
        Errors = new List<string>() {
                        "Invalid payload"
                    },
        Success = false
      });
    }

    private async Task<AuthResult> GenerateJwtToken(IdentityUser user)
    {
      var jwtTokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
          {
            new Claim("Id", user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
        Expires = DateTime.UtcNow.AddSeconds(300),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = jwtTokenHandler.CreateToken(tokenDescriptor);
      var jwtToken = jwtTokenHandler.WriteToken(token);

      var refreshToken = new RefreshToken()
      {
        JwtId = token.Id,
        IsUsed = false,
        UserId = user.Id,
        AddedDate = DateTime.UtcNow,
        ExpiryDate = DateTime.UtcNow.AddYears(1),
        IsRevoked = false,
        Token = RandomString(25) + Guid.NewGuid()
      };

      await _db.RefreshTokens.AddAsync(refreshToken);
      await _db.SaveChangesAsync();

      return new AuthResult()
      {
        Token = jwtToken,
        Success = true,
        RefreshToken = refreshToken.Token
      };
    }

    [HttpPost]
    [Route("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
    {
      if (ModelState.IsValid)
      {
        var res = await VerifyToken(tokenRequest);

        if (res == null)
        {
          return BadRequest(new RegistrationResponse()
          {
            Errors = new List<string>() {
                    "Invalid tokens"
                },
            Success = false
          });
        }

        return Ok(res);
      }

      return BadRequest(new RegistrationResponse()
      {
        Errors = new List<string>() {
                "Invalid payload"
            },
        Success = false
      });
    }

    internal string RandomString(int length)
    {
      var random = new Random();
      var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      return new string(Enumerable.Repeat(chars, length)
      .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private async Task<AuthResult> VerifyToken(TokenRequest tokenRequest)
    {
      var jwtTokenHandler = new JwtSecurityTokenHandler();

      try
      {
        // This validation function will make sure that the token meets the validation parameters
        // and its an actual jwt token not just a random string
        var principal = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

        // Now we need to check if the token has a valid security algorithm
        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        {
          var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

          if (result == false)
          {
            return null;
          }
        }

        // Will get the time stamp in unix time
        var utcExpiryDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        // we convert the expiry date from seconds to the date
        var expDate = UnixTimeStampToDateTime(utcExpiryDate);

        if (expDate > DateTime.UtcNow)
        {
          return new AuthResult()
          {
            Errors = new List<string>() { "We cannot refresh this since the token has not expired" },
            Success = false
          };
        }

        // Check the token we got if its saved in the db
        var storedRefreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

        if (storedRefreshToken == null)
        {
          return new AuthResult()
          {
            Errors = new List<string>() { "refresh token doesnt exist" },
            Success = false
          };
        }

        // Check the date of the saved token if it has expired
        if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        {
          return new AuthResult()
          {
            Errors = new List<string>() { "token has expired, user needs to relogin" },
            Success = false
          };
        }

        // check if the refresh token has been used
        if (storedRefreshToken.IsUsed)
        {
          return new AuthResult()
          {
            Errors = new List<string>() { "token has been used" },
            Success = false
          };
        }

        // Check if the token is revoked
        if (storedRefreshToken.IsRevoked)
        {
          return new AuthResult()
          {
            Errors = new List<string>() { "token has been revoked" },
            Success = false
          };
        }

        // we are getting here the jwt token id
        var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        // check the id that the recieved token has against the id saved in the db
        if (storedRefreshToken.JwtId != jti)
        {
          return new AuthResult()
          {
            Errors = new List<string>() { "the token doesn't match the saved token" },
            Success = false
          };
        }

        storedRefreshToken.IsUsed = true;
        _db.RefreshTokens.Update(storedRefreshToken);
        await _db.SaveChangesAsync();

        var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
        return await GenerateJwtToken(dbUser);
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
      // Unix timestamp is seconds past epoch
      System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
      dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
      return dtDateTime;
    }
  }
}