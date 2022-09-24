using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Guests.DTOs;
using Application.Rooms.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Bookings.DTOs;

public class BookingDTO
{
    public int Id { get; set; }
    public DateTime PlaceAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RoomDTO? Room { get; set; }
    public GuestDTO? Guest { get; set; }
    public Status Status => Status.Created;

    public static Booking MapToEntity(BookingDTO dto)
    {
        if(dto.Room != null && dto.Guest != null)
        {
            return new Booking
            {
                PlaceAt = dto.PlaceAt,
                Start = dto.Start,
                End = dto.End,
                Room = RoomDTO.MapToEntity(dto.Room),
                Guest = GuestDTO.MapToEntity(dto.Guest),
                Id = dto.Id
            };
        }
        throw new MissingRequiredInformationException();
    }
}
