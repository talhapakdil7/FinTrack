using Microsoft.EntityFrameworkCore;
using FinTrack.API.Entities;

namespace FinTrack.API.Data;

// DbContext = database bağlantı noktası
public class AppDbContext : DbContext
{
    // Constructor (connection alır)
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Transactions tablosu
    public DbSet<Transaction> Transactions { get; set; }
}