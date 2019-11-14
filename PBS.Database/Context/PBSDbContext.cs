using Microsoft.EntityFrameworkCore;
using PBS.Database.Models;

namespace PBS.Database.Context
{
    public class PbsDbContext : DbContext
    {
        public PbsDbContext (DbContextOptions options) : base (options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> Claims { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<ParkingLotImage> ParkingLotImages { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<SlotType> SlotTypes { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slot> ()
                .Property (x => x.HourlyRate)
                .HasDefaultValue (0);

            modelBuilder.Entity<Booking> ()
                .Property (x => x.Amount)
                .HasDefaultValue (0);

            modelBuilder.Entity<User> ()
                .Property (x => x.IsEmailConfirmed)
                .HasDefaultValue (false);
        }
    }
}
