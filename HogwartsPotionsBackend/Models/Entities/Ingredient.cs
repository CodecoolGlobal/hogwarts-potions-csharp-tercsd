using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotionsBackend.Models.Entities;

public class Ingredient
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Potion> Potions { get; set; }
    public virtual ICollection<Recipe> Recipes { get; set; }
}
