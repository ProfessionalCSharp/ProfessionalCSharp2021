public class ProcessDocuments
{
    public static Task StartAsync(DocumentManager dm) =>
        Task.Run(new ProcessDocuments(dm).RunAsync);

    protected ProcessDocuments(DocumentManager dm) => 
        _documentManager = dm ?? throw new ArgumentNullException(nameof(dm));

    private readonly DocumentManager _documentManager;

    protected async Task RunAsync()
    {
        Random random = new();
        Stopwatch stopwatch = new();
        stopwatch.Start();
        bool stop = false;
        do
        {
            if (stopwatch.Elapsed >= TimeSpan.FromSeconds(5))
            {
                stop = true;
            }
            if (_documentManager.IsDocumentAvailable)
            {
                stopwatch.Restart();
                Document doc = _documentManager.GetDocument();
                Console.WriteLine($"Processing document {doc.Title}");
            }
            await Task.Delay(random.Next(20));  // wait a random time before processing the next document
        } while (!stop) ;
        Console.WriteLine("stopped reading documents");
    }
}