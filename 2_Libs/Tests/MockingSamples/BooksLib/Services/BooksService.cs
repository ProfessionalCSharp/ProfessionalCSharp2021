﻿using BooksLib.Models;
using BooksLib.Repositories;
using System.Collections.ObjectModel;

namespace BooksLib.Services;

public class BooksService : IBooksService
{
    private readonly ObservableCollection<Book> _books = new();
    private readonly IBooksRepository _booksRepository;
    public BooksService(IBooksRepository repository)
        => _booksRepository = repository;

    public async Task LoadBooksAsync()
    {
        if (_books.Count > 0) return;

        IEnumerable<Book> books = await _booksRepository.GetItemsAsync();
        _books.Clear();
        foreach (var b in books)
        {
            _books.Add(b);
        }
    }

    public Book? GetBook(int bookId) =>
        _books.Where(b => b.BookId == bookId).SingleOrDefault();

    public async Task<Book> AddOrUpdateBookAsync(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);

        Book? updated = null;
        if (book.BookId == 0)
        {
            updated = await _booksRepository.AddAsync(book);
            _books.Add(updated);
        }
        else
        {
            updated = await _booksRepository.UpdateAsync(book);
            if (updated is null) throw new InvalidOperationException();

            Book old = _books.Where(b => b.BookId == updated.BookId).Single();
            int ix = _books.IndexOf(old);
            _books.RemoveAt(ix);
            _books.Insert(ix, updated);
        }
        return updated;
    }

    public IEnumerable<Book> Books => _books;
}
