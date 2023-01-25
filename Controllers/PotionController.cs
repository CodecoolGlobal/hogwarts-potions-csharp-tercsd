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


        [HttpGet("/potions/{studentId}")]
        public async Task<List<Potion>> GetAllPotionsByStudent(long studentId)
        {
            return await _context.GetAllPotionsByStudent(studentId);
        }
    }
}
