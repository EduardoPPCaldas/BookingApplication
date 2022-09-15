using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Guests.Requests;
using Application.Guests.Responses;

namespace Application.Guests.Ports
{
    public interface IGuestManager
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);

        Task<GuestResponse> GetGuest(int guestId);
    }
}