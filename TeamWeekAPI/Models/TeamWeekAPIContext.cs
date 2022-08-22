using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace TeamWeekAPI.Models
{
  public class TeamWeekAPIContext : IdentityDbContext
  {
    public TeamWeekAPIContext(DbContextOptions<TeamWeekAPIContext> options)
        : base(options)
    {
    }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<AnimalTeam> AnimalTeams { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
			builder.Entity<Animal>()
          .HasData(
              new Animal { AnimalId = 1, Image = "https://cdn.discordapp.com/attachments/1008839085172981781/1008883732104626246/musclepikachu.png", Name = "Pikachad", HP = 3, Attack = 5 },
              new Animal { AnimalId = 2, Image = "https://cdn.discordapp.com/attachments/1008839085172981781/1008930285691351131/MonionNoBgZoom.png", Name = "Monion", HP = 6, Attack = 3 },
              new Animal { AnimalId = 3, Image = "https://cdn.discordapp.com/attachments/1008839085172981781/1009168139780624395/chickenyoshi.png", Name = "Noshi", HP = 4, Attack = 3 },
              new Animal { AnimalId = 4, Image = "https://cdn.discordapp.com/attachments/1008839085172981781/1009185539242606743/beerbellybear.png", Name = "Scooter", HP = 8, Attack = 2 },
              new Animal { AnimalId = 5, Image = "https://cdn.discordapp.com/attachments/1008839085172981781/1009201332357439619/peterpigeon.png", Name = "Pigeon Pete", HP = 13, Attack = 1 },
							new Animal { AnimalId = 6, Image = "https://cdn.discordapp.com/attachments/927592064949026866/1008876043827937350/unknown.png", Name = "Cheeso Dude", HP = 2, Attack = 4 },
							new Animal { AnimalId = 7, Image = "https://cdn.discordapp.com/attachments/927592064949026866/1008873198881869885/73371860-F5C2-4494-AC67-B3EA6111A5D6.jpg", Name = "Cat With Sword", HP = 4, Attack = 5 },
							new Animal { AnimalId = 8, Image = "https://cdn.discordapp.com/attachments/927592064949026866/1009231673625428058/unknown.png", Name = "Pepper Jackson", HP = 3, Attack = 3 },
							new Animal { AnimalId = 9, Image = "https://cdn.discordapp.com/attachments/1008839085172981781/1008892391442350141/monion.png", Name = "Happy Monion", HP = 5, Attack = 1 }
          );
    }
  }
}