namespace SimpleDataStructures.Structures;

public class SimpleArrayList
{
    public const int DefaultCapacity = 12;
}

public class SimpleArrayList<T>
{
    protected T?[] Items;
    private int _absoluteMaxCapacity;

    public int Capacity { get; protected set; }
    protected int NextAvailableIndex;

    public SimpleArrayList()
        : this(SimpleArrayList.DefaultCapacity, Arr.MaxCapacity)
    {
    }

    public SimpleArrayList(int capacity)
        : this(capacity, Arr.MaxCapacity)
    {
    }

    public SimpleArrayList(int capacity, int absoluteMaxCapacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1, nameof(capacity));
        ArgumentOutOfRangeException.ThrowIfLessThan(absoluteMaxCapacity, 1, nameof(absoluteMaxCapacity));

        Items = new T?[capacity];
        Capacity = capacity;
        NextAvailableIndex = 0;

        _absoluteMaxCapacity = absoluteMaxCapacity;
    }

    public SimpleArrayList(T?[] items)
        : this(items, Arr.MaxCapacity)
    {
    }

    public SimpleArrayList(T?[] items, int absoluteMaxCapacity)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));
        ArgumentOutOfRangeException.ThrowIfLessThan(absoluteMaxCapacity, 1, nameof(absoluteMaxCapacity));

        Items = items;
        Capacity = items.Length;
        NextAvailableIndex = items.Length;
        _absoluteMaxCapacity = absoluteMaxCapacity;
    }

    public T? this[int index]
    {
        get => Items![index];
        set => Items![index] = value;
    }

    public T? RemoveAt(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, NextAvailableIndex, nameof(index));

        if (index == 0)
        {
            // Remove from the beginning.
            var temp = new T?[Capacity];

            var firstValue = Items[0];

            // Alternately, we can iterate over all the items and shift them down by one.
            Array.Copy(Items, 1, temp, 0, --NextAvailableIndex);

            Items = temp;

            return firstValue;
        }

        if (index == NextAvailableIndex - 1)
        {
            // Remove from the end.
            var lastValue = Items[--NextAvailableIndex];

            Items[NextAvailableIndex] = default;

            return lastValue;
        }

        var value = Items[index];

        // At this point, we need to remove from the middle.
        for (var i = index; i < NextAvailableIndex - 1; i++)
        {
            Items[i] = Items[i + 1];
        }

        // Clear the value fro  m the latest available slot.
        Items[--NextAvailableIndex] = default;

        return value;
    }

    public SimpleArrayList<T> Add(T item)
    {
        ExpandCapacityIfNeeded(1);

        Items![NextAvailableIndex++] = item;

        return this;
    }

    public T?[] AsArray()
    {
        if (NextAvailableIndex == Capacity)
        {
            return Items!;
        }

        return Arr.Trim(Items!, NextAvailableIndex);
    }

    public bool Any(Func<T?, bool> predicate)
    {
        for (var i = 0; i < NextAvailableIndex; i++)
        {
            if (!predicate(Items[i]))
            {
                continue;
            }

            return true;
        }

        return false;
    }

    public int IndexOf(T? item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (item == null)
        {
            return -1;
        }

        // Apply linear search starting from the beginning of the array to find an item that corresponds to the given item.
        for (var i = 0; i < NextAvailableIndex; i++)
        {
            if (Items[i]!.Equals(item))
            {
                // At this point we found the first item that matches the given item.
                // Return the index.
                return i;
            }
        }

        // If we got this far, no item is found in the array.
        return -1;
    }

    public int LastIndexOf(T? item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (item == null)
        {
            return -1;
        }

        // Apply linear search starting from the beginning of the array to find an item that corresponds to the given item.
        for (var i = NextAvailableIndex - 1; i >= 0; i--)
        {
            if (Items[i]!.Equals(item))
            {
                // At this point we found the first item that matches the given item.
                // Return the index.
                return i;
            }
        }

        // If we got this far, no item is found in the array.
        return -1;
    }

    public void Clear()
    {
        Items = [];
        Capacity = 0;
        NextAvailableIndex = 0;
    }

    public int Count
        => NextAvailableIndex;


    protected void ExpandCapacityIfNeeded(int totalToAdd)
    {
        var availableSpace = Capacity - NextAvailableIndex;

        if (availableSpace >= totalToAdd)
        {
            // The array still have sufficient space.
            return;
        }

        if (_absoluteMaxCapacity - NextAvailableIndex < totalToAdd)
        {
            throw new OutOfMemoryException("The list have reached it's limit! Unable to expand it any further.");
        }

        // Expanding the array is a costly operation. Doubling the size, with the hope that further expansion won't be necessary.
        var requiredCapacity = Items!.Length + totalToAdd;

        lock (Items)
        {
            Capacity = GetNewProjectedCapacity(requiredCapacity);

            var temp = new T?[Capacity];

            Array.Copy(Items, temp, Items.Length);

            Items = temp;
        }
    }

    private int GetNewProjectedCapacity(int requiredCapacity)
    {
        var factor = (requiredCapacity / Capacity) + 1;

        if (factor < 2)
        {
            return Math.Min(Items!.Length * 2, _absoluteMaxCapacity);
        }

        return Math.Min(factor * Capacity, _absoluteMaxCapacity);
    }
}
