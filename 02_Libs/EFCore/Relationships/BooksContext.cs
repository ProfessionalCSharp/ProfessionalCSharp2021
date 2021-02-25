using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("books");

        modelBuilder.ApplyConfiguration<Person>(new PersonConfiguration());

        var authors = GetAuthors();
        modelBuilder.Entity<Person>().HasData(authors);
        var books = GetBooks();
        modelBuilder.Entity<Book>().HasData(books);
    }

    private ICollection<Book> GetBooks()
    {
        string[] titles =
        {
            "Professional C# and .NET 2021 Edition",  // CN
            "Professional C# 7 and .NET Core 2.0", // CN
            "Professional C# 6 and .NET Core 1.0", // CN
            "Professional C# 5.0 and .NET 4.5.1", // CN, Jay Glynn, Morgan Skinner
            "Professional C# 2012 and .NET 4.5", // CN, Jay Glynn, Morgan Skinner
            "Professional C# 4 and .NET 4",
            "Professional C# 2008", // CN, Bill Evjen, Karli Watson, Morgan Skinner, Jay Glynn
            "Professional C# 2005 with .NET 3.0", // CN, Bill Evjen, Jay Glynn, Karli Watson, Morgan Skinner
            "Professional C# 2005", // CN, Bill Evjen, Jay Glynn, Karli Watson, Morgan Skinner, Allen Jones
            "Professional C# 3rd Edition", // CN, Simon Robinson, Karli Watson, Morgan Skinner, Bill Evjen, 4-jun-2004
            "Professional C# 2nd Edition", // CN, Simon Robinson, K S Allen, Ollie Cornes, Jay Glynn, Zach Greenvoss, Burton Harvey, Morgan Skinner, Karli Watson
            "Professional C#", // CN, Simon Robinson, Ollie Cornes, Jay Glynn 01-Jun-2001
        };
        DateTime?[] dates =
        {
            null,
            new DateTime(2018/5/29), // 7
            new DateTime(2016/4/29), // 6
            new DateTime(2014/2/14), // 5
            new DateTime(2012/10/30), // 2012 4.5
            new DateTime(2010/3/5), // 4
            new DateTime(2008/3/28), // 2008
            new DateTime(2007/3/15), // 2005 and 3
            new DateTime(2005/11/4), // 2005
            new DateTime(2004/6/4), // 3rd
            new DateTime(2003/2/28), // 2nd
            new DateTime(2001/6/1) // 1st
        };
        return Enumerable.Range(0, 12).Select(i => new Book(titles[i], "Wrox Press", i + 1) { ReleaseDate = dates[i] }).ToArray();
    }

    private IList<Person> GetAuthors()
    {
        return new[]
        {
            new Person("Allen", "Jones", 1),
            new Person("Bill", "Evjen", 2),
            new Person("Burton", "Harvey", 3),
            new Person("Christian", "Nagel", 4),
            new Person("Jay", "Glynn", 5),
            new Person("Karli", "Watson", 6),
            new Person("K S", "Allen", 7),
            new Person("Morgan", "Skinner", 8),
            new Person("Ollie", "Cornes", 9),
            new Person("Simon", "Robinson", 10),
            new Person("Zach", "Greenvoss", 11)
        };
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Person> People => Set<Person>();
}
