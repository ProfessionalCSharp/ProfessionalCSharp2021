using System.ComponentModel;

namespace ASPNETCoreLocalization.Models;

public record Book(
    [property: DisplayName("BookTitle")] string Title,
    [property: DisplayName("Publisher")] string Publisher);
