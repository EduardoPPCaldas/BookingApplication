using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bookings.DTOs;

namespace Application.Bookings.Responses;

public class BookingResponse : Response
{
    public BookingDTO? Data { get; set; }
}
