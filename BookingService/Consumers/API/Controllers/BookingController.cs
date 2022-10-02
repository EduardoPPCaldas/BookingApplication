using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Bookings.DTOs;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
using Application.Payments.DTOs;
using Application.Payments.Requests;
using Application.Payments.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingManager _bookingManager;
    public BookingController(IBookingManager bookingManager)
    {
        _bookingManager = bookingManager;
    }

    [HttpPost]
    public async Task<ActionResult<BookingDTO>> Post(BookingDTO bookingDTO)
    {
        var request = new CreateBookingRequest
        {
            Data = bookingDTO
        };
        var res = await _bookingManager.CreateBooking(request);
        if(res.Success) return Created("", res.Data);
        if(res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION) return BadRequest(res);
        return BadRequest(500);
    }

    [HttpPost("{bookingId}/pay")]
    public async Task<ActionResult<PaymentResponse>> Pay(
        CreatePaymentRequest createPaymentRequest,
        int bookingId
    )
    {
        createPaymentRequest.BookingId = bookingId;
        var res = await _bookingManager.PayForBooking(createPaymentRequest);

        if (res.Success) return Created("", res);

        return BadRequest(res);
    }
}
