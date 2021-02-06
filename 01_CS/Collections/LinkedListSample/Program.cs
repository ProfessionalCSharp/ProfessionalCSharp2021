using System;
using System.Collections.Generic;

LinkedList<Document> list = new();
LinkedListNode<Document> first = list.AddFirst(new Document(1, "first"));
list.AddAfter(first, new Document(2, "after first"));
LinkedListNode<Document> last = list.AddLast(new Document(3, "Last"));
Document doc4 = new(4, "before last");
list.AddBefore(last, doc4);

foreach (var item in list)
{
    Console.WriteLine(item);
}

list.Remove(doc4);

Console.WriteLine("after removal");
foreach (var item in list)
{
    Console.WriteLine(item);
}

record Document(int Id, string Text);
