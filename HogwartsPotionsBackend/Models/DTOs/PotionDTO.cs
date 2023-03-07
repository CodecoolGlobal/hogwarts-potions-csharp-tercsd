using HogwartsPotionsBackend.Models.Entities;
using HogwartsPotionsBackend.Models.Enums;
using System.Collections.Generic;

namespace HogwartsPotionsBackend.Models.DTOs
{
    public class PotionDTO
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public BrewingStatus? BrewingStatus { get; set; }

        public long? StudentID { get; set; }

        public IReadOnlyList<IngredientDTO> Ingredients { get; set; }
    }
}
