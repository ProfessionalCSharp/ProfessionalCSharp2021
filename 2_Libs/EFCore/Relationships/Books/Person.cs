public class Person
{
    public Person(string firstName, string lastName, int personId = 0)
    {
        FirstName = firstName;
        LastName = lastName;
        PersonId = personId;
        BusinessAddress = new Address();
    }

    public Person(string firstName, string lastName, Address businessAddress, int personId = 0)
        : this(firstName, lastName, personId)
    {
        BusinessAddress = businessAddress;
    }

    public int PersonId { get; private set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Book> WrittenBooks = new HashSet<Book>();

    public Address BusinessAddress { get; set; }

    public Address? PrivateAddress { get; set; }
}
