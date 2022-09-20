using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Rooms.DTOs;

namespace Application.Rooms.Requests;

public class CreateRoomRequest
{
    public RoomDTO? Data { get; set; }
}
