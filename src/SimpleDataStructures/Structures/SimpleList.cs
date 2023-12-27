namespace SimpleDataStructures.Structures;

public class SimpleList<T> : SimpleArrayList<T> where T : IEquatable<T?>
{
    public SimpleList() : base()
    {
    }

    public SimpleList(int capacity) : base(capacity)
    {
    }

    public SimpleList(T?[] items)
        : base(items)
    {
    }

    public SimpleList<T> AddRange(T[] items)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        ExpandCapacityIfNeeded(items.Length);

        foreach (var item in items)
        {
            _items![++_nextAvailableIndex] = item;
        }

        return this;
    }

    public int IndexOf(T? item)
        => Arr.IndexOf(_items, item);

    public int LastIndexOf(T? item)
        => Arr.LastIndexOf(_items, item);

    public int[] IndexOfAll(T[] items)
        => Arr.IndexOfAll(_items!, items);

    public void ForEach(Action<int, T?> callback)
        => Arr.ForEach(_items!, callback);

    public SimpleList<T> Where(Func<int, T?, bool> callback)
    {
        var items = Arr.Reduce(_items!, callback);

        return new SimpleList<T>(items);
    }

    public SimpleList<T> Remove(T item)
    {
        var updated = new T?[_capacity];
        var index = 0;
        for (var i = 0; i < _items!.Length; i++)
        {
            if (_items[i] != null && _items[i]!.Equals(item))
            {
                continue;
            }

            updated[index++] = _items[i];
        }

        _items = updated;
        _nextAvailableIndex = index + 1;

        return this;
    }

    public bool Contains(T item)
    {
        for (var i = 0; i < _items!.Length; i++)
        {
            if (_items[i] != null && _items[i]!.Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    public SimpleList<T> RemoveRange(T[] items)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        var updated = new T?[_capacity];
        var index = 0;
        var removed = false;

        // get all indexes that should be removed.
        // get all the items that are not in the index.

        for (var x = 0; x < _items!.Length; x++)
        {
            var item = _items![x];
            for (var y = 0; y < items.Length; y++)
            {
                var removeItem = items[y];
                if (item != null && item.Equals(removeItem))
                {
                    removed = true;
                    break;
                }
            }
            var s = new LinkedList<int>();
            if (removed)
            {
                removed = false;
                continue;
            }

            updated[index++] = item;
        }

        _items = updated;
        _nextAvailableIndex = index;

        return this;
    }
}
