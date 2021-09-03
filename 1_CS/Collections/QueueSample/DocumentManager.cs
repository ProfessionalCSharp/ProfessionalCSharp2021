public class DocumentManager
{
    private readonly object _syncQueue = new object();

    private readonly Queue<Document> _documentQueue = new();

    public int AddDocument(Document doc)
    {
        lock (_syncQueue)
        {
            _documentQueue.Enqueue(doc);
            return _documentQueue.Count;
        }
    }

    public Document GetDocument()
    {
        lock (_syncQueue)
        {
            return _documentQueue.Dequeue();
        }
    }

    public bool IsDocumentAvailable => _documentQueue.Count > 0;
}
