using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Guests.DTOs;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;
        public GuestController(ILogger<GuestController> logger, IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guestDTO)
        {
            var createGuestRequest = new CreateGuestRequest
            {
                Data = guestDTO
            };
            var res = await _guestManager.CreateGuest(createGuestRequest);

            if(res.Success)
            {
                return Created("", res.Data);
            }

            if(res.ErrorCode == ErrorCodes.NOT_FOUND)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown error code", res);
            return BadRequest(500);
        }
    }
}