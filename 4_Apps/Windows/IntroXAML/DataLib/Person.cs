namespace DataLib
{
    public class Person
    {
        public Person(string firstName, string lastName) =>
            (FirstName, LastName) = (firstName, lastName);

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString() => $"{FirstName} {LastName}";
    }
}
