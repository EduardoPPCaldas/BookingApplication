using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Rooms.Requests;
using Application.Rooms.Responses;

namespace Application.Rooms.Ports;

public interface IRoomManager
{
    Task<RoomResponse> CreateRoom(CreateRoomRequest request);
}
