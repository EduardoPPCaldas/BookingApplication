using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports;

public interface IRoomRepository
{
    Task<Room> Get(int Id);
    Task<int> Create(Room room);
}
