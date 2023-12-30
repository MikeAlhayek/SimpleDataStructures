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

    public override bool Equals(object? obj)
    {
        var node = obj as SimpleLinkedListNode<T>;

        if (node == null)
        {
            return false;
        }

        return Value!.Equals(node.Value) && Previous == node.Previous && Next == node.Next;
    }

    public override int GetHashCode()
    {
        var code = Value!.GetHashCode();

        if (Previous != null)
        {
            code += Previous.GetHashCode();
        }

        if (Next != null)
        {
            code += Next.GetHashCode();
        }

        return code;
    }
}
