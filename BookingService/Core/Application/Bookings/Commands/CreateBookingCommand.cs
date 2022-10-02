using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bookings.DTOs;
using Application.Bookings.Responses;
using MediatR;

namespace Application.Bookings.Commands;

public class CreateBookingCommand : IRequest<BookingResponse>
{
    public BookingDTO bookingDTO { get; set;}
}
