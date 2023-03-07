using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsPotions.Services;

public class PotionService : IPotionService
{
    private readonly HogwartsContext _context;

    public PotionService(HogwartsContext context)
    {
        _context = context;
    }
    public async Task<List<Potion>> GetAllPotions()
    {
        return await _context.Potions
            .Include(p => p.Maker)
            .Include(p => p.Ingredients)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Potion>> GetAllPotionsByStudent(long studentId)
    {
        return await _context.Potions
            .Include(p => p.Maker)
            .Include(p => p.Ingredients)
            .AsNoTracking()
            .Where(p => p.Maker.ID == studentId)
            .ToListAsync();
    }

    public async Task<Potion> AddPotion(Recipe potion)
    {
        if (potion.Ingredients.Any())
        {
            var newPotion = new Potion()
            {
                Name = potion.Name,
                Maker = await _context.Students.Where(s => s.ID == potion.Maker.ID).FirstAsync(),
                Ingredients = new HashSet<Ingredient>(),
            };

            foreach (var ingredient in potion.Ingredients)
            {
                if (!_context.Ingredients.Any(i => i.Name == ingredient.Name))
                {
                    await _context.Ingredients.AddAsync(ingredient);
                    newPotion.Ingredients.Add(ingredient);
                }
                else
                {
                    var existingIngredient = await _context.Ingredients.Where(i => i.Name == ingredient.Name).FirstAsync();
                    newPotion.Ingredients.Add(existingIngredient);
                }
            }
            var foundRecipes = (await _context.Recipes
                .Include(r => r.Ingredients)
                .ToListAsync())
                .Where(r => r.Ingredients.ToHashSet()
                .SetEquals(newPotion.Ingredients.ToHashSet()))
                .ToList();
            if (foundRecipes.Count > 0)
            {
                newPotion.BrewingStatus = BrewingStatus.Replica;
                string replicaPotionName = $"{foundRecipes.First().Name} replica";
                newPotion.Name = replicaPotionName;
            }
            else
            {
                int discoveryCount = 1;
                while ((await _context.Recipes.ToListAsync()).Any(r => r.Name == $"{newPotion.Maker.Name}'s discovery #{discoveryCount}"))
                {
                    discoveryCount++;
                }
                string newPotionName = $"{newPotion.Maker.Name}'s discovery #{discoveryCount}";
                newPotion.BrewingStatus = BrewingStatus.Discovery;
                newPotion.Name = newPotionName;

                var discoveredRecipe = new Recipe()
                {
                    Maker = newPotion.Maker,
                    Name = newPotionName,
                    Ingredients = newPotion.Ingredients.ToHashSet(),
                };
                await _context.Recipes.AddAsync(discoveredRecipe);
            }

            await _context.Potions.AddAsync(newPotion);
            await _context.SaveChangesAsync();
            return newPotion;
        }
        return null;
    }

    public async Task<Potion> AddBrewingPotion(Recipe potion)
    {
        if (potion.Ingredients.Any())
        {
            var newPotion = new Potion()
            {
                Name = potion.Name,
                Maker = await _context.Students.Where(s => s.ID == potion.Maker.ID).FirstAsync(),
                Ingredients = new HashSet<Ingredient>(),
            };

            foreach (var ingredient in potion.Ingredients)
            {
                if (!_context.Ingredients.Any(i => i.Name == ingredient.Name))
                {
                    await _context.Ingredients.AddAsync(ingredient);
                    newPotion.Ingredients.Add(ingredient);
                }
                else
                {
                    var existingIngredient = await _context.Ingredients.Where(i => i.Name == ingredient.Name).FirstAsync();
                    newPotion.Ingredients.Add(existingIngredient);
                }
            }
            if (newPotion.Ingredients.Count < _context.MaxIngredientsForPotions)
            {
                newPotion.BrewingStatus = BrewingStatus.Brew;
            }
            else
            {
                var foundRecipes = (await _context.Recipes.Include(r => r.Ingredients).ToListAsync())
                    .Where(r => r.Ingredients.ToHashSet()
                    .SetEquals(newPotion.Ingredients.ToHashSet()))
                    .ToList();
                if (foundRecipes.Count > 0)
                {
                    newPotion.BrewingStatus = BrewingStatus.Replica;
                    string replicaPotionName = $"{foundRecipes.First().Name} replica";
                    newPotion.Name = replicaPotionName;
                }
                else
                {
                    int discoveryCount = 1;
                    while ((await _context.Recipes.ToListAsync()).Any(r => r.Name == $"{newPotion.Maker.Name}'s discovery #{discoveryCount}"))
                    {
                        discoveryCount++;
                    }
                    string newPotionName = $"{newPotion.Maker.Name}'s discovery #{discoveryCount}";
                    newPotion.BrewingStatus = BrewingStatus.Discovery;
                    newPotion.Name = newPotionName;

                    var discoveredRecipe = new Recipe()
                    {
                        Maker = newPotion.Maker,
                        Name = newPotionName,
                        Ingredients = newPotion.Ingredients.ToHashSet(),
                    };
                    await _context.Recipes.AddAsync(discoveredRecipe);
                }
            }

            await _context.Potions.AddAsync(newPotion);
            await _context.SaveChangesAsync();
            return newPotion;
        }
        return null;
    }

    public async Task<Potion> AddToPotion(long potionId, Ingredient ingredient)
    {
        var potion = await _context.Potions
            .Include(r => r.Ingredients)
            .Include(r => r.Maker)
            .Where(p => p.ID == potionId)
            .FirstOrDefaultAsync();

        if (!_context.Ingredients.Any(i => i.Name == ingredient.Name))
        {
            await _context.Ingredients.AddAsync(ingredient);
            potion.Ingredients.Add(ingredient);
        }
        else
        {
            var existingIngredient = await _context.Ingredients.Where(i => i.Name == ingredient.Name).FirstAsync();
            potion.Ingredients.Add(existingIngredient);
        }

        if (potion.Ingredients.Count < 5)
        {
            potion.BrewingStatus = BrewingStatus.Brew;

        }
        else
        {
            var foundRecipes = (await _context.Recipes
                .Include(r => r.Ingredients)
                .ToListAsync()
                )
                .Where(r => r.Ingredients.ToHashSet().SetEquals(potion.Ingredients.ToHashSet()))
                .ToList();
            if (foundRecipes.Count > 0)
            {
                potion.BrewingStatus = BrewingStatus.Replica;
                string replicaPotionName = $"{foundRecipes.First().Name} replica";
                potion.Name = replicaPotionName;
            }
            else
            {
                int discoveryCount = 1;
                while ((await _context.Recipes.ToListAsync()).Any(r => r.Name == $"{potion.Maker.Name}'s discovery #{discoveryCount}"))
                {
                    discoveryCount++;
                }
                string newPotionName = $"{potion.Maker.Name}'s discovery #{discoveryCount}";
                potion.BrewingStatus = BrewingStatus.Discovery;
                potion.Name = newPotionName;

                var discoveredRecipe = new Recipe()
                {
                    Maker = potion.Maker,
                    Name = newPotionName,
                    Ingredients = potion.Ingredients.ToHashSet(),
                };
                await _context.Recipes.AddAsync(discoveredRecipe);
            }
        }
        await _context.SaveChangesAsync();
        return potion;
    }

    public async Task<Potion> GetPotionById(long potionId)
    {
        return await _context.Potions
            .Include(p => p.Maker)
            .Include(p => p.Ingredients)
            .AsNoTracking()
            .Where(p => p.ID == potionId)
            .SingleAsync();
    }

    public async Task<List<Recipe>> GetPotionHelpById(long potionId)
    {
        var potion = await _context.Potions
            .Include(r => r.Ingredients)
            .Where(p => p.ID == potionId)
            .FirstOrDefaultAsync();
        var recipes = await _context.Recipes
            .Include(r => r.Ingredients)
            .ToListAsync();
        var matchingRecipes = recipes
            .Where(r => potion.Ingredients.ToHashSet().IsSubsetOf(r.Ingredients.ToHashSet()))
            .ToList();
        return await _context.Recipes
            .AsNoTracking()
            .Where(r => matchingRecipes.Contains(r))
            .ToListAsync();
    }
}
