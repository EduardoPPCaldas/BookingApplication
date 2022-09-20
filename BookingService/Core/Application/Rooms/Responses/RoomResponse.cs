using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Rooms.DTOs;

namespace Application.Rooms.Responses;

public class RoomResponse : Response
{
    public RoomDTO? Data { get; set; }
}
