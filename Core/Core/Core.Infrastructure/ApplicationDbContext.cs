using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(
                "Server=tcp:127.0.0.1,1433;Database=loan;User Id=sa;Password=Odei0React!;Encrypt=False;TrustServerCertificate=True;")
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Unique index for Email
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Unique index for Document
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Document)
            .IsUnique();
    }
}