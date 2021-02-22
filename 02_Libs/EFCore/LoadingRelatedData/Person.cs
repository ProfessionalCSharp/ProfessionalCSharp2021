class Person
{
    public Person(string firstName, string lastName, int personId = 0)
    {
        FirstName = firstName;
        LastName = lastName;
        PersonId = personId;
    }

    public int PersonId { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Address? Address { get; set; }
}

