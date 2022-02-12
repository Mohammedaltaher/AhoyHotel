using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Context;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        MapModelBuilder(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
    private static void MapModelBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<HotelFacility>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<HotelImage>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Room>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Booking>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Review>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Facility>().HasQueryFilter(p => !p.IsDeleted);
    }
    public async Task<int> SaveChangesAsync()
    {
        UpdateUpdateDate();
        return await base.SaveChangesAsync();
    }
   
    private void UpdateUpdateDate()
    {
        var UpdateDate = "UpdateDate";
        ChangeTracker.DetectChanges();
        var modified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        foreach (EntityEntry entity in modified)
        {
            foreach (PropertyEntry prop in entity.Properties)
            {
                if (prop.Metadata.Name == UpdateDate)
                {
                    entity.CurrentValues[UpdateDate] = DateTime.Now;
                }
            }
        }
    }

    #region DbSet Properties
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<HotelFacility> HotelFacilities { get; set; }
    public DbSet<HotelImage> HotelImages { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    #endregion
}

