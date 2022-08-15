using System.ComponentModel.DataAnnotations;

namespace TeamWeekAPI.Models
{
  public class Team
  {
    public int TeamId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Wins { get; set; }
    [Required]
    public int Losses { get; set; }
    [Required]
    public int PlayerId { get; set; }
    public virtual Player Player { get; set; }
  }
}