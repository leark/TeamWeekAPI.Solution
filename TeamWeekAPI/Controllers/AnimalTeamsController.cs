using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TeamWeekAPI.Models;
using TeamWeekAPI.Controllers;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System;

namespace TeamWeekAPI.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Route("api/[controller]")]
  [ApiController]
  public class AnimalTeamsController : ControllerBase
  {
    private readonly TeamWeekAPIContext _db;

    public AnimalTeamsController(TeamWeekAPIContext db)
    {
      _db = db;
    }

    [HttpGet("{teamId}")]
    public async Task<ActionResult<IEnumerable<AnimalTeam>>> GetAnimalTeams(int teamId)
    {
      var query = _db.AnimalTeams.AsQueryable();
      query = query.Where(q => q.TeamId == teamId);

      return await query.ToListAsync();
    }

    // DELETE: api/AnimalTeams/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimalTeam(int id)
    {
      var at = await _db.AnimalTeams.FindAsync(id);
      if (at == null)
      {
        return NotFound();
      }

      _db.AnimalTeams.Remove(at);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}