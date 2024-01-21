namespace SimpleDataStructures.Models;

public class SimpleBinaryTreeNode<T> where T : IComparable<T?>
{
    public T Value { get; internal set; }

    public SimpleBinaryTreeNode<T>? LeftChild { get; set; }

    public SimpleBinaryTreeNode<T>? RightChild { get; set; }

    public SimpleBinaryTreeNode<T>? Parent { get; set; }

    public SimpleBinaryTreeNode(T value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        Value = value;
    }

    public bool IsLeaf()
        => LeftChild is null && RightChild is null;

    public bool HasOneChild()
        => (LeftChild is not null || RightChild is not null)
        && !(LeftChild is not null && RightChild is not null);

    public override bool Equals(object? obj)
    {
        var other = obj as SimpleBinaryTreeNode<T>;

        if (other == null && Value == null)
        {
            return true;
        }

        return Value != null && Value.Equals(other!.Value);
    }

    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }
}
