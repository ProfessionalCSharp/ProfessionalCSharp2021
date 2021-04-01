using Books.Models;
using Books.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Data
{
    public class BooksContext : DbContext, IBookChapterService
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<BookChapter> Chapters => Set<BookChapter>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookChapter>().Property(b => b.Title).HasMaxLength(120);
        }

        public async Task AddAsync(BookChapter chapter)
        {
            await Chapters.AddAsync(chapter);
            await SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            await this.Chapters.AddRangeAsync(chapters);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<BookChapter>> GetAllAsync()
        {
            var chapters = await Chapters.ToListAsync();
            return chapters;
        }

        public async Task<BookChapter?> FindAsync(Guid id)
        {
            var chapter = await Chapters.FindAsync(id);
            return chapter;
        }

        public async Task<BookChapter?> RemoveAsync(Guid id)
        {
            var chapter = await Chapters.FindAsync(id);
            Chapters.Remove(chapter);
            await SaveChangesAsync();
            return chapter;
        }

        public async Task<BookChapter?> UpdateAsync(BookChapter chapter)
        {
            Chapters.Update(chapter);
            await SaveChangesAsync();
            return chapter;
        }
    }
}
