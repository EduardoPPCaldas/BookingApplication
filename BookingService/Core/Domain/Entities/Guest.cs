using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using Domain.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public PersonId? DocumentId { get; set; }

        public bool IsValid()
        {
            this.ValidateState();
            return true;
        }
        private void ValidateState()
        {
            if (DocumentId == null ||
                DocumentId.IdNumber == null ||
                DocumentId.IdNumber.Length <= 3 ||
                DocumentId.DocumentType == 0)
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Surname) ||
                string.IsNullOrEmpty(Email))
            {
                throw new MissingRequiredInformationException();
            }
            if(!Utils.ValidateEmail(Email))
            {
                throw new InvalidEmailException();
            }
        }
        public async Task Save(IGuestRepository guestRepository)
        {
            ValidateState();
            if(this.Id == 0)
            {
                this.Id = await guestRepository.Create(this);
            }
            else 
            {
                // await guestRepository.Update(this);
            }
        }
    }
}