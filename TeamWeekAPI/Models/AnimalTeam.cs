using System.ComponentModel.DataAnnotations;

namespace TeamWeekAPI.Models
{
  public class AnimalTeam
  {
    public int AnimalTeamId { get; set; }
    public int AnimalId { get; set; }
    public int TeamId { get; set; }
    public virtual Animal Animal { get; set; }
    public virtual Team Team { get; set; }
  }
}