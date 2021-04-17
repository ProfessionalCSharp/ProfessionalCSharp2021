using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace BooksLib.Models
{
    public class Book : ObservableObject
    {
        public Book(string? title = null, string? publisher = null, int id = 0)
        {
            BookId = id;
            Title = title ?? string.Empty;
            Publisher = publisher ?? string.Empty;
        }
        public int BookId { get; set; }
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _publisher;
        public string Publisher
        {
            get => _publisher;
            set => SetProperty(ref _publisher, value);
        }

        public override string ToString() => Title;

    }
}
