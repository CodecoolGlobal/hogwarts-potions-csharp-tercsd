using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public const int MaxIngredientsForPotions = 5;

        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Potion> Potions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Recipe>().ToTable("Recipe");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<Potion>().ToTable("Potion");
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            return await Potions.Include(p => p.Maker).Include(p => p.Ingredients)
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<Potion>> GetAllPotionsByStudent(long studentId)
        {
            return await Potions.Include(p => p.Maker).Include(p => p.Ingredients)
                .AsNoTracking().Where(p => p.Maker.ID == studentId).ToListAsync();
        }

        public async Task<Potion> AddPotion(Recipe potion)
        {
            if (potion.Ingredients.Any())
            {
                var newPotion = new Potion()
                {
                    Name = potion.Name,
                    Maker = await Students.Where(s => s.ID == potion.Maker.ID).FirstAsync(),
                    Ingredients = new HashSet<Ingredient>(),
                };

                foreach (var ingredient in potion.Ingredients)
                {
                    if (!Ingredients.Any(i => i.Name == ingredient.Name))
                    {
                        await Ingredients.AddAsync(ingredient);
                        newPotion.Ingredients.Add(ingredient);
                    }
                    else
                    {
                        var existingIngredient = await Ingredients.Where(i => i.Name == ingredient.Name).FirstAsync();
                        newPotion.Ingredients.Add(existingIngredient);
                    }
                }
                var foundRecipes = (await Recipes.Include(r => r.Ingredients).ToListAsync())
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
                    while ((await Recipes.ToListAsync()).Any(r => r.Name == $"{newPotion.Maker.Name}'s discovery #{discoveryCount}"))
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
                    await Recipes.AddAsync(discoveredRecipe);
                }

                await Potions.AddAsync(newPotion);
                await SaveChangesAsync();
                return newPotion;
            }
            return null;
        }

        public async Task AddRoom(Room room)
        {
            try
            {
                Rooms.Add(room);
                await SaveChangesAsync();
                return;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<Room> GetRoom(long roomId)
        {
            return await Rooms.Include(r => r.Residents).AsNoTracking()
                .FirstOrDefaultAsync(r => r.ID == roomId);
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await Rooms.Include(r => r.Residents).AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateRoom(long id, Room room)
        {
            try
            {
                var roomToUpdate = await Rooms.SingleOrDefaultAsync(r => r.ID == id);
                roomToUpdate.Capacity = room.Capacity;
                await SaveChangesAsync();
                return;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task DeleteRoom(long id)
        {
            try
            {
                var roomToDelete = await Rooms.SingleOrDefaultAsync(r => r.ID == id);
                Rooms.Remove(roomToDelete);
                await SaveChangesAsync();

                return;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await Rooms.Include(r => r.Residents).ThenInclude(s => s.Room).AsNoTracking()
                .Where(r => !r.Residents.Any(s => s.PetType == PetType.Cat || s.PetType == PetType.Owl))
                .ToListAsync();
        }


    }
}
