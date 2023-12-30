namespace SimpleDataStructures.Models;

public class SimpleLinkedListNode<T>
{
    public T? Value { get; set; }

    public SimpleLinkedListNode<T>? Previous { get; internal set; }

    public SimpleLinkedListNode<T>? Next { get; internal set; }

    public SimpleLinkedListNode()
    {
    }

    public SimpleLinkedListNode(T value)
    {
        Value = value;
    }
}
