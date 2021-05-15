using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookModels
{
    public record Book(
        [property: Required]
        [property: MaxLength(50)]
        [property: DisplayName("Title")]
        string Title,
        [property: Required]
        [property:MaxLength(50)]
        [property:DisplayName("Publisher")]
        string Publisher,
        int BookId = 0);
}
