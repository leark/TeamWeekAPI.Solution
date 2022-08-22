using System.ComponentModel.DataAnnotations;

namespace TeamWeekAPI.Models
{
  public class Team
  {
    public int TeamId { get; set; }
    public int AppUserId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Wins { get; set; }
    [Required]
    public int Losses { get; set; }
  }
}