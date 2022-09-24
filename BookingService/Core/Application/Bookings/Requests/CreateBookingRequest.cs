using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bookings.DTOs;

namespace Application.Bookings.Requests;

public class CreateBookingRequest
{
    public BookingDTO? Data { get; set; }
}
