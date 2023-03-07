using HogwartsPotionsBackend.Models.Entities;
using HogwartsPotionsBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public async Task<ActionResult<List<Room>>> GetAllRooms()
    {
        return Ok(await _service.GetAllRooms());
    }

    [HttpPost]
    public async Task<ActionResult> AddRoom([FromBody] Room room)
    {
        await _service.AddRoom(room);
        return Ok();

    }

    [HttpGet("/room/{id}")]
    public async Task<ActionResult<Room>> GetRoomById(long id)
    {
        return Ok(await _service.GetRoom(id));
    }

    [HttpPut("/room/{id}")]
    public async Task<ActionResult> UpdateRoomByIdAsync(long id, [FromBody] Room updatedRoom)
    {
        await _service.UpdateRoom(id, updatedRoom);
        return Ok();
    }

    [HttpDelete("/room/{id}")]
    public async Task<ActionResult> DeleteRoomById(long id)
    {
        await _service.DeleteRoom(id);
        return Ok();
    }

    [HttpGet("/room/rat-owners")]
    public async Task<ActionResult<List<Room>>> GetRoomsForRatOwners()
    {
        return Ok(await _service.GetRoomsForRatOwners());
    }
}
