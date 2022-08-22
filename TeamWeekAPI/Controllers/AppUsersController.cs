using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TeamWeekAPI.Models;
using System.Linq;

namespace TeamWeekAPI.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Route("api/[controller]")]
  [ApiController]
  public class AppUsersController : ControllerBase
  {
    private readonly TeamWeekAPIContext _db;

    public AppUsersController(TeamWeekAPIContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> Get(string name)
    {
      var query = _db.AppUsers.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }

      return await query.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<AppUser>> Post(AppUser appUser)
    {
      _db.AppUsers.Add(appUser);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetAppUser), new { id = appUser.AppUserId }, appUser);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetAppUser(int id)
    {
      var appUser = await _db.AppUsers.FindAsync(id);

      if (appUser == null)
      {
        return NotFound();
      }

      return appUser;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, AppUser appUser)
    {
      if (id != appUser.AppUserId)
      {
        return BadRequest();
      }

      _db.Entry(appUser).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AppUserExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // DELETE: api/Animals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppUser(int id)
    {
      var appUser = await _db.AppUsers.FindAsync(id);
      if (appUser == null)
      {
        return NotFound();
      }

      _db.AppUsers.Remove(appUser);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool AppUserExists(int id)
    {
      return _db.AppUsers.Any(p => p.AppUserId == id);
    }
  }
}