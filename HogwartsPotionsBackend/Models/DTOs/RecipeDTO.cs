using HogwartsPotionsBackend.Models.Entities;
using System.Collections.Generic;

namespace HogwartsPotionsBackend.Models.DTOs
{
    public class RecipeDTO
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public long? StudentID { get; set; }

        public IReadOnlyList<IngredientDTO> Ingredients { get; set; }
    }
}
