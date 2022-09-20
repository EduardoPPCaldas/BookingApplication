using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Rooms.DTOs;
using Application.Rooms.Ports;
using Application.Rooms.Requests;
using Application.Rooms.Responses;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Rooms;

public class RoomManager : IRoomManager
{
    private readonly IRoomRepository _roomRepository;

    public RoomManager(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
    {
        try
        {
            if(request.Data != null)
            {
                var room = RoomDTO.MapToEntity(request.Data);
                await room.Save(_roomRepository);

                request.Data.Id = room.Id;

                return new RoomResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            throw new MissingFieldException();
        }
        catch (MissingRequiredInformationException)
        {
            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION,
                Message = "Missing required fields"
            };
        }
        catch (System.Exception)
        {
            return new RoomResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.ROOM_COULD_NOT_STORE_DATA,
                Message = "There was an error when saving on DB"
            };
        }
    }
}
