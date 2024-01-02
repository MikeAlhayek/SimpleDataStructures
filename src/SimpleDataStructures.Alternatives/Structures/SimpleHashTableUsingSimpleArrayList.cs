namespace SimpleDataStructures.Structures.Alternatives;

public class SimpleHashTableUsingSimpleArrayList<T>
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

    private SimpleArrayList<SimpleHashBucket<T>> _bucketsTable;

    public SimpleHashTableUsingSimpleArrayList(HashTableOptions? options = null)
         : this(_defaultSize, options)
    {
    }

    public SimpleHashTableUsingSimpleArrayList(int capacity, HashTableOptions? options = null)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        _bucketCapacity = GetBucketCapacity(options);
        _bucketsIndexes = new SimpleArrayList<int>(capacity);
        _bucketsTable = new(capacity);
    }

    public SimpleHashTableUsingSimpleArrayList(T?[] items, HashTableOptions? options = null)
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
            _bucketsTable[index] = new SimpleHashBucket<T>(index);

            _bucketsTable[index]!.Add(item);
            _bucketsIndexes.Add(index);
            _totalItems++;
            return true;
        }

        if (_bucketsTable[index]!.Count == _bucketCapacity)
        {
            // The bucket have reached max capacity, 
            // Rehash the table then attempt to add again.
            RehashTable(_bucketsTable, GetNextSize(_bucketsTable.Capacity));

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

        var totalItemsInBucket = _bucketsTable[index]!.Count;

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

        for (var i = 0; i < _bucketsIndexes.Count; i++)
        {
            var bucketIndex = _bucketsIndexes[i];

            if (_bucketsTable[bucketIndex]!.Contains(item))
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

            for (var x = 0; x < _bucketsTable[bucketIndex]!.Items.Count; x++)
            {
                var value = _bucketsTable[bucketIndex]!.Items[x];

                items[itemIndex++] = value;
            }
        }

        return items;
    }

    public void Clear()
    {
        _bucketsTable.Clear();
        _bucketsTable = new SimpleArrayList<SimpleHashBucket<T>>(_defaultSize);
    }

    private void RehashTable(SimpleArrayList<SimpleHashBucket<T>> existing, int nextSize)
    {
        var table = new SimpleArrayList<SimpleHashBucket<T>>(nextSize);
        var indexes = new SimpleArrayList<int>(nextSize);

        for (var i = 0; i < _bucketsIndexes.Count; i++)
        {
            var bucketIndex = _bucketsIndexes[i];

            for (var x = 0; x < _bucketsTable[bucketIndex]!.Items.Count; x++)
            {
                var value = _bucketsTable[bucketIndex]!.Items[x];

                var index = value!.GetHashCode() % table.Capacity;

                if (table[index] is null)
                {
                    table[index] = new SimpleHashBucket<T>(index);
                    table[index]!.Add(value);

                    indexes.Add(index);

                    continue;
                }

                if (table[index]!.Count == _bucketCapacity)
                {
                    var sizeAfterNext = table.Count;

                    // Clear table after rehash to free up the unwanted data.
                    table.Clear();

                    RehashTable(existing, GetNextSize(sizeAfterNext));

                    return;
                }

                table[index]!.Add(value);
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
}

internal class SimpleHashBucket<T>(int key)
{
    public int Key = key;

    internal SimpleArrayList<T> Items = new();

    public void Clear()
    {
        Items.Clear();
    }

    public int Count
        => Items.Count;

    public bool Add(T item)
    {
        if (Items.Contains(item))
        {
            return false;
        }

        Items.Add(item);

        return true;
    }

    public bool Remove(T item)
    {
        var index = Items.IndexOf(item);

        if (index == -1)
        {
            return false;
        }

        Items.RemoveAt(index);

        return true;
    }

    public bool Contains(T locate)
    {
        return Items.Contains(locate);
    }
}
