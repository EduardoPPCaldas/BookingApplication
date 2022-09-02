using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports
{
    public interface IGuestRepository
    {
        Task<Guest> Get(int Id);
        Task<int> Save(Guest guest);
    }
}