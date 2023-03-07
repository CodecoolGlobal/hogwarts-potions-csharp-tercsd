using AutoMapper;
using HogwartsPotionsBackend.Models.DTOs;
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
    private readonly IMapper _mapper;

    public PotionController(IPotionService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PotionDTO>>> GetAllPotions()
    {
        var potions = await _service.GetAllPotions();
        var potionDTOs = _mapper.Map<IEnumerable<PotionDTO>>(potions);
        return Ok(potionDTOs);
    }

    [HttpPost]
    public async Task<ActionResult<PotionDTO>> AddPotion([FromBody] NewRecipeDTO newRecipeDTO)
    {
        var recipe = _mapper.Map<Recipe>(newRecipeDTO);
        var newPotion = await _service.AddPotion(recipe);
        if (newPotion == null) return BadRequest();
        var potionDTO = _mapper.Map<PotionDTO>(newPotion);
        return Ok(potionDTO);
    }


    [HttpGet("/potions/student/{studentId}")]
    public async Task<ActionResult<List<PotionDTO>>> GetAllPotionsByStudent(long studentId)
    {
        var potions = await _service.GetAllPotionsByStudent(studentId);
        var potionDTOs = _mapper.Map<IEnumerable<PotionDTO>>(potions);
        return Ok(potionDTOs);
    }

    [HttpPost("/potions/brew")]
    public async Task<ActionResult<PotionDTO>> AddBrewingPotion([FromBody] RecipeDTO recipeDTO)
    {
        var recipe = _mapper.Map<Recipe>(recipeDTO);
        var potion = await _service.AddBrewingPotion(recipe);
        if (potion == null) return BadRequest();
        var potionDTO = _mapper.Map<PotionDTO>(potion);
        return Ok(potionDTO);
    }

    [HttpPut("/potions/{potionId}/add")]
    public async Task<ActionResult<PotionDTO>> AddToPotion(long potionId, [FromBody] IngredientDTO ingredientDTO)
    {
        var ingredient = _mapper.Map<Ingredient>(ingredientDTO);
        var potion = await _service.AddToPotion(potionId, ingredient);
        if (potion == null) return BadRequest();
        var potionDTO = _mapper.Map<PotionDTO>(potion);
        return Ok(potionDTO);
    }

    [HttpGet("/potions/{potionId}")]
    public async Task<ActionResult<PotionDTO>> GetPotionById(long potionId)
    {
        var potion = await _service.GetPotionById(potionId);
        if (potion == null) return NotFound();
        var potionDTO = _mapper.Map<PotionDTO>(potion);
        return Ok(potionDTO);
    }

    [HttpGet("/potions/{potionId}/help")]
    public async Task<ActionResult<List<RecipeDTO>>> GetPotionHelpById(long potionId)
    {
        var recipe = await _service.GetPotionHelpById(potionId);
        var recipeDTO = _mapper.Map<RecipeDTO>(recipe);
        return Ok(recipeDTO);
    }
}
