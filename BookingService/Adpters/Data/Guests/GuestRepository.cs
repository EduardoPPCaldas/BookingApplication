using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports;

namespace Data.Guests
{
    public class GuestRepository : IGuestRepository
    {
        private HotelDbContext _hotelDbContext;
        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }

        public Task<Guest> Get(int Id)
        {
            throw new NotImplementedException();
        }
    }
}