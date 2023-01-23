using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<Student>().ToTable("Student");
        }

        public async Task AddRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetRoom(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRoom(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomsForRatOwners()
        {
            throw new NotImplementedException();
        }
    }
}
