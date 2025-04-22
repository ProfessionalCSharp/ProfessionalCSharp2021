namespace AutoInclude;

public class Person(string firstName, string lastName, int personId = 0)
{
    public int PersonId { get; private set; } = personId;

    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;

    public int AddressId { get; set; }
    [ForeignKey(nameof(AddressId))]
    public virtual Address? Address { get; set; }
}
