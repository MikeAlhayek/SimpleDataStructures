using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Alternatives.Structures;

internal class SimpleHashBucketNoHashCache<T>(int key)
{
    public int Key = key;

    internal SimpleLinkedList<T> Items = new();

    public void Clear()
    {
        Items.Clear();
    }

    public int Count
        => Items.Count;

    public bool Add(T item)
    {
        if (Items.ValueExists(item))
        {
            return false;
        }

        Items.InsertNext(item);

        return true;
    }

    public bool Remove(T item)
    {
        if (item is null)
        {
            return false;
        }

        var node = Items.Find(item);

        if (node is null)
        {
            return false;
        }

        return Items.Remove(node) != null;
    }

    public bool Contains(T locate)
    {
        return Items.ValueExists(locate);
    }
}
