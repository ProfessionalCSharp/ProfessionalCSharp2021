using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options)
        : base(options) {}

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("pm");

        modelBuilder.Entity<Payment>()
            .HasDiscriminator<string>("Type")
            .HasValue<CashPayment>("cash")
            .HasValue<CreditcardPayment>("creditcard");

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasColumnType("Money");
    }
}

