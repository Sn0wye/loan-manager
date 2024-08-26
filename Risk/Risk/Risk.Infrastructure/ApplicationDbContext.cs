namespace Risk.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Risk.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public DbSet<Loan> Loans { get; init; }
    public DbSet<User> Users { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,1433;Database=loan;User Id=sa;Password=Odei0React!;Encrypt=False;TrustServerCertificate=True;")
            .UseSnakeCaseNamingConvention();
    }
}