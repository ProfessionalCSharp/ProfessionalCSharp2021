using System.ComponentModel.DataAnnotations;

namespace Intro
{
    public class Book
    {
        public int BookId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        public string? Publisher { get; set; }
    }
}
