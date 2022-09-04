using Domain.Entities;
using Domain.Enums;
using Xunit;

namespace DomainTests.Bookings;

public class UnitTest1
{
    [Fact]
    public void ShouldAlwaysStartWithCreatedStatus()
    {
        var booking = new Booking();
        Assert.Equal(Status.Created, booking.CurrentStatus);
    }

    [Fact]
    public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
    {
        var booking = new Booking();
        booking.ChangeState(Action.Pay);
        Assert.Equal(Status.Paid, booking.CurrentStatus);
    }

    [Fact]
    public void ShouldSetStatusToCanceledWhenCancelingABookingWithCreatedStatus()
    {
        var booking = new Booking();
        booking.ChangeState(Action.Cancel);
        Assert.Equal(Status.Canceled, booking.CurrentStatus);
    }

    [Fact]
    public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
    {
        var booking = new Booking();
        booking.ChangeState(Action.Pay);
        booking.ChangeState(Action.Finish);
        Assert.Equal(Status.Finished, booking.CurrentStatus);
    }

    [Fact]
    public void ShouldSetStatusToRefoundedWhenRefoundedAPaidBooking()
    {
        var booking = new Booking();
        booking.ChangeState(Action.Pay);
        booking.ChangeState(Action.Refound);
        Assert.Equal(Status.Refounded, booking.CurrentStatus);
    }

    [Fact]
    public void ShouldSetStatusToCreatedWhenReopenAFinishedBooking()
    {
        var booking = new Booking();
        booking.ChangeState(Action.Cancel);
        booking.ChangeState(Action.Reopen);
        Assert.Equal(Status.Created, booking.CurrentStatus);
    }

    // Need to create not available status change tests
}