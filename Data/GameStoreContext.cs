using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) 
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Souls-like" },
            new { Id = 2, Name = "Adventures" },
            new { Id = 3, Name = "Fighting" },
            new { Id = 4, Name = "Shooter" }
        );
    }
}
