

namespace SimpleDataStructures.Structures;

public class SimpleQueue<T>
{
    private readonly static IndexOutOfRangeException _emptyQueue = new("The queue is empty.");
    private readonly SimpleArrayList<T> _items;

    public SimpleQueue()
    {
        _items = new SimpleArrayList<T>();
    }

    public SimpleQueue(int capacity)
    {
        _items = new SimpleArrayList<T>(capacity);
    }

    public SimpleQueue(T[] items)
    {
        _items = new SimpleArrayList<T>(items);
    }

    public T Enqueue(T value)
    {
        _items.Add(value);

        return value;
    }

    public T? Dequeue()
    {
        if (_items.IsEmpty())
        {
            throw _emptyQueue;
        }

        // Remove first available item;
        return _items.RemoveFirst();
    }

    public T? Peek()
    {
        if (_items.IsEmpty())
        {
            throw _emptyQueue;
        }

        return _items.First();
    }

    public int Size()
        => _items.Count;

    public bool Contains(T value)
        => _items.Contains(value);
}
