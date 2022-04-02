using PriorityQueueSample;

DocumentManager dm = new();
dm.AddDocument(new Document("one", "Sample", 8));
dm.AddDocument(new Document("two", "Sample", 3));
dm.AddDocument(new Document("three", "Sample", 4));
dm.AddDocument(new Document("four", "Sample", 8));
dm.AddDocument(new Document("five", "Sample", 1));
dm.AddDocument(new Document("six", "Sample", 9));
dm.AddDocument(new Document("seven", "Sample", 1));
dm.AddDocument(new Document("eight", "Sample", 1));

Console.WriteLine("All documents unordered");
dm.DisplayAllNodesUnordered();
Console.WriteLine();

Console.WriteLine("dequeue all documents from the queue - ordered by priority");
Console.WriteLine("Elements with the lowest priority are dequeued first");
dm.Run();
