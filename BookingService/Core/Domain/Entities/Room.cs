using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public Price? Price { get; set; }

        public bool IsAvailable
        {
            get 
            {
                if(this.InMaintenance || this.HasGuest)
                {
                    return false;
                }
                return true;
            }
        }

        public bool HasGuest
        {
            get
            {
                return true;
            }
        }
        private void ValidateState()
        {

            if (string.IsNullOrEmpty(Name))
            {
                throw new MissingRequiredInformationException();
            }
        }
        public async Task Save(IRoomRepository roomRepository)
        {
            ValidateState();
            if(this.Id == 0)
            {
                this.Id = await roomRepository.Create(this);
            }
            else 
            {
                // await guestRepository.Update(this);
            }
        }
    }
}