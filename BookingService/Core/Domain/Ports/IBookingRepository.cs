using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports;

public interface IBookingRepository
{
    Task<int> Create(Booking booking);

    Task<Booking> Get(int id);
}
