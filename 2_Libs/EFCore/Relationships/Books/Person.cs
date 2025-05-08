public class Person(string firstName, string lastName, int personId = 0)
{
    public Person(string firstName, string lastName, Address businessAddress, int personId = 0)
        : this(firstName, lastName, personId)
    {
        BusinessAddress = businessAddress;
    }

    public int PersonId { get; private set; } = personId;

    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;

    public ICollection<Book> WrittenBooks = new HashSet<Book>();

    public Address BusinessAddress { get; set; } = new Address();

    public Address? PrivateAddress { get; set; }
}
