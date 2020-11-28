using System;
using System.Collections.Generic;

public class PriorityDocumentManager
{
    private readonly LinkedList<Document> _documentList = new();

    // priorities 0.9
    private readonly LinkedListNode<Document>[] _priorityNodes = new LinkedListNode<Document>[10];

    public void AddDocument(Document d) =>
        AddDocumentToPriorityNode(d, d.Priority);

    private void AddDocumentToPriorityNode(Document doc, int priority)
    {
        if (priority > 9 || priority < 0)
            throw new ArgumentException("Priority must be between 0 and 9");

        LinkedListNode<Document>? prioNode = _priorityNodes[priority];

        if (prioNode == null)
        {
            --priority;
            if (priority >= 0)
            {
                // check for the next lower priority
                AddDocumentToPriorityNode(doc, priority);
            }
            else // now no priority node exists with the same priority or lower
                 // add the new document to the end
            {
                _documentList.AddLast(doc);
                LinkedListNode<Document> last = _documentList.Last!;  // just added one document, so there's a last one
                _priorityNodes[doc.Priority] = last;
            }
            return;
        }
        else // a priority node exists
        {
            if (priority == doc.Priority)
            // priority node with the same priority exists
            {
                _documentList.AddAfter(prioNode, doc);

                // set the priority node to the last document with the same priority
                _priorityNodes[doc.Priority] = prioNode.Next ?? throw new InvalidOperationException();
            }
            else // only priority node with a lower priority exists
            {
                // get the first node of the lower priority
                var firstPrioNode = prioNode;

                while (firstPrioNode != null &&
                    firstPrioNode.Previous != null &&
                    firstPrioNode.Previous.Value.Priority == prioNode!.Value.Priority)
                {
                    firstPrioNode = prioNode.Previous;
                    prioNode = firstPrioNode;
                }

                if (firstPrioNode is null) throw new InvalidOperationException();
                _documentList.AddBefore(firstPrioNode, doc);

                if (firstPrioNode.Previous is null) throw new InvalidOperationException();
                // set the priority node to the new value
                _priorityNodes[doc.Priority] = firstPrioNode.Previous;
            }
        }
    }

    public void DisplayAllNodes()
    {
        foreach (Document doc in _documentList)
        {
            Console.WriteLine($"priority: {doc.Priority}, title {doc.Title}");
        }
    }

    // returns the document with the highest priority
    // (that's first in the linked list)
    public Document GetDocument()
    {
        Document doc = _documentList.First?.Value ?? throw new InvalidOperationException();
        _documentList.RemoveFirst();
        return doc;
    }
}
