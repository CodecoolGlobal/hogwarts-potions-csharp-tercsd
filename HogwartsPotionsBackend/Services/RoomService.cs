using HogwartsPotionsBackend.Models;
using HogwartsPotionsBackend.Models.Entities;
using HogwartsPotionsBackend.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsPotionsBackend.Services;

public class RoomService : IRoomService
{
    private readonly HogwartsContext _context;

    public RoomService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task<Room?> AddRoom(Room room)
    {
        try
        {
            _context.Rooms.Add(room);
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0 ? room : null;
        }
        catch (DbUpdateException ex)
        {
            throw ex;
        }
    }

    public async Task<Room?> GetRoom(long roomId)
    {
        return await _context.Rooms
            .Include(r => r.Residents)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ID == roomId);
    }

    public async Task<List<Room>> GetAllRooms()
    {
        return await _context.Rooms
            .Include(r => r.Residents)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Room?> UpdateRoom(long id, Room room)
    {
        try
        {
            var roomToUpdate = await _context.Rooms.SingleOrDefaultAsync(r => r.ID == id);
            if (roomToUpdate == null) return null;
            roomToUpdate.Capacity = room.Capacity;
            await _context.SaveChangesAsync();
            return roomToUpdate;
        }
        catch (DbUpdateException ex)
        {
            throw ex;
        }
    }

    public async Task<bool> DeleteRoom(long id)
    {
        try
        {
            var roomToDelete = await _context.Rooms.SingleOrDefaultAsync(r => r.ID == id);
            if (roomToDelete == null)
            {
                return false;
            }
            _context.Rooms.Remove(roomToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (DbUpdateException ex)
        {
            throw ex;
        }
    }

    public async Task<List<Room>> GetAvailableRooms()
    {
        return await _context.Rooms
            .Include(r => r.Residents)
            .AsNoTracking()
            .Where(r => !r.Residents.Any())
            .ToListAsync();
    }

    public async Task<List<Room>> GetRoomsForRatOwners()
    {
        return await _context.Rooms
            .Include(r => r.Residents)
            .AsNoTracking()
            .Where(r => !r.Residents.Any(s => s.PetType == PetType.Cat || s.PetType == PetType.Owl))
            .ToListAsync();
    }
}
