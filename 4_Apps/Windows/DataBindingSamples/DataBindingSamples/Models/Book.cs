using System.Collections.Generic;

namespace DataBindingSamples.Models
{
    public class Book : ObservableObject
    {
        public Book(int id, string title, string publisher, params string[] authors)
        {
            BookId = id;
            _title = title;
            _publisher = publisher;
            Authors = authors;
        }
        public int BookId { get; }
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
        public IEnumerable<string> Authors { get; }

        public override string ToString() => Title;
    }
}
