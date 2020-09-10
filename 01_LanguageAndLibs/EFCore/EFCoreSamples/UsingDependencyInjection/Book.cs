using System.ComponentModel.DataAnnotations.Schema;

namespace UsingDependencyInjection
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Publisher { get; set; }
    }
}
