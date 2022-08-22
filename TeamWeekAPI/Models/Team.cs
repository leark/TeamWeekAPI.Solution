using System.ComponentModel.DataAnnotations;

namespace TeamWeekAPI.Models
{
  public class Team
  {
    public int TeamId { get; set; }
    public string UserId { get; set; } = "nope";
    public string Name { get; set; }
    [Required]
    public int Wins { get; set; }
    [Required]
    public int Losses { get; set; }
  }
}