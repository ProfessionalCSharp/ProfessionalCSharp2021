public static class PeopleFactory
{
    private static int s_peopleCount;
    public static Person CreatePerson(string firstName, string lastName)
    {
        s_peopleCount++;
        return new Person(firstName, lastName);
    }

    public static int PersonCount => s_peopleCount;
}
