namespace SimpleDataStructures.Structures;

internal class SimpleHashBucket<T>(int key)
{
    public int Key = key;

    internal SimpleLinkedList<SimpleHashBucketNode<T>> Items = new();

    public void Clear()
    {
        Items.Clear();
    }

    public int Count
        => Items.Count;

    public bool Add(T item, int hash)
    {
        var node = new SimpleHashBucketNode<T>(item, hash);

        return Add(node);
    }

    public bool Add(SimpleHashBucketNode<T> node)
    {
        if (Items.Contains(node))
        {
            return false;
        }

        Items.InsertNext(node);

        return true;
    }

    public bool Remove(T item)
    {
        if (item is null)
        {
            return false;
        }

        var locate = new SimpleHashBucketNode<T>(item, item!.GetHashCode());

        return Remove(locate);
    }

    public bool Remove(SimpleHashBucketNode<T> locate)
    {
        var node = Items.Find(locate);

        if (node is null)
        {
            return false;
        }

        Items.Remove(node);

        return true;
    }

    public bool Contains(SimpleHashBucketNode<T> locate)
    {

        return Items.Contains(locate);
    }
}
