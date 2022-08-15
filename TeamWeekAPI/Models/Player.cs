using System.ComponentModel.DataAnnotations;

namespace TeamWeekAPI.Models
{
  public class Player
  {
    public int PlayerId { get; set; }
    [Required]
    public string Name { get; set; }
  }
}