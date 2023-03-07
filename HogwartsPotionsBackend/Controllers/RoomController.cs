using HogwartsPotionsBackend.Models.Entities;
using HogwartsPotionsBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotionsBackend.Controllers;

[ApiController, Route("/room")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _service;

    public RoomController(IRoomService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<Room>> GetAllRooms()
    {
        return await _service.GetAllRooms();
    }

    [HttpPost]
    public async Task AddRoom([FromBody] Room room)
    {
        await _service.AddRoom(room);
    }

    [HttpGet("/room/{id}")]
    public async Task<Room> GetRoomById(long id)
    {
        return await _service.GetRoom(id);
    }

    [HttpPut("/room/{id}")]
    public async Task UpdateRoomByIdAsync(long id, [FromBody] Room updatedRoom)
    {
        await _service.UpdateRoom(id, updatedRoom);
    }

    [HttpDelete("/room/{id}")]
    public async Task DeleteRoomById(long id)
    {
        await _service.DeleteRoom(id);
    }

    [HttpGet("/room/rat-owners")]
    public async Task<List<Room>> GetRoomsForRatOwners()
    {
        return await _service.GetRoomsForRatOwners();
    }
}
