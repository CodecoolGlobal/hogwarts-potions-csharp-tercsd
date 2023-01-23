using HogwartsPotions.Models.Entities;
using System.Diagnostics;
using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HogwartsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{Name="Harry Potter", HouseType=HouseType.Gryffindor, PetType=PetType.Owl},
            new Student{Name="Hermione Granger", HouseType=HouseType.Gryffindor, PetType=PetType.Cat},
            new Student{Name="Draco Malfoy", HouseType=HouseType.Slytherin},
            new Student{Name="Neville Longbottom", HouseType=HouseType.Gryffindor},
            new Student{Name="Ron Weasley", HouseType=HouseType.Gryffindor, PetType=PetType.Rat},

            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var rooms = new Room[]
            {
            new Room{Capacity=4},
            new Room{Capacity=4},
            new Room{Capacity=4},
            new Room{Capacity=4},
            };
            foreach (Room r in rooms)
            {
                context.Rooms.Add(r);
            }
            context.SaveChanges();

        }
    }
}
