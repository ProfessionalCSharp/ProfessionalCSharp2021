namespace StoredProceduresWithEFCore.Data;

public partial class Person
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? AddressLineOne { get; set; }

    public string? AddressLineTwo { get; set; }

    public string? BusinessAddressLocationCountry { get; set; }

    public string? BusinessAddressLocationCity { get; set; }

    public virtual PrivateAddress? PrivateAddress { get; set; }

    public virtual ICollection<Book> WrittenBooksBooks { get; set; } = new List<Book>();
}
