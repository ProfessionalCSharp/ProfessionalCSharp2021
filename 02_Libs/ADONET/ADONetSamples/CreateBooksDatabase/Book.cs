using System;
using System.ComponentModel.DataAnnotations;

namespace CreateBooksDatabase
{
    public class Book
    {
        public Book(int id, string title, string publisher, string? isbn = default, DateTime? releaseDate = default)
        {
            Id = id;
            Title = title;
            Publisher = publisher;
            Isbn = isbn;
            ReleaseDate = releaseDate;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string? Publisher { get; set; }
        public string? Isbn { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
    }
}
