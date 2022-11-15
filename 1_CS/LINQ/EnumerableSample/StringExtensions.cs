namespace EnumerableSample;

public static class StringExtensions
{
    public static string FirstName(this string name)
    {
        int ix = name.LastIndexOf(' ');
        return name[..ix];
    }

    public static string LastName(this string name)
    {
        int ix = name.LastIndexOf(' ') + 1;
        return name[ix..];
    }
}
