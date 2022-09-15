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
                return NotFound(res);
            }
            
            if(res.ErrorCode == ErrorCodes.INVALID_EMAIL)
            {
                return BadRequest(res);
            }

            if(res.ErrorCode == ErrorCodes.INVALID_PERSON_ID)
            {
                return BadRequest(res);
            }

            if(res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown error code", res);
            return BadRequest(500);
        }

        [HttpGet("{guestId}")]
        public async Task<ActionResult<GuestDTO>> Get(int guestId)
        {
            var res = await _guestManager.GetGuest(guestId);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }
    }
}