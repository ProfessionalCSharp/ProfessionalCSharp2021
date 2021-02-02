using System.ComponentModel.DataAnnotations;

public record Book(
    int BookId = 0,
    [StringLength(50)] string Title = "",
    string? Publisher = default);
