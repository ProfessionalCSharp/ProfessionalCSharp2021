using Microsoft.EntityFrameworkCore;

using MultipleProvidersWithAspire.ApiService.Models;

namespace MultipleProvidersWithAspire.ApiService.Data;

public class BooksContext(DbContextOptions<BooksContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
}
