

namespace SimpleDataStructures.Structures;


public class SimpleStack<T>
{
    private readonly static IndexOutOfRangeException _emptyStack = new("The stack is empty.");
    private readonly SimpleLinkedList<T> _list;

    public SimpleStack()
    {
        _list = new SimpleLinkedList<T>();
    }

    public SimpleStack(T[] items)
    {
        _list = new SimpleLinkedList<T>();

        for (var i = 0; i < items.Length; i++)
        {
            _list.InsertNext(items[i]);
        }
    }

    public T Push(T value)
    {
        _list.InsertNext(value);

        return value;
    }

    public T? Pop()
    {
        var lastNode = _list.GetLastOrDefault() ?? throw _emptyStack;

        // Remove last available item;
        _list.Remove(lastNode);

        return lastNode.Value;
    }

    public T? Peek()
    {
        var lastNode = _list.GetLastOrDefault() ?? throw _emptyStack;

        return lastNode.Value;
    }

    public bool Contains(T value)
        => _list.ValueExists(value);

    public int Size()
        => _list.Count;
}
