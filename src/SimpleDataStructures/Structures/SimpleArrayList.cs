namespace SimpleDataStructures.Structures;

public class SimpleArrayList
{
    public const int DefaultCapacity = 12;
}

public class SimpleArrayList<T>
{
    protected T?[] _items;
    protected int _capacity;
    protected int _nextAvailableIndex;

    public SimpleArrayList()
        : this(SimpleArrayList.DefaultCapacity)
    {
    }

    public SimpleArrayList(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1, nameof(capacity));

        _items = new T?[capacity];
        _capacity = capacity;
        _nextAvailableIndex = 0;
    }

    public SimpleArrayList(T?[] items)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        _items = items;
        _capacity = items.Length;
        _nextAvailableIndex = items.Length - 1;
    }

    public T? this[int index]
    {
        get => _items![index];
        set => _items![index] = value;
    }

    public SimpleArrayList<T> Add(T item)
    {
        ExpandCapacityIfNeeded(1);

        _items![_nextAvailableIndex++] = item;

        return this;
    }

    public T?[] AsArray()
    {
        if (_nextAvailableIndex == _capacity)
        {
            return _items!;
        }

        return Arr.Trim(_items!, _nextAvailableIndex);
    }

    public bool Any(Func<T?, bool> predicate)
    {
        for (var i = 0; i < _items!.Length; i++)
        {
            if (!predicate(_items[i]))
            {
                continue;
            }

            return true;
        }

        return false;
    }

    public void Clear()
    {
        _items = [];
        _capacity = 0;
        _nextAvailableIndex = 0;
    }

    public int Count
        => _nextAvailableIndex;

    public int Capacity
        => _capacity;

    protected void ExpandCapacityIfNeeded(int totalToAdd)
    {
        var availableSpace = _capacity - _nextAvailableIndex;

        if (availableSpace >= totalToAdd)
        {
            // The array still have sufficient space.
            return;
        }

        if (Arr.MaxCapacity - _nextAvailableIndex < totalToAdd)
        {
            throw new ArgumentException("The list have reached it's limit! Unable to expand it any further.");
        }

        // Expanding the array is a costly operation. Doubling the size, with the hope that further expansion won't be necessary.
        var requiredCapacity = _items!.Length + totalToAdd;

        lock (_items)
        {
            _capacity = GetNewProjectedCapacity(requiredCapacity);

            var temp = new T?[_capacity];

            Array.Copy(_items, temp, _items.Length);

            _items = temp;
        }
    }

    private int GetNewProjectedCapacity(int requiredCapacity)
    {
        var factor = (requiredCapacity / _capacity) + 1;

        if (factor < 2)
        {
            return Math.Min(_items!.Length * 2, Arr.MaxCapacity);
        }

        return Math.Min(factor * _capacity, Arr.MaxCapacity);
    }
}
