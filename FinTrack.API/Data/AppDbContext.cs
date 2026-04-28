using Microsoft.EntityFrameworkCore;
using FinTrack.API.Entities;

namespace FinTrack.API.Data;

// DbContext = database bağlantı noktası
public class AppDbContext : DbContext
{
    // Constructor (connection alır)
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) //Gelen options’ı üst class’a (DbContext) gönderiyorsun
    {
    }

    // Transactions tablosu
    public DbSet<Transaction> Transactions { get; set; }

    // Users tablosu
    public DbSet<User> Users { get; set; }

    public DbSet<Category> Categories { get; set; }
}