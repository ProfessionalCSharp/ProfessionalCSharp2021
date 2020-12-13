public class Person
{ 
    public Person(string firstName, string lastName)
    {
        _firstName = firstName;
        LastName = lastName;
    }

    private readonly string _firstName;
    public string FirstName => _firstName;

    public string LastName { get; }
    public string FullName => $"{FirstName} {LastName}";

    private int _age;
    public int Age
    {
        get => _age;
        set => _age = value;
    }

    public void Deconstruct(out string firstName, out string lastName, out int age)
    {
        firstName = FirstName;
        lastName = LastName;
        age = Age;
    }
}