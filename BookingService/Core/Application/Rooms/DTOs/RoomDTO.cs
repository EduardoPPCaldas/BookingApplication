using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Rooms.DTOs;

public class RoomDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public decimal Value { get; set; }
    public AcceptedCurrencies Currency { get; set; }

    public static Room MapToEntity(RoomDTO dto)
    {
        return new Room
        {
            Id = dto.Id,
            InMaintenance = dto.InMaintenance,
            Level = dto.Level,
            Name = dto.Name,
            Price = new Price
            {
                Currency = dto.Currency,
                Value = dto.Value
            }
        };
    }

    public static RoomDTO MapToDto(Room room)
    {
        return new RoomDTO
        {
            Currency = room.Price.Currency,
            Id = room.Id,
            InMaintenance = room.InMaintenance,
            Level = room.Level,
            Name = room.Name,
            Value = room.Price.Value
        };
    }
}
