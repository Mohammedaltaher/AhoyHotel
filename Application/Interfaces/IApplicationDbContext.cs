namespace Application.Interfaces;
public interface IApplicationDbContext
{

    DbSet<Hotel> Hotels { get; set; }
    DbSet<HotelFacility> HotelFacilities { get; set; }
    DbSet<HotelImage> HotelImages { get; set; }
    DbSet<Facility> Facilities { get; set; }
    DbSet<Room> Rooms { get; set; }
     DbSet<Booking> Bookings { get; set; }
    DbSet<Review> Reviews { get; set; }
    Task<int> SaveChangesAsync();
}

