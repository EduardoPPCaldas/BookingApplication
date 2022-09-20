using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports;

namespace Data.Rooms;

public class RoomRepository : IRoomRepository
{
    private readonly HotelDbContext _hotelDbContext;

    public RoomRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }

    public async Task<int> Create(Room room)
    {
        _hotelDbContext.Rooms.Add(room);
        await _hotelDbContext.SaveChangesAsync();
        return room.Id;
    }

    public Task<Room> Get(int Id)
    {
        throw new NotImplementedException();
    }
}
