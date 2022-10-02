using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bookings.DTOs;
using Application.Bookings.Responses;
using Domain.Entities;
using Domain.Ports;
using MediatR;

namespace Application.Bookings.Queries;

public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingResponse>
{
    private readonly IBookingRepository _bookingRepository;
    public GetBookingQueryHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    public async Task<BookingResponse> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.Get(request.Id);
        var dto = BookingDTO.MapToDTO(booking);
        return new BookingResponse
        {
            Data = dto,
            Success = true
        };
    }
}
