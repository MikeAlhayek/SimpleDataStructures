namespace SimpleDataStructures.Structures;

public class SimpleList<T> : SimpleArrayList<T>
{
    public SimpleList()
        : base()
    {
    }

    public SimpleList(int capacity)
        : base(capacity)
    {
    }


    public SimpleList(int capacity, int absoluteMaxCapacity)
    : base(capacity, absoluteMaxCapacity)
    {
    }

    public SimpleList(T?[] items)
        : base(items)
    {
    }


    public SimpleList(T?[] items, int absoluteMaxCapacity)
        : base(items, absoluteMaxCapacity)
    {
    }

    public SimpleList<T> AddRange(T[] items)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        ExpandCapacityIfNeeded(items.Length);

        foreach (var item in items)
        {
            Items![++NextAvailableIndex] = item;
        }

        return this;
    }

    public int[] IndexOfAll(T[] items)
        => Arr.IndexOfAll(Items!, items);

    public void ForEach(Action<int, T?> callback)
        => Arr.ForEach(Items!, callback);

    public SimpleList<T> Where(Func<int, T?, bool> callback)
    {
        var items = Arr.Reduce(Items!, callback);

        return new SimpleList<T>(items);
    }

    public SimpleList<T> Remove(T item)
    {
        var updated = new T?[Capacity];
        var index = 0;
        for (var i = 0; i < NextAvailableIndex; i++)
        {
            if (Items[i] != null && Items[i]!.Equals(item))
            {
                continue;
            }

            updated[index++] = Items[i];
        }

        Items = updated;
        NextAvailableIndex = index + 1;

        return this;
    }

    public bool Contains(T item)
    {
        for (var i = 0; i < NextAvailableIndex; i++)
        {
            if (Items[i] != null && Items[i]!.Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    public SimpleList<T> RemoveRange(T[] items)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        var updated = new T?[Capacity];
        var index = 0;
        var removed = false;

        // get all indexes that should be removed.
        // get all the items that are not in the index.

        for (var x = 0; x < NextAvailableIndex; x++)
        {
            var item = Items![x];
            for (var y = 0; y < items.Length; y++)
            {
                var removeItem = items[y];
                if (item != null && item.Equals(removeItem))
                {
                    removed = true;
                    break;
                }
            }

            if (removed)
            {
                removed = false;
                continue;
            }

            updated[index++] = item;
        }

        Items = updated;
        NextAvailableIndex = index;

        return this;
    }
}
