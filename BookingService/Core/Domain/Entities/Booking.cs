using Domain.Enums;
using Domain.Exceptions;
using Domain.Ports;
using Action = Domain.Enums.Action;

namespace Domain.Entities
{
    public class Booking
    {
        public Booking()
        {
            this.Status = Status.Created;
        }
        public int Id { get; set; }
        public DateTime PlaceAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room? Room { get; set; }
        public Guest? Guest { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus { get { return this.Status; } }

        public void ChangeState(Action action)
        {
            this.Status = (this.Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => this.Status
            };
        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            this.ValidateState();
            if(this.Id == 0)
            {
                await bookingRepository.Create(this);
            }
            else
            {
                // await bookingRepository.Update(this);
            }
        }

        private void ValidateState()
        {
            if (Room == null || Guest == null)
            {
                throw new MissingRequiredInformationException();
            }
        }
    }
}