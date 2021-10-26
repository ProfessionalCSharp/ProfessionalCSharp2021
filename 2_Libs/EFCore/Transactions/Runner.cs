using Microsoft.EntityFrameworkCore;

using System.Transactions;

class Runner
{
    private readonly IDbContextFactory<MenusContext> _menusContextFactory;

    public Runner(IDbContextFactory<MenusContext> menusContextFactory) =>
        _menusContextFactory = menusContextFactory;

    public async Task CreateDatabaseAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task AddTwoRecordsWithOneTxAsync()
    {
        Console.WriteLine(nameof(AddTwoRecordsWithOneTxAsync));
        try
        {
            using var context = _menusContextFactory.CreateDbContext();
            var card = context.MenuCards
                .OrderBy(mc => mc.MenuCardId)
                .First();
            MenuItem m1 = new("added")
            {
                MenuCardId = card.MenuCardId,
                Price = 99.99m
            };

            var notExistingCard = Guid.NewGuid();
            MenuItem mInvalid = new("invalid")
            {
                MenuCardId = notExistingCard,
                Price = 999.99m
            };
            context.MenuItems.AddRange(m1, mInvalid);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{records} records added");

        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"{ex.InnerException?.Message}");
        }
        Console.WriteLine();
    }

    public async Task AddTwoRecordsWithTwoTxAsync()
    {
        Console.WriteLine(nameof(AddTwoRecordsWithTwoTxAsync));
        try
        {
            using var context = _menusContextFactory.CreateDbContext();
            var card = context.MenuCards
                .OrderBy(mc => mc.MenuCardId)
                .First();
            MenuItem m1 = new MenuItem("added")
            {
                MenuCardId = card.MenuCardId,
                Price = 99.99m
            };
            context.MenuItems.Add(m1);
            int records = context.SaveChanges();
            Console.WriteLine($"{records} records added");

            var notExistingCard = Guid.NewGuid();
            MenuItem mInvalid = new("invalid")
            {
                MenuCardId = notExistingCard,
                Price = 999.99m
            };
            context.MenuItems.Add(mInvalid);
            records = await context.SaveChangesAsync();
            Console.WriteLine($"{records} records added");

        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"{ex.InnerException?.Message}");
        }
        Console.WriteLine();
    }

    public async Task TwoSaveChangesWithOneTxAsync()
    {
        Console.WriteLine(nameof(TwoSaveChangesWithOneTxAsync));
        using var context = _menusContextFactory.CreateDbContext();
        using var tx = await context.Database.BeginTransactionAsync();
        try
        {
            var card = context.MenuCards
                .OrderBy(mc => mc.MenuCardId)
                .First();
            MenuItem m1 = new("added with explicit tx")
            {
                MenuCardId = card.MenuCardId,
                Price = 99.99m
            };
            context.MenuItems.Add(m1);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{records} records added");

            var notExistingCard = Guid.NewGuid();
            MenuItem mInvalid = new("invalid")
            {
                MenuCardId = notExistingCard,
                Price = 999.99m
            };
            context.MenuItems.Add(mInvalid);
            records = await context.SaveChangesAsync();

            Console.WriteLine($"{records} records added");
            tx.Commit();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"{ex.InnerException?.Message}");
            Console.WriteLine("rolling back…");
            tx?.Rollback();
        }
        Console.WriteLine();
    }

    public async Task AmbientTransactionsAsync()
    {
        Console.WriteLine(nameof(AmbientTransactionsAsync));

        using var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);

        if (Transaction.Current is null) throw new InvalidOperationException("no ambient transaction available");
        Transaction.Current.TransactionCompleted += (sender, e) =>
        {
            var ti = e.Transaction?.TransactionInformation;
            Console.WriteLine($"transaction completed with status: {ti?.Status}, identifier: {ti?.LocalIdentifier}");
        };

        using var context = _menusContextFactory.CreateDbContext();
        try
        {
            var card = context.MenuCards
                .OrderBy(mc => mc.MenuCardId)
                .First();
            MenuItem m1 = new("added with explicit tx")
            {
                MenuCardId = card.MenuCardId,
                Price = 99.99m
            };
            context.MenuItems.Add(m1);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{records} records added");

            var notExistingCard = Guid.NewGuid();
            MenuItem mInvalid = new("invalid")
            {
                MenuCardId = notExistingCard,
                Price = 999.99m
            };
            context.MenuItems.Add(mInvalid);
            records = await context.SaveChangesAsync();

            Console.WriteLine($"{records} records added");
            scope.Complete();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine($"{ex.InnerException?.Message}");
        }

        Console.WriteLine();
    }

    public async Task DeleteDatabaseAsync()
    {
        using var context = _menusContextFactory.CreateDbContext();
        Console.Write("Delete the database? (y|n) ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y")
        {
            bool deleted = await context.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
        }
    }
}
