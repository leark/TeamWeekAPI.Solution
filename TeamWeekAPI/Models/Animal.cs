using System.ComponentModel.DataAnnotations;

namespace TeamWeekAPI.Models
{
  public class Animal
  {
    public int AnimalId { get; set; }
    [Required]
    public string Image { get; set; }
    [Required]
    [StringLength(20)]
    public string Name { get; set; }
    [Required]
    [Range(1, 20)]
    public int HP { get; set; }
    [Required]
    [Range(1, 9)]
    public int Attack { get; set; }

    public Animal() { }
    public Animal(int id, string image, string name, int hp, int attack)
    {
      AnimalId = id;
      Image = image;
      Name = name;
      HP = hp;
      Attack = attack;
    }
    public static Animal Clone(Animal animal)
    {
      return new Animal(animal.AnimalId, animal.Image, animal.Name, animal.HP, animal.Attack);
    }
  }
}