using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace WebApi.Test;
public class SharedDatabaseFixture
{
    private static bool _databaseInitialized;
    public ApplicationDbContext? Context { get; set; }

    public SharedDatabaseFixture()
    {
        if (!_databaseInitialized)
        {
            using ApplicationDbContext context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(HotelData.MockHotelSamples());
            context.AddRange(FacilityData.MockFacilitySamples());
            context.SaveChanges();

            _databaseInitialized = true;
        }
    }

    public static ApplicationDbContext CreateContext() => new(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("HotelMemoryDB").Options);
}