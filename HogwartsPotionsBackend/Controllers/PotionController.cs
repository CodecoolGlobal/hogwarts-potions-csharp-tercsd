using HogwartsPotionsBackend.Models.Entities;
using HogwartsPotionsBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotionsBackend.Controllers;

[ApiController, Route("/potions")]
public class PotionController : ControllerBase
{
    private readonly IPotionService _service;

    public PotionController(IPotionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Potion>>> GetAllPotions()
    {
        return Ok(await _service.GetAllPotions());
    }

    [HttpPost]
    public async Task<ActionResult<Potion>> AddPotion([FromBody] Recipe potion)
    {
        return Ok(await _service.AddPotion(potion));
    }


    [HttpGet("/potions/student/{studentId}")]
    public async Task<ActionResult<List<Potion>>> GetAllPotionsByStudent(long studentId)
    {
        return Ok(await _service.GetAllPotionsByStudent(studentId));
    }

    [HttpPost("/potions/brew")]
    public async Task<ActionResult<Potion>> AddBrewingPotion([FromBody] Recipe potion)
    {
        return Ok(await _service.AddBrewingPotion(potion));
    }

    [HttpPut("/potions/{potionId}/add")]
    public async Task<ActionResult<Potion>> AddToPotion(long potionId, [FromBody] Ingredient ingredient)
    {
        return Ok(await _service.AddToPotion(potionId, ingredient));
    }

    [HttpGet("/potions/{potionId}")]
    public async Task<ActionResult<Potion>> GetPotionById(long potionId)
    {
        return Ok(await _service.GetPotionById(potionId));
    }

    [HttpGet("/potions/{potionId}/help")]
    public async Task<ActionResult<List<Recipe>>> GetPotionHelpById(long potionId)
    {
        return Ok(await _service.GetPotionHelpById(potionId));
    }
}
