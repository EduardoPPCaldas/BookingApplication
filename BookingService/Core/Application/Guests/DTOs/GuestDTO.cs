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
                Surname = guestDTO.Surname,
                Email = guestDTO.Email,
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (DocumentType)guestDTO.IdTypeCode
                }
            };
        }

        public static GuestDTO MapToDTO(Guest guest)
        {
            if(guest.DocumentId == null)
            {
                guest.DocumentId = new PersonId
                {
                    DocumentType = DocumentType.DriverLicense,
                    IdNumber = "Add Id Number"
                };
            }
            return new GuestDTO
            {
                Id = guest.Id,
                Email = guest.Email,
                IdNumber = guest.DocumentId.IdNumber,
                IdTypeCode = (int)guest.DocumentId.DocumentType,
                Name = guest.Name,
                Surname = guest.Surname
            };
        }
    }
}