using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace HogwartsPotions.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HogwartsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var rooms = new Room[]
            {
                new Room{Capacity=4},
                new Room{Capacity=4},
                new Room{Capacity=4},
                new Room{Capacity=4},
            };
            foreach (var r in rooms)
            {
                context.Rooms.Add(r);
            }
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{Name="Harry Potter", HouseType=HouseType.Gryffindor, PetType=PetType.Owl, RoomID=1},
                new Student{Name="Hermione Granger", HouseType=HouseType.Gryffindor, PetType=PetType.Cat, RoomID=2},
                new Student{Name="Draco Malfoy", HouseType=HouseType.Slytherin, RoomID=3},
                new Student{Name="Neville Longbottom", HouseType=HouseType.Gryffindor},
                new Student{Name="Ron Weasley", HouseType=HouseType.Gryffindor, PetType=PetType.Rat, RoomID=1},

            };
            foreach (var s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            Recipe[] recipes = new Recipe[] {
                new Recipe {
                    Name = "Polyjuice Potion",
                    Ingredients = new HashSet<Ingredient> {
                        new Ingredient { Name = "Lacewing Flies" },
                        new Ingredient { Name = "Leeches" },
                        new Ingredient { Name = "Powdered Bicorn Horn" },
                        new Ingredient { Name = "Knotgrass" },
                        new Ingredient { Name = "Fluxweed" },
                    }
                },
                new Recipe {
                    Name = "Amortentia",
                    Ingredients = new HashSet<Ingredient> {
                        new Ingredient { Name = "Rose thorns" },
                        new Ingredient { Name = "Lovage" },
                        new Ingredient { Name = "Mistletoe Berries" },
                        new Ingredient { Name = "Infinite water" },
                        new Ingredient { Name = "Sopophorous Bean" },
                    }
                },
                new Recipe {
                    Name = "Sleeping Draught",
                    Ingredients = new HashSet<Ingredient> {
                        new Ingredient { Name = "Powdered root of asphodel" },
                        new Ingredient { Name = "Powdered horn of a bicorn" },
                        new Ingredient { Name = "Moonstone" },
                        new Ingredient { Name = "Lemon Juice" },
                        new Ingredient { Name = "Dried nettles" },
                    }
                },
                new Recipe {
                    Name = "Veritaserum",
                    Ingredients = new HashSet<Ingredient> {
                        new Ingredient { Name = "Powdered horn of a unicorn" },
                        new Ingredient { Name = "Powdered bezoar" },
                        new Ingredient { Name = "Powdered root of ginseng" },
                        new Ingredient { Name = "Shredded skin of a boomslang" },
                        new Ingredient { Name = "Bicorn horn" },
                    }
                },
                new Recipe {
                    Name = "Felix Felicis",
                    Ingredients = new HashSet<Ingredient> {
                        new Ingredient { Name = "Powdered root of mandrake" },
                        new Ingredient { Name = "Powdered horn of a bicorn" },
                        new Ingredient { Name = "Powdered asphodel root" },
                        new Ingredient { Name = "Lacewing flies" },
                        new Ingredient { Name = "Dragon liver" },
                    }
                }
            };
            foreach (var r in recipes)
            {
                context.Recipes.Add(r);
            }
            context.SaveChanges();

        }
    }
}
