using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/potions")]
    public class PotionController : ControllerBase
    {
        private readonly IPotionService _service;

        public PotionController(IPotionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Potion>> GetAllPotions()
        {
            return await _service.GetAllPotions();
        }

        [HttpPost]
        public async Task<Potion> AddPotion([FromBody] Recipe potion)
        {
            return await _service.AddPotion(potion);
        }


        [HttpGet("/potions/student/{studentId}")]
        public async Task<List<Potion>> GetAllPotionsByStudent(long studentId)
        {
            return await _service.GetAllPotionsByStudent(studentId);
        }

        [HttpPost("/potions/brew")]
        public async Task<Potion> AddBrewingPotion([FromBody] Recipe potion)
        {
            return await _service.AddBrewingPotion(potion);
        }

        [HttpPut("/potions/{potionId}/add")]
        public async Task<Potion> AddToPotion(long potionId, [FromBody] Ingredient ingredient)
        {
            return await _service.AddToPotion(potionId, ingredient);
        }

        [HttpGet("/potions/{potionId}")]
        public async Task<Potion> GetPotionById(long potionId)
        {
            return await _service.GetPotionById(potionId);
        }

        [HttpGet("/potions/{potionId}/help")]
        public async Task<List<Recipe>> GetPotionHelpById(long potionId)
        {
            return await _service.GetPotionHelpById(potionId);
        }
    }
}
