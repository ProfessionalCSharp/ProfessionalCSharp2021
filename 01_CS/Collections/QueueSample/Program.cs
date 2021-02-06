using System;
using System.Threading.Tasks;

DocumentManager dm = new();

Task processDocuments = ProcessDocuments.StartAsync(dm);

// Create documents and add them to the DocumentManager
Random random = new();
for (int i = 0; i < 1000; i++)
{
    var doc = new Document($"Doc {i}", "content");
    int queueSize = dm.AddDocument(doc);
    Console.WriteLine($"Added document {doc.Title}, queue size: {queueSize}");
    await Task.Delay(random.Next(20));
}
Console.WriteLine($"finished adding documents");
await processDocuments;
Console.WriteLine("bye!");
