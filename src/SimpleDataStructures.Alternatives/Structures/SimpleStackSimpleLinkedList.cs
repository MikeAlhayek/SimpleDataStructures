namespace SimpleDataStructures.Structures;

public class SimpleStackSimpleLinkedList<T>
{
    private readonly static IndexOutOfRangeException _emptyStack = new("The stack is empty.");
    private readonly SimpleLinkedList<T> _list;

    public SimpleStackSimpleLinkedList()
    {
        _list = new SimpleLinkedList<T>();
    }

    public SimpleStackSimpleLinkedList(T[] items)
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

    public int Size()
        => _list.Count;
}
