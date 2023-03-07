using AutoMapper;
using HogwartsPotionsBackend.Models.DTOs;
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
    private readonly IMapper _mapper;

    public RoomController(IRoomService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomDTO>>> GetAllRooms()
    {
        var rooms = await _service.GetAllRooms();
        var roomDTOs = _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        return Ok(roomDTOs);
    }

    [HttpPost]
    public async Task<ActionResult> AddRoom([FromBody] NewRoomDTO newRoomDTO)
    {
        var newRoom = _mapper.Map<Room>(newRoomDTO);
        var result = await _service.AddRoom(newRoom);
        if (result == null) return BadRequest();
        var roomDTO = _mapper.Map<RoomDTO>(result);

        return Ok(roomDTO);

    }

    [HttpGet("/room/{id}")]
    public async Task<ActionResult<RoomDTO>> GetRoomById(long id)
    {
        var room = await _service.GetRoom(id);
        if (room == null) return NotFound();
        var roomDTO = _mapper.Map<RoomDTO>(room);
        return Ok(roomDTO);
    }

    [HttpPut("/room/{id}")]
    public async Task<ActionResult> UpdateRoomById(long id, [FromBody] RoomDTO roomDTO)
    {
        var room = _mapper.Map<Room>(roomDTO);
        var result = await _service.UpdateRoom(id, room);
        if (result == null) return BadRequest();
        var resultDTO = _mapper.Map<RoomDTO>(result);
        return Ok(resultDTO);
    }

    [HttpDelete("/room/{id}")]
    public async Task<ActionResult> DeleteRoomById(long id)
    {
        var result = await _service.DeleteRoom(id);
        return result ? Ok() : BadRequest();
    }

    [HttpGet("/room/available")]
    public async Task<ActionResult<List<RoomDTO>>> GetAvailableRooms()
    {
        var rooms = await _service.GetAvailableRooms();
        var roomDTOs = _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        return Ok(roomDTOs);
    }

    [HttpGet("/room/rat-owners")]
    public async Task<ActionResult<List<RoomDTO>>> GetRoomsForRatOwners()
    {
        var rooms = await _service.GetRoomsForRatOwners();
        var roomDTOs = _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        return Ok(roomDTOs);
    }
}
