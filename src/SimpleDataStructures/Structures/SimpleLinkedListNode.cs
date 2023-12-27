namespace SimpleDataStructures.Structures;

public class SimpleLinkedListNode<T> : IEquatable<SimpleLinkedListNode<T>> where T : IEquatable<T?>
{
    public T? Data { get; set; }

    public SimpleLinkedListNode<T>? Previous { get; internal set; }

    public SimpleLinkedListNode<T>? Next { get; internal set; }

    public SimpleLinkedListNode()
    {
    }

    public SimpleLinkedListNode(T? data)
    {
        Data = data;
    }

    public bool Equals(SimpleLinkedListNode<T>? other)
    {
        return this == other;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SimpleLinkedListNode<T>);
    }

    public override int GetHashCode()
    {
        if (Data == null)
        {
            return 0;
        }

        return Data!.GetHashCode();
    }
}
