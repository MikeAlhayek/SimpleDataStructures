namespace SimpleDataStructures.Structures;

internal class SimpleHashBucketNode<TValue>(TValue value, int hashCode)
{
    internal int HashCode { get; } = hashCode;

    internal TValue Value { get; } = value;

    public override bool Equals(object? obj)
    {
        return obj is SimpleHashBucketNode<TValue> node && HashCode == node.HashCode;
    }

    public override int GetHashCode()
    {
        return HashCode;
    }
}

