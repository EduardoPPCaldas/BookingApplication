using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Bookings.DTOs;
using Application.Bookings.Ports;
using Application.Bookings.Requests;
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
}
