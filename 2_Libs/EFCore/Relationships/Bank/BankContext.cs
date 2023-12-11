public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
{
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("bank");

        modelBuilder.Entity<Payment>()
            .HasDiscriminator<string>("Type")
            .HasValue<CashPayment>("cash")
            .HasValue<CreditcardPayment>("creditcard");

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasColumnType("Money");
    }
}
