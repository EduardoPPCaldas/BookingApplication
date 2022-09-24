using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bookings.Requests;
using Application.Bookings.Responses;

namespace Application.Bookings.Ports;

public interface IBookingManager
{
    Task<BookingResponse> CreateBooking(CreateBookingRequest request);
}
