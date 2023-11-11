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

if (list.First is not null)
{
    IterateUsingNext(list.First);
}

// this needs to iterate through the list until the document is found
list.Remove(doc4);

Console.WriteLine("after removal");
foreach (var item in list)
{
    Console.WriteLine(item);
}

static void IterateUsingNext(LinkedListNode<Document> start)
{
    if (start.Value is null) 
        return;
    LinkedListNode<Document>? current = start;
    do
    {
        Console.WriteLine(current.Value);
        current = current.Next;
    } while (current is not null);
}

record Document(int Id, string Text);
