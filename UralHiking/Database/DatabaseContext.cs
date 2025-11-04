using Microsoft.EntityFrameworkCore;
using UralHiking.Models;

namespace UralHiking.Database;

public sealed class DatabaseContext : DbContext
{
    public DbSet<HikingRoute> HikingRoutes => Set<HikingRoute>();
    public DbSet<Coordinate> Coordinates => Set<Coordinate>();
    public DbSet<GearItem> GearItems => Set<GearItem>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HikingRoute>()
            .Ignore(x => x.Difficulty)
            .HasMany(x => x.Coordinates)
            .WithOne(x => x.HikingRoute)
            .HasForeignKey(x => x.HikingRouteId);

        modelBuilder.Entity<HikingRoute>()
            .HasMany(x => x.GearItems)
            .WithMany(x => x.HikingRoutes);
    }
}