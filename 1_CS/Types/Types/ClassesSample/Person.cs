public class Person
{ 
    public Person(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }

    private readonly string _firstName;
    public string FirstName => _firstName;
    private readonly string _lastName;
    public string LastName => _lastName;

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