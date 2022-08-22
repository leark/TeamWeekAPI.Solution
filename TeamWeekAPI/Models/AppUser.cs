using System.ComponentModel.DataAnnotations;

namespace TeamWeekAPI.Models
{
  public class AppUser
  {
    public int AppUserId { get; set; }
    [Required]
    public string Name { get; set; }
  }
}