using System.ComponentModel.DataAnnotations;

public record Book(
    [StringLength(50)] string Title,
    string? Publisher = default,
    int BookId = 0);
