using HogwartsPotionsBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotionsBackend.Models;

public class HogwartsContext : DbContext
{
    public int MaxIngredientsForPotions = 5;

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
}
