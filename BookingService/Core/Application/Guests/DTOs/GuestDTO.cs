using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Guests.DTOs
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Guest MapToEntity(GuestDTO guestDTO)
        {
            return new Guest
            {
                Id = guestDTO.Id,
                Name = guestDTO.Name,
                Surname = guestDTO.Name,
                Email = guestDTO.Email,
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (DocumentType)guestDTO.IdTypeCode
                }
            };
        }
    }
}