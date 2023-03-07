using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRooms();
        Task<Room> GetRoom(long roomId);
        Task AddRoom(Room room);
        Task UpdateRoom(long id, Room room);
        Task DeleteRoom(long id);
        Task<List<Room>> GetRoomsForRatOwners();
    }
}