using Microsoft.EntityFrameworkCore;

namespace TeamWeekAPI.Models
{
  public class TeamWeekAPIContext : DbContext
  {
    public TeamWeekAPIContext(DbContextOptions<TeamWeekAPIContext> options)
        : base(options)
    {
    }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<AnimalTeam> AnimalTeams { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Animal>()
          .HasData(
              new Animal { AnimalId = 1, Name = "George", HP = 3, Attack = 5 },
              new Animal { AnimalId = 2, Name = "Scott", HP = 6, Attack = 3 },
              new Animal { AnimalId = 3, Name = "Michael", HP = 4, Attack = 3 },
              new Animal { AnimalId = 4, Name = "Scooter", HP = 8, Attack = 2 },
              new Animal { AnimalId = 5, Name = "Roger", HP = 13, Attack = 1 }
          );
      builder.Entity<Player>()
      .HasData(
          new Player { PlayerId = 1, Name = "Myrtle" },
          new Player { PlayerId = 2, Name = "Darrel" },
          new Player { PlayerId = 3, Name = "Rita" },
          new Player { PlayerId = 4, Name = "Salvador" },
          new Player { PlayerId = 5, Name = "Virgil" }
      );

      builder.Entity<Team>()
      .HasData(
          new Team { TeamId = 1, Name = "Militant Commandos", Wins = 0, Losses = 0, PlayerId = 1 },
          new Team { TeamId = 2, Name = "Flash Rockets", Wins = 0, Losses = 0, PlayerId = 2 },
          new Team { TeamId = 3, Name = "Silent Mutants", Wins = 0, Losses = 0, PlayerId = 3 },
          new Team { TeamId = 4, Name = "Nunchuk Killers", Wins = 0, Losses = 0, PlayerId = 4 },
          new Team { TeamId = 5, Name = "Alpha Blasters", Wins = 0, Losses = 0, PlayerId = 5 }
      );

      builder.Entity<AnimalTeam>()
        .HasData(
            new AnimalTeam { AnimalTeamId = 1, AnimalId = 1, TeamId = 1 },
            new AnimalTeam { AnimalTeamId = 2, AnimalId = 2, TeamId = 2 },
            new AnimalTeam { AnimalTeamId = 3, AnimalId = 3, TeamId = 3 },
            new AnimalTeam { AnimalTeamId = 4, AnimalId = 4, TeamId = 4 },
            new AnimalTeam { AnimalTeamId = 5, AnimalId = 5, TeamId = 5 }
        );
    }
  }
}