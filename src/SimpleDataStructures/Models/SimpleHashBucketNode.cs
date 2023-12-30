namespace SimpleDataStructures.Structures;

internal class SimpleHashBucketNode<T>(T value, int hashCode)
{
    internal int HashCode { get; } = hashCode;

    internal T Value { get; } = value;

    public override bool Equals(object? obj)
    {
        return obj is SimpleHashBucketNode<T> node && HashCode == node.HashCode;
    }

    public override int GetHashCode()
    {
        return HashCode;
    }
}

