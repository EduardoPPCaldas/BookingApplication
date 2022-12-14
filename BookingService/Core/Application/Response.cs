using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public enum ErrorCodes
    {
        // 1 to 99 - GUESTS
        NOT_FOUND = 1,
        COULD_NOT_STORE_DATA = 2,
        INVALID_PERSON_ID = 3,
        MISSING_REQUIRED_INFORMATION = 4,
        INVALID_EMAIL = 5,
        GUEST_NOT_FOUND = 6,

        // 100 to 199 - ROOMS

        ROOM_NOT_FOUND = 100,
        ROOM_COULD_NOT_STORE_DATA = 101,
        ROOM_INVALID_PERSON_ID = 102,
        ROOM_MISSING_REQUIRED_INFORMATION = 103,
        ROOM_INVALID_EMAIL = 104,

        // Booking related codes 200 - 300
        BOOKING_NOT_FOUND = 200,

        // Payment related codes 500 - 1000
        PAYMENTS_INVALID_PAYMENT_INTENTION = 500,
        PAYMENTS_PAYMENT_PROVIDER_NOT_IMPLEMENTED = 501

    }
    public abstract class Response
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}