class GreetingService
{
    public string Greet(Person person)
    {
        return $"Hello, {person.FirstName}!";
    }
}

