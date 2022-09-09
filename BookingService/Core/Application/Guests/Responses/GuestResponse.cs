using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Guests.DTOs;

namespace Application.Guests.Responses
{
    public class GuestResponse : Response
    {
        public GuestDTO? Data;
    }
}