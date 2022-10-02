using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Bookings.Commands;
using Application.Bookings.DTOs;
using Application.Bookings.Ports;
using Application.Bookings.Queries;
using Application.Bookings.Requests;
using Application.Bookings.Responses;
using Application.Payments.DTOs;
using Application.Payments.Requests;
using Application.Payments.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingManager _bookingManager;
    private readonly IMediator _mediator;
    public BookingController(IBookingManager bookingManager,
        IMediator mediator
    )
    {
        _bookingManager = bookingManager;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<BookingDTO>> Post(BookingDTO bookingDTO)
    {
        var command = new CreateBookingCommand
        {
            bookingDTO = bookingDTO
        };
        var res = await _mediator.Send(command);

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

    [HttpGet]
    public async Task<ActionResult<BookingResponse>> Get(int id)
    {
        var query = new GetBookingQuery
        {
            Id = id
        };
        var res = await _mediator.Send(query);
        if(res.Success) return Ok(res.Data);

        if(res.ErrorCode == ErrorCodes.BOOKING_NOT_FOUND) return NotFound(res);

        return BadRequest(res);
    }
}
