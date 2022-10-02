using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Bookings;

public class BookingRepository : IBookingRepository
{
    private readonly HotelDbContext _hotelDbContext;

    public BookingRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }
    public async Task<int> Create(Booking booking)
    {
        _hotelDbContext.Bookings.Add(booking);
        await _hotelDbContext.SaveChangesAsync();
        return booking.Id;
    }

    public async Task<Booking?> Get(int id)
    {
        return await _hotelDbContext.Bookings
        .Include(b => b.Guest)
        .Include(b => b.Room)
        .Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
