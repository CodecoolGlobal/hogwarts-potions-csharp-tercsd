using HogwartsPotionsBackend.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotionsBackend.Services;

public interface IRoomService
{
    Task<List<Room>> GetAllRooms();
    Task<Room?> GetRoom(long roomId);
    Task<Room?> AddRoom(Room room);
    Task<Room?> UpdateRoom(long id, Room room);
    Task<bool> DeleteRoom(long id);
    Task<List<Room>> GetAvailableRooms();
    Task<List<Room>> GetRoomsForRatOwners();
}