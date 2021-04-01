using System;

namespace Books.Models
{
    public record BookChapter(Guid Id, int Number, string Title, int PageCount);
}
