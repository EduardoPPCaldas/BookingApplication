using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
using Application.Bookings.Responses;
using MediatR;

namespace Application.Bookings.Commands;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
{
    private readonly IBookingManager _bookingManager;

    public CreateBookingHandler(IBookingManager bookingManager)
    {
        _bookingManager = bookingManager;
    }
    public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var req = new CreateBookingRequest
        {
            Data = request.bookingDTO
        };
        return await _bookingManager.CreateBooking(req);
    }
}
