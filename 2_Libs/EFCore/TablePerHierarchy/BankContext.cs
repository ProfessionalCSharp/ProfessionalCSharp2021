using Microsoft.EntityFrameworkCore;

class BankContext : DbContext
{
    private static class ColumnNames
    {
        public const string Type = nameof(Type);
    }

    private static class ColumnValues
    {
        public const string Cash = nameof(Cash);
        public const string Creditcard = nameof(Creditcard);
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>().Property(p => p.Name).IsRequired();
        modelBuilder.Entity<Payment>().Property(p => p.Amount).HasColumnType("Money");
        modelBuilder.Entity<Payment>().Property<string>(ColumnNames.Type); // shadow state for the discriminator
        modelBuilder.Entity<Payment>().HasDiscriminator<string>(ColumnNames.Type)
            .HasValue<CashPayment>(ColumnValues.Cash)
            .HasValue<CreditcardPayment>(ColumnValues.Creditcard);
    }

    public DbSet<Payment> Payments => Set<Payment>();
}
