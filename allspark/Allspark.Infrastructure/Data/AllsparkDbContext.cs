using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Data;

public interface IAllsparkDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Assignment> Assignments { get; set; }
}

[ExcludeFromCodeCoverage]
public class AllsparkDbContext : DbContext, IAllsparkDbContext
{
    public AllsparkDbContext(DbContextOptions<AllsparkDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Assignment > Assignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the entity mappings and relationships here
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AllsparkDbContext).Assembly);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<User>()
            .Property(p => p.FullName)
            .HasColumnType("nvarchar(255)");

        modelBuilder.Entity<User>()
            .Property(p => p.Email)
            .HasColumnType("nvarchar(255)");

        modelBuilder.Entity<User>()
           .Property(p => p.Role)
           .HasColumnType("tinyint")
           .HasDefaultValue(3);

        modelBuilder.Entity<User>()
           .Property(p => p.CreatedAt)
           .HasColumnType("datetime")
           .HasDefaultValue(DateTime.UtcNow);
    }
}