using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HogwartsPotions.Services;

public class RoomService : IRoomService
{
    private readonly HogwartsContext _context;

    public RoomService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task AddRoom(Room room)
    {
        try
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return;
        }
        catch (DbUpdateException ex)
        {
            throw ex;
        }
    }

    public async Task<Room> GetRoom(long roomId)
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

    public async Task UpdateRoom(long id, Room room)
    {
        try
        {
            var roomToUpdate = await _context.Rooms.SingleOrDefaultAsync(r => r.ID == id);
            roomToUpdate.Capacity = room.Capacity;
            await _context.SaveChangesAsync();
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
            var roomToDelete = await _context.Rooms.SingleOrDefaultAsync(r => r.ID == id);
            _context.Rooms.Remove(roomToDelete);
            await _context.SaveChangesAsync();

            return;
        }
        catch (DbUpdateException ex)
        {
            throw ex;
        }
    }

    public async Task<List<Room>> GetRoomsForRatOwners()
    {
        return await _context.Rooms
            .Include(r => r.Residents)
            .ThenInclude(s => s.Room)
            .AsNoTracking()
            .Where(r => !r.Residents.Any(s => s.PetType == PetType.Cat || s.PetType == PetType.Owl))
            .ToListAsync();
    }
}
