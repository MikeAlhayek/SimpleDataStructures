namespace SimpleDataStructures.Structures;

public class SimpleHashTable<T> where T : IEquatable<T?>
{
    private const double _extendFactor = 1.3;
    private const int _defaultSize = 17;

    private int _bucketCapacity;

    private int _size = 0;
    private int _totalItems = 0;

    private SimpleArrayList<SimpleHashBucket> _bucketsTable;

    public SimpleHashTable(HashTableOptions? options = null)
         : this(_defaultSize, options)
    {
    }

    public SimpleHashTable(int capacity, HashTableOptions? options = null)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        _bucketCapacity = GetBucketCapacity(options);
        _bucketsTable = new(capacity);
    }

    public SimpleHashTable(T?[] items, HashTableOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(items);

        var capacity = items.Length > 0 ? Num.GetNextPrimeNumber(items.Length) : _defaultSize;
        _bucketCapacity = GetBucketCapacity(options);
        _bucketsTable = new(capacity);

        for (var i = 0; i < items.Length; i++)
        {
            Add(items![i]);
        }
    }

    public bool Add(T? item)
    {
        if (item == null)
        {
            return false;
        }

        var hash = item!.GetHashCode();
        var index = hash % _bucketsTable.Capacity;

        if (_bucketsTable[index] == null)
        {
            _bucketsTable[index] = new SimpleHashBucket(index, _bucketCapacity);

            _bucketsTable[index]!.Add(item);
            _size++;
            _totalItems++;
            return true;
        }

        if (_bucketsTable[index]!.IsFull)
        {
            // rehash the table then repeat.
            RehashTable(ref _bucketsTable, SimpleHashTable<T>.GetNextSize(_bucketsTable.Capacity));

            return Add(item);
        }

        var added = _bucketsTable[index]!.Add(item);

        if (added)
        {
            _totalItems++;
        }

        return added;
    }

    public bool Remove(T item)
    {
        if (item == null)
        {
            return false;
        }

        var hash = item.GetHashCode();
        var index = hash % _bucketsTable.Capacity;

        var totalItemsInBucket = _bucketsTable[index]!.Length;

        if (totalItemsInBucket == 1)
        {
            _bucketsTable[index]!.Clear();
            _size--;
            _totalItems--;
            return true;
        }

        // At this point we know there is at least 2 collisions, remove yes.
        var removed = _bucketsTable[index]!.Remove(item);

        if (removed)
        {
            _totalItems -= totalItemsInBucket;
        }

        return removed;
    }

    public int Count
        => _size;

    public bool Contains(T? item)
    {
        if (item == null || _totalItems == 0)
        {
            return false;
        }

        var hash = item.GetHashCode();
        var index = hash % _bucketsTable.Capacity;

        if (_bucketsTable[index] == null)
        {
            return false;
        }

        for (var i = 0; i < _bucketsTable[index]!.Length; i++)
        {
            if (_bucketsTable[index] is null)
            {
                continue;
            }

            if (_bucketsTable[index]![i] != null && _bucketsTable[index]![i]!.Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    public T?[] AsArray()
    {
        var items = new T?[_totalItems];

        var index = 0;
        for (var i = 0; i < _bucketsTable.Count; i++)
        {
            foreach (var bucket in _bucketsTable[i]!._items)
            {
                items[index++] = bucket;
            }
        }

        return items;
    }

    public void Clear()
    {
        _bucketsTable.Clear();
        _bucketsTable = new SimpleArrayList<SimpleHashBucket>(_defaultSize);
    }

    private void RehashTable(ref SimpleArrayList<SimpleHashBucket> existing, int nextSize)
    {
        var table = new SimpleArrayList<SimpleHashBucket>(nextSize);
        var size = 0;
        for (var i = 0; i < existing.Capacity; i++)
        {
            if (existing[i] == null || existing[i]!.Length == 0)
            {
                continue;
            }

            for (var x = 0; x < existing[i]!.Length; x++)
            {
                if (existing[i] == null)
                {
                    continue;
                }

                var hash = existing[i]![x]!.GetHashCode();
                var index = hash % table.Capacity;

                if (table[index] == null)
                {
                    table[index] = new SimpleHashBucket(index, _bucketCapacity);
                    table[index]!.Add(existing[i]![x]!);

                    size++;

                    continue;
                }

                if (table[index]!.IsFull)
                {
                    RehashTable(ref existing, SimpleHashTable<T>.GetNextSize(table.Count));

                    // Clear table after rehash to free up the unwanted data.
                    table.Clear();
                    return;
                }

                table[index]!.Add(existing[i]![x]!);
            }
        }

        existing.Clear();
        existing = table;
        _size = size;
    }

    private static int GetBucketCapacity(HashTableOptions? options)
    {
        if (options == null || options.TotalCollisions <= 1)
        {
            return HashTableOptions.DefaultTotalCollisions;
        }

        return options.TotalCollisions;
    }

    private static int GetNextSize(int start)
    {
        var estimate = (int)(start * _extendFactor);

        return Num.GetNextPrimeNumber(estimate);
    }

    private class SimpleHashBucket
    {
        private readonly int _capacity;

        private int _nextAvailableIndex = 0;

        public int Key;

        internal T?[] _items;

        public SimpleHashBucket(int key, int capacity)
        {
            Key = key;
            _capacity = capacity;
            _items = new T?[_capacity];
        }

        public bool IsFull
            => Length == _capacity;

        public void Clear()
        {
            _items = new T?[_capacity];
            _nextAvailableIndex = 0;
        }

        public int Length
            => _nextAvailableIndex;

        public T? this[int index]
        {
            get => _items![index];
            set => _items![index] = value;
        }

        public bool Add(T? item)
        {
            if (item is null || IsFull)
            {
                return false;
            }

            for (var i = 0; i < _nextAvailableIndex; i++)
            {
                if (_items[i]!.Equals(item))
                {
                    return false;
                }
            }

            _items[_nextAvailableIndex++] = item;

            return true;
        }

        public bool Remove(T item)
        {
            var arr = new T?[_capacity];
            var lastIndex = 0;
            var removed = false;

            for (var i = 0; i < _nextAvailableIndex; i++)
            {
                if (_items[i]!.Equals(item))
                {
                    removed = true;

                    continue;
                }

                arr[lastIndex++] = _items[i];
            }

            if (removed)
            {
                _items = arr;
                _nextAvailableIndex = lastIndex;
            }

            return removed;
        }
    }
}
