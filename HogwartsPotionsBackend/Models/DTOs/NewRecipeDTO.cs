using HogwartsPotionsBackend.Models.Enums;
using System.Collections.Generic;

namespace HogwartsPotionsBackend.Models.DTOs
{
    public class NewRecipeDTO
    {
        public string Name { get; set; }

        public long? StudentID { get; set; }

        public IReadOnlyList<NewIngredientDTO> Ingredients { get; set; }
    }
}
