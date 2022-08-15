using System.ComponentModel.DataAnnotations;

namespace TeamWeekAPI.Models
{
  public class Animal
  {
    public int AnimalId { get; set; }
    [Required]
    [StringLength(20)]
    public string Name { get; set; }
    [Required]
    [Range(1, 20)]
    public int HP { get; set; }
    [Required]
    [Range(1, 9)]
    public int Attack { get; set; }
  }
}