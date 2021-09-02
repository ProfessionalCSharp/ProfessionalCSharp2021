// string s1 = null; // compiler warning - CS8600: Converting a null literal or a possible null value to non-nullable type

string ? s1 = null;
// string s2 = s1.ToUpper(); // compiler warning
string? s2 = s1?.ToUpper();
string s3 = s1?.ToUpper() ?? string.Empty;

if (s1 is not null)
{
    string s4 = s1.ToUpper();
}

if (s1 != null)
{
    string s5 = s1.ToUpper();
}

Book? b1 = null;
Book b2 = new Book("Professional C#");
string title = b2.Title;
string? publisher = b2.Publisher;


class Book
{
    public Book(string title) => Title = title;
    
    public string Title { get; set; }
    public string? Publisher { get; set; }
}
