using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bookings.DTOs;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
using Application.Bookings.Responses;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Bookings;

public class BookingManager : IBookingManager
{
    private readonly IBookingRepository _bookingRepository;
    public BookingManager(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
    {
        try
        {
            if(request.Data != null)
            {
                var booking = BookingDTO.MapToEntity(request.Data);
                await booking.Save(_bookingRepository);
                request.Data.Id = booking.Id;

                return new BookingResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            throw new MissingRequiredInformationException();
        }
        catch (MissingRequiredInformationException)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                Message = "Missing required information"
            };
        }
    }
}
