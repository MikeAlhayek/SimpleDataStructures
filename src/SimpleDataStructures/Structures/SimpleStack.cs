

namespace SimpleDataStructures.Structures;

public class SimpleStack<T>
{
    private readonly static IndexOutOfRangeException _emptyStack = new("The stack is empty.");
    private readonly SimpleArrayList<T> _items;

    public SimpleStack()
    {
        _items = new SimpleArrayList<T>();
    }

    public SimpleStack(int capacity)
    {
        _items = new SimpleArrayList<T>(capacity);
    }

    public SimpleStack(T[] items)
    {
        _items = new SimpleArrayList<T>(items);
    }

    public T Push(T value)
    {
        _items.Add(value);

        return value;
    }

    public T? Pop()
    {
        if (_items.IsEmpty())
        {
            throw _emptyStack;
        }

        // Remove last available item;
        return _items.RemoveLast();
    }

    public T? Peek()
    {
        if (_items.IsEmpty())
        {
            throw _emptyStack;
        }

        return _items.Last();
    }

    public bool Contains(T value)
        => _items.Contains(value);

    public int Size()
        => _items.Count;
}
