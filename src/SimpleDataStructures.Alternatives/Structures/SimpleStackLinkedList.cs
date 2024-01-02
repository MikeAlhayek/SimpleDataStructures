namespace SimpleDataStructures.Structures;

public class SimpleStackLinkedList<T>
{
    private readonly static IndexOutOfRangeException _emptyStack = new("The stack is empty.");
    private readonly LinkedList<T> _list;

    public SimpleStackLinkedList()
    {
        _list = new LinkedList<T>();
    }

    public SimpleStackLinkedList(T[] items)
    {
        _list = new LinkedList<T>();

        for (var i = 0; i < items.Length; i++)
        {
            _list.AddLast(items[i]);
        }
    }

    public T Push(T value)
    {
        _list.AddLast(value);

        return value;
    }

    public T? Pop()
    {
        var lastNode = _list.LastOrDefault() ?? throw _emptyStack;

        // Remove last available item;
        _list.Remove(lastNode);

        return lastNode;
    }

    public T? Peek()
    {
        var lastNode = _list.LastOrDefault() ?? throw _emptyStack;

        return lastNode;
    }

    public int Size()
        => _list.Count;
}
