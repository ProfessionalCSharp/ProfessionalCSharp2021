using System;
using Wrox.ProCSharp.Generics;

var dm = new DocumentManager<Document>();
dm.AddDocument(new Document("Title A", "Sample A"));
dm.AddDocument(new Document("Title B", "Sample B"));

dm.DisplayAllDocuments();

if (dm.IsDocumentAvailable)
{
    Document d = dm.GetDocument();
    Console.WriteLine(d.Content);
}
