public interface ITitle
{
    string Title { get; }
}

public class LinkedList<T> : IEnumerable<T>
    where T : ITitle
{
    public LinkedListNode<T>? First { get; private set; }
    public LinkedListNode<T>? Last { get; private set; }
    public LinkedListNode<T> AddLast(T node)
    {
        LinkedListNode<T> newNode = new(node);
        if (First is null || Last is null)
        {
            First = newNode;
            Last = First;
        }
        else
        {
            newNode.Prev = Last;
            LinkedListNode<T> previous = Last;
            Last.Next = newNode;
            Last = newNode;
        }
        return newNode;
    }

    public IEnumerator<T> GetEnumerator()
    {
        LinkedListNode<T>? current = First;
        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void DisplayAllTitles()
    {
        foreach (T item in this)
        {
            Console.WriteLine(item.Title);
        }
    }
}

