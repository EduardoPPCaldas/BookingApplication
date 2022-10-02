using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bookings.DTOs;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
using Application.Bookings.Responses;
using Application.Payments;
using Application.Payments.Ports;
using Application.Payments.Requests;
using Application.Payments.Responses;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Bookings;

public class BookingManager : IBookingManager
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPaymentProcessorFactory _paymentProcessorFactory;
    public BookingManager(IBookingRepository bookingRepository,
        IPaymentProcessorFactory paymentProcessorFactory
    )
    {
        _bookingRepository = bookingRepository;
        _paymentProcessorFactory = paymentProcessorFactory;
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

    public async Task<PaymentResponse> PayForBooking(CreatePaymentRequest request)
    {
        var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(request.SelectedPaymentProvider);      

        request.PaymentIntention = request.PaymentIntention ?? "";   

        var response = await paymentProcessor.CapturePayment(request.PaymentIntention);
        if (response.Success)
        {
            return new PaymentResponse
            {
                Success = true,
                Data = response.Data,
                Message = "Payment successfully processed"
            };
        }
        return response;
    }
}
