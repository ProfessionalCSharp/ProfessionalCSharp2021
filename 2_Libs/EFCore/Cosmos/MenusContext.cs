﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

internal class MenusContext(DbContextOptions<MenusContext> options, IOptions<RestaurantConfiguration> restaurantOptions) : DbContext(options)
{
    private readonly string _restaurantId = restaurantOptions.Value.RestaurantId ?? throw new System.Exception("restaurantid required");

    public DbSet<MenuCard> MenuCards => Set<MenuCard>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("menucards");

        modelBuilder.Entity<MenuCard>().OwnsMany(c => c.MenuItems);
        modelBuilder.Entity<MenuCard>().HasKey(c => c.MenuCardId);

        modelBuilder.Entity<MenuCard>().HasPartitionKey(c => c.RestaurantId);
//        modelBuilder.Entity<MenuCard>().HasQueryFilter(c => c.RestaurantId == _restaurantId);
    }
}

