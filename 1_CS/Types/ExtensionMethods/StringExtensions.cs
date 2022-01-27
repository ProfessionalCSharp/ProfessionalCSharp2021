
namespace ExtensionsForString;
public static class StringExtensions
{
    public static int GetWordCount(this string s) =>
        s.Split().Length;
}
