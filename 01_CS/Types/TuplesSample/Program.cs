using System;

IntroTuples();
TuplesDeconstruction();
ReturningTuples();

void ReturningTuples()
{
    (int result, int remainder) = Divide(7, 2);
    Console.WriteLine($"7 / 2 - result: {result}, remainder: {remainder}");
}


void IntroTuples()
{
    // define the tuple members and use a tuple literal
    (string AString, int Number, Book Book) tuple = ("magic", 42, new Book("Professional C#", "Wrox Press"));
    Console.WriteLine($"a string: {tuple.AString}, number: {tuple.Number}, book: {tuple.Book}");

    // use the ValueTuple type
    var tuple2 = ("magic", 42, new Book("Professional C#", "Wrox Press"));
    Console.WriteLine($"aString: {tuple2.Item1}, number: {tuple2.Item2}, book: {tuple2.Item3}");

    // assign member names in the tuple literal
    var tuple3 = (AString: "magic", Number: 42, Book: new Book("Professional C#", "Wrox Press"));
    Console.WriteLine($"aString: {tuple3.AString}, number: {tuple3.Number}, book: {tuple3.Book}");

    // assign tuple to different names
    (string S, int N, Book B) tuple4 = tuple3;

    // inferring tuple names:
    Book book = new("Professional C#", "Wrox Press");
    var tuple5 = (ANumber: 42, book.Title);
    Console.WriteLine(tuple5.Title);
}

void TuplesDeconstruction()
{
    var tuple1 = (AString: "magic", Number: 42, Book: new Book("Professional C#", "Wrox Press"));
    (string aString, int number, Book book) = tuple1;

    Console.WriteLine($"a string: {aString}, number: {number}, book: {book}");

    // use discard
    (_, _, var book1) = tuple1;
    Console.WriteLine(book1.Title);

}

(int result, int remainder) Divide(int dividend, int divisor)
{
    int result = dividend / divisor;
    int remainder = dividend % divisor;
    return (result, remainder);
}


public record Book(string Title, string Publisher);


