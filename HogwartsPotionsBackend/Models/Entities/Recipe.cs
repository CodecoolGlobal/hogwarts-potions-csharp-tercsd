using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotionsBackend.Models.Entities;

public class Recipe
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }

    public string Name { get; set; }

    public long? StudentID { get; set; }
    public Student Maker { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; }
}
