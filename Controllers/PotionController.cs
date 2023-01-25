using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/potions")]
    public class PotionController : ControllerBase
    {
        private readonly HogwartsContext _context;

        public PotionController(HogwartsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.GetAllPotions();
        }

        [HttpPost]
        public async Task<Potion> AddPotion([FromBody] Recipe potion)
        {
            return await _context.AddPotion(potion);
        }


        [HttpGet("/potions/student/{studentId}")]
        public async Task<List<Potion>> GetAllPotionsByStudent(long studentId)
        {
            return await _context.GetAllPotionsByStudent(studentId);
        }

        [HttpPost("/potions/brew")]
        public async Task<Potion> AddBrewingPotion([FromBody] Recipe potion)
        {
            return await _context.AddBrewingPotion(potion);
        }

        [HttpPut("/potions/{potionId}/add")]
        public async Task<Potion> AddToPotion(long potionId, [FromBody] Ingredient ingredient)
        {
            return await _context.AddToPotion(potionId, ingredient);
        }

        [HttpGet("/potions/{potionId}")]
        public async Task<Potion> GetPotionById(long potionId)
        {
            return await _context.GetPotionById(potionId);
        }

        [HttpGet("/potions/{potionId}/help")]
        public async Task<List<Recipe>> GetPotionHelpById(long potionId)
        {
            return await _context.GetPotionHelpById(potionId);
        }
    }
}
