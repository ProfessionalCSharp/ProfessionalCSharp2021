using AsyncStreaming.Shared;

using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreAPIAsyncStreaming.Models
{
    public class SomeDataContext : DbContext
    {
        public SomeDataContext(DbContextOptions<SomeDataContext> options)
            : base(options) { }

        public DbSet<SomeData> SomeData => Set<SomeData>();
    }
}
