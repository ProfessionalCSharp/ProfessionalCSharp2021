using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BooksLib.Models
{
    public class Book : ObservableObject
    {
        public int BookId { get; set; }

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string? _publisher;
        public string? Publisher
        {
            get => _publisher;
            set => SetProperty(ref _publisher, value);
        }

        public override string ToString() => Title ?? string.Empty;
    }
}
