public class Person
{
    public Person(string firstName, string lastName, int personId = 0)
    {
        FirstName = firstName;
        LastName = lastName;
        PersonId = personId;
    }

    public int PersonId { get; private set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int AddressId { get; set; }
    [ForeignKey(nameof(AddressId))]
    public virtual Address? Address { get; set; }
}
