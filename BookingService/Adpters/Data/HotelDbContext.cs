using Data.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;
public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) 
    {

    }
    public virtual DbSet<Guest> Guests => Set<Guest>();
    public virtual DbSet<Room> Rooms => Set<Room>();
    public virtual DbSet<Booking> Bookings => Set<Booking>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GuestConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
    }
}
