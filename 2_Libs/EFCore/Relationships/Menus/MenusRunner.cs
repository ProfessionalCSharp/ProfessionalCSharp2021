public class MenusRunner(MenusContext menusContext)
{
    public async Task CreateTheDatabaseAsync()
    {
        await menusContext.Database.MigrateAsync();
    }

    public async Task DeleteTheDatabaseAsync()
    {
        await menusContext.Database.EnsureDeletedAsync();
    }
}
