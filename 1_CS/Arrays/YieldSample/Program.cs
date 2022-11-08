HelloWorld();
MusicTitles();

GameMoves game = new();

IEnumerator<IEnumerator> enumerator = game.Cross();
while (enumerator.MoveNext())
{
    enumerator = enumerator.Current as IEnumerator<IEnumerator> ?? throw new InvalidOperationException();
}


void MusicTitles()
{
    MusicTitles titles = new();
    foreach (var title in titles)
    {
        Console.WriteLine(title);
    }
    Console.WriteLine();

    Console.WriteLine("reverse");
    foreach (var title in titles.Reverse())
    {
        Console.WriteLine(title);
    }
    Console.WriteLine();

    Console.WriteLine("subset");
    foreach (var title in titles.Subset(2, 2))
    {
        Console.WriteLine(title);
    }
}

void HelloWorld()
{
    HelloCollection helloCollection = new();
    foreach (string s in helloCollection)
    {
        Console.WriteLine(s);
    }
}

class HelloCollection
{
    public IEnumerator<string> GetEnumerator()
    {
        yield return "Hello";
        yield return "World";
    }
}
