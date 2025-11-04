using Microsoft.EntityFrameworkCore;
using OrderApp.Domain.Entities;

namespace OrderApp.Infraestructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem>  OrderItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.OrderDate)
                .IsRequired();

            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)");
            entity.HasOne<Order>()
                .WithMany(o => o.Items)
                .HasForeignKey(oid => oid.OrderId);
        });
    }
}