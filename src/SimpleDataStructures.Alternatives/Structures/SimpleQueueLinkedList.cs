namespace SimpleDataStructures.Structures;

public class SimpleQueueLinkedList<T>
{
    private readonly static IndexOutOfRangeException _emptyQueue = new("The queue is empty.");
    private readonly LinkedList<T> _list;

    public SimpleQueueLinkedList()
    {
        _list = new LinkedList<T>();
    }

    public SimpleQueueLinkedList(T[] items)
    {
        _list = new LinkedList<T>();

        for (var i = 0; i < items.Length; i++)
        {
            _list.AddLast(items[i]);
        }
    }

    public T Enqueue(T value)
    {
        _list.AddLast(value);

        return value;
    }

    public T? Dequeue()
    {
        var firstNode = _list.FirstOrDefault() ?? throw _emptyQueue;

        // Remove first available item;
        _list.Remove(firstNode);

        return firstNode;
    }

    public T? Peek()
    {
        var lastNode = _list.FirstOrDefault() ?? throw _emptyQueue;

        return lastNode;
    }

    public T? Rear()
    {
        var lastNode = _list.LastOrDefault() ?? throw _emptyQueue;

        return lastNode;
    }

    public int Size()
        => _list.Count;
}
