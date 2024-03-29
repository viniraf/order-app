﻿using OrderApp.Domain.Order;

namespace OrderApp.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<OrderClass> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<Notification>();

        modelBuilder.Entity<Product>()
            .Property(p => p.Name).IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(255);
        modelBuilder.Entity<Product>()
            .Property(p => p.Price).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<Product>()
            .Property(p => p.Price).IsRequired();

        modelBuilder.Entity<Category>()
            .Property(c => c.Name).IsRequired();

        modelBuilder.Entity<OrderClass>()
            .Property(o => o.ClientId).IsRequired();
        modelBuilder.Entity<OrderClass>()
            .Property(o => o.DeliveryAddress).IsRequired();
        modelBuilder.Entity<OrderClass>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
            .UsingEntity(x => x.ToTable("OrderProducts"));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(100);
    }
}
