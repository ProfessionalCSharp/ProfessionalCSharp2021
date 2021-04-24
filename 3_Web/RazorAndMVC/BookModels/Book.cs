using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace BookModels
{
    public record Book(
        [property: MaxLength(50)]
        [property: DisplayName("Title")]
        string Title,
        [property:MaxLength(50)]
        [property:DisplayName("Publisher")]
        string Publisher,
        int BookId = 0);
}
