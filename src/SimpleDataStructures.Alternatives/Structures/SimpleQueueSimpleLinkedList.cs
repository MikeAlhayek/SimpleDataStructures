namespace SimpleDataStructures.Structures;

public class SimpleQueueSimpleLinkedList<T>
{
    private readonly static IndexOutOfRangeException _emptyQueue = new("The queue is empty.");
    private readonly SimpleLinkedList<T> _list;

    public SimpleQueueSimpleLinkedList()
    {
        _list = new SimpleLinkedList<T>();
    }

    public SimpleQueueSimpleLinkedList(T[] items)
    {
        _list = new SimpleLinkedList<T>();

        for (var i = 0; i < items.Length; i++)
        {
            _list.InsertNext(items[i]);
        }
    }

    public T Enqueue(T value)
    {
        _list.InsertNext(value);

        return value;
    }

    public T? Dequeue()
    {
        var firstNode = _list.GetFirstOrDefault() ?? throw _emptyQueue;

        // Remove first available item;
        _list.Remove(firstNode);

        return firstNode.Value;
    }

    public T? Peek()
    {
        var lastNode = _list.GetFirstOrDefault() ?? throw _emptyQueue;

        return lastNode.Value;
    }

    public T? Rear()
    {
        var lastNode = _list.GetLastOrDefault() ?? throw _emptyQueue;

        return lastNode.Value;
    }

    public int Size()
        => _list.Count;

    public bool Contains(T value)
        => _list.ValueExists(value);
}
