using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Rooms.DTOs;
using Application.Rooms.Ports;
using Application.Rooms.Requests;
using Microsoft.AspNetCore.Mvc;
using ErrorCodes = Application.ErrorCodes;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly ILogger<GuestController> _logger;
    private readonly IRoomManager _roomManager;

    public RoomController(
        ILogger<GuestController> logger,
        IRoomManager roomManager
    )
    {
        _logger = logger;
        _roomManager = roomManager;
    }

    [HttpPost]
    public async Task<ActionResult<RoomDTO>> Post(RoomDTO roomDTO)
    {
        var createRoomRequest = new CreateRoomRequest
        {
            Data = roomDTO
        };
        var res = await _roomManager.CreateRoom(createRoomRequest);

        if(res.Success)
        {
            return Created("", res.Data);
        }

        if(res.ErrorCode == ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION)
        {
            return BadRequest(res);
        }

        if(res.ErrorCode == ErrorCodes.COULD_NOT_STORE_DATA)
        {
            return BadRequest(res);
        }

        return BadRequest(500);
    }
}
