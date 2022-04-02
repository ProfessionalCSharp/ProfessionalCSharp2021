namespace PriorityQueueSample;

public record Document(string Title, string Content, int Priority);

public class DocumentManager
{
    private readonly PriorityQueue<Document, int> _documentQueue = new();

    public void AddDocument(Document doc)
    {
        _documentQueue.Enqueue(doc, doc.Priority);
    }
    
    public void DisplayAllNodesUnordered()
    {       
        foreach ((Document doc, _) in _documentQueue.UnorderedItems)
        {
            Console.WriteLine(doc);
        }
    }

    public Document GetDocument()
    {
        return _documentQueue.Dequeue();
    }

    public void Run()
    {
        while (_documentQueue.Count >= 1)
        {
            var doc = _documentQueue.Dequeue();
            Console.WriteLine(doc);
        }
    }
}
