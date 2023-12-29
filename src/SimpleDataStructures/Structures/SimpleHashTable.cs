namespace SimpleDataStructures.Structures;

public class SimpleHashTable<T>
{
    private const double _extendFactor = 1.3;
    private const int _defaultSize = 17;

    private readonly int _bucketCapacity;

    /// <summary>
    /// Represents the total items available is all buckets.
    /// Anytime there is a collision, a bucket will have more than one item.
    /// </summary>
    private int _totalItems = 0;

    /// <summary>
    /// List of indexes in _bucketsTable that are not null.
    /// </summary>
    private SimpleArrayList<int> _bucketsIndexes;

    private SimpleArrayList<SimpleHashBucket> _bucketsTable;

    public SimpleHashTable(HashTableOptions? options = null)
         : this(_defaultSize, options)
    {
    }

    public SimpleHashTable(int capacity, HashTableOptions? options = null)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        _bucketCapacity = GetBucketCapacity(options);
        _bucketsIndexes = new SimpleArrayList<int>(capacity);
        _bucketsTable = new(capacity);
    }

    public SimpleHashTable(T?[] items, HashTableOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(items);

        var capacity = items.Length > 0 ? Num.GetNextPrimeNumber(items.Length) : _defaultSize;
        _bucketCapacity = GetBucketCapacity(options);
        _bucketsIndexes = new SimpleArrayList<int>(capacity);
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
            _bucketsIndexes.Add(index);
            _totalItems++;
            return true;
        }

        if (_bucketsTable[index]!.IsFull)
        {
            // Rehash the table then repeat.
            RehashTable(_bucketsTable, SimpleHashTable<T>.GetNextSize(_bucketsTable.Capacity));

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
            _bucketsIndexes.RemoveAt(index);
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
        => _totalItems;

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

        var itemIndex = 0;

        for (var i = 0; i < _bucketsIndexes.Count; i++)
        {
            var bucketIndex = _bucketsIndexes[i];

            for (var x = 0; x < _bucketsTable[bucketIndex]!.Length; x++)
            {
                items[itemIndex++] = _bucketsTable[bucketIndex]![x];
            }
        }

        return items;
    }

    public void Clear()
    {
        _bucketsTable.Clear();
        _bucketsTable = new SimpleArrayList<SimpleHashBucket>(_defaultSize);
    }

    private void RehashTable(SimpleArrayList<SimpleHashBucket> existing, int nextSize)
    {
        var table = new SimpleArrayList<SimpleHashBucket>(nextSize);
        var indexes = new SimpleArrayList<int>(nextSize);
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

                    indexes.Add(index);
                    size++;

                    continue;
                }

                if (table[index]!.IsFull)
                {
                    var sizeAfterNext = table.Count;

                    // Clear table after rehash to free up the unwanted data.
                    table.Clear();

                    RehashTable(existing, SimpleHashTable<T>.GetNextSize(sizeAfterNext));

                    return;
                }

                table[index]!.Add(existing[i]![x]!);
            }
        }

        _bucketsTable = table;
        _bucketsIndexes = indexes;
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
