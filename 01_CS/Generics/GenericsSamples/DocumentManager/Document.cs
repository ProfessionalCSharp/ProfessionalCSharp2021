
namespace Wrox.ProCSharp.Generics
{
    public interface IDocument
    {
        string Title { get; }
        string Content { get; }
    }

    public record Document(string Title, string Content) : IDocument
    {
    }
}
