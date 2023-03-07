using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services
{
    public interface IPotionService
    {
        Task<List<Potion>> GetAllPotions();
        Task<List<Potion>> GetAllPotionsByStudent(long studentId);
        Task<Potion> GetPotionById(long potionId);
        Task<List<Recipe>> GetPotionHelpById(long potionId);
        Task<Potion> AddBrewingPotion(Recipe potion);
        Task<Potion> AddPotion(Recipe potion);
        Task<Potion> AddToPotion(long potionId, Ingredient ingredient);
    }
}