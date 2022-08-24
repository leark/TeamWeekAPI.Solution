using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TeamWeekAPI.Models;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System;
namespace TeamWeekAPI.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [Route("api/[controller]")]
  [ApiController]
  public class TeamsController : ControllerBase
  {
    private readonly TeamWeekAPIContext _db;

    public TeamsController(TeamWeekAPIContext db)
    {
      _db = db;
    }

    // GET api/teams
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> Get(string name, int wins, int losses)
    {
      var query = _db.Teams.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
        query = query.Where(entry => entry.Wins == wins);
        query = query.Where(entry => entry.Losses == losses);
      }

      return await query.ToListAsync();
    }

    // POST api/teams
    [HttpPost]
    public async Task<ActionResult<Team>> Post(Team team)
    {
      team.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      // team.UserId = User.Identity.GetUserId();
      _db.Teams.Add(team);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
    }
    // GET: api/Teams/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Team>> GetTeam(int id)
    {
      var team = await _db.Teams.FindAsync(id);

      if (team == null)
      {
        return NotFound();
      }

      return team;
    }

    // GET: api/Teams/TeamAnimals/5
    [HttpGet("TeamAnimals/{id}")]
    public async Task<ActionResult<IEnumerable<Animal>>> GetTeamAnimals(int id)
    {
      List<Animal> animals = await _db.Animals.FromSqlRaw($"SELECT a.AnimalId, a.Image, a.Name, a.HP, a.Attack FROM animals a JOIN animalteams ateams ON ateams.AnimalId = a.AnimalId AND ateams.TeamId = {id};").ToListAsync();
      if (animals == null)
      {
        return NotFound();
      }
      return Ok(animals);
    }

    // PUT: api/Teams/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Team team)
    {
      if (id != team.TeamId)
      {
        return BadRequest();
      }

      _db.Entry(team).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TeamExists(id))
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

    // DELETE: api/Teams/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(int id)
    {
      var team = await _db.Teams.FindAsync(id);
      if (team == null)
      {
        return NotFound();
      }

      _db.Teams.Remove(team);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    // POST: api/Teams/5/3
    [HttpPost("{teamId}/{animalId}")]
    public async Task<IActionResult> Post(int teamId, int animalId)
    {
      if (TeamExists(teamId) && AnimalExists(animalId))
      {
        AnimalTeam at = new AnimalTeam();
        at.TeamId = teamId;
        at.AnimalId = animalId;
        _db.AnimalTeams.Add(at);
        await _db.SaveChangesAsync();
        return Ok(await findAnimalTeam(teamId, animalId));
      }
      else
      {
        return NotFound();
      }
    }

    // PUT: api/Teams/3/2/4
    [HttpPut("{teamId}/{oldAnimalId}/{newAnimalId}")]
    public async Task<IActionResult> Put(int teamId, int oldAnimalId, int newAnimalId)
    {
      AnimalTeam at = await findAnimalTeam(teamId, oldAnimalId);
      if (at == null)
      {
        return BadRequest();
      }
      else
      {
        at.AnimalId = newAnimalId;
      }

      _db.Entry(at).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!_db.AnimalTeams.Any(e => e.AnimalTeamId == at.AnimalTeamId))
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

    // GET: api/Teams/battle/3
    [HttpGet("battle/{id}")]
    public IActionResult BattleTeam(int id)
    {
      if (TeamExists(id))
      {
        var rand = new Random();
        List<Team> teams = _db.Teams.ToList();
        Team team = teams.FirstOrDefault(t => t.TeamId == id);
        int numTeams = teams.Count;
        int randTeam = rand.Next(numTeams);
        Team enemyTeam = teams[randTeam];
        if (numTeams > 1)
        {
          while (enemyTeam.TeamId == id)
          {
            randTeam = rand.Next(numTeams);
            enemyTeam = teams[randTeam];
          }
          // team.Name;
          // enemyTeam.Name;
          Object json = Battle(team, enemyTeam);
          // return winning team & losing team comp
          return Ok(json);
        }
        return NotFound();
      }
      else
      {
        return NotFound();
      }
    }

    private bool TeamExists(int id)
    {
      return _db.Teams.Any(e => e.TeamId == id);
    }

    private bool AnimalExists(int id)
    {
      return _db.Animals.Any(e => e.AnimalId == id);
    }

    private async Task<AnimalTeam> findAnimalTeam(int teamId, int animalId)
    {
      return await _db.AnimalTeams.FirstOrDefaultAsync(a => a.TeamId == teamId && a.AnimalId == animalId);
    }

    private Object Battle(Team team, Team enemyTeam)
    {
      List<Animal> animals = _db.Animals.FromSqlRaw($"SELECT a.AnimalId, a.Image, a.Name, a.HP, a.Attack FROM animals a JOIN animalteams ateams ON ateams.AnimalId = a.AnimalId AND ateams.TeamId = {team.TeamId};").ToList();
      Stack<Animal> t1 = new Stack<Animal>(animals);

      List<Animal> enemyAnimals = _db.Animals.FromSqlRaw($"SELECT a.AnimalId, a.Image, a.Name, a.HP, a.Attack FROM animals a JOIN animalteams ateams ON ateams.AnimalId = a.AnimalId AND ateams.TeamId = {enemyTeam.TeamId};").ToList();
      Stack<Animal> t2 = new Stack<Animal>(enemyAnimals);

      Animal t1a = new Animal();
      Animal t2a = new Animal();
      while (t1.TryPeek(out t1a) && t2.TryPeek(out t2a))
      {
        t1a.HP -= t2a.Attack;
        t2a.HP -= t1a.Attack;

        if (t1a.HP <= 0)
        {
          t1.Pop();
        }
        if (t2a.HP <= 0)
        {
          t2.Pop();
        }
      }

      int result = 0;
      if (t1a != null && t2a == null)
      {
        result = 1;
      }
      else if (t1a == null && t2a != null)
      {
        result = 2;
      }

      Object json = new
      {
        outcome = result,
        team1 = new
        {
          id = team.TeamId,
          name = team.Name,
          animals = t1
        },
        team2 = new
        {
          id = enemyTeam.TeamId,
          name = enemyTeam.Name,
          animals = t2
        }
      };

      return json;
    }
  }
}
