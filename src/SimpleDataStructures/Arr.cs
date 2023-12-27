using System.Numerics;
using SimpleDataStructures.Structures;

namespace SimpleDataStructures;

public class Arr
{
    /// <summary>
    /// The absolute max items an array can host.
    /// </summary>
    public const int MaxCapacity = 2147483591;

    /// <summary>
    /// Finds the first occurrence of the given item with in the given array.
    /// </summary>
    /// <param name="items">The array to search.</param>
    /// <param name="item">The item to locate.</param>
    /// <returns>Returns the index of the first occurrence of a given value in an array.
    /// Returns -1 when no items were found.</returns>
    public static int IndexOf<T>(T?[] items, T? item) where T : IEquatable<T?>
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        if (item == null)
        {
            return -1;
        }

        // Apply linear search starting from the beginning of the array to find an item that corresponds to the given item.
        for (var i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i]!.Equals(item))
            {
                // At this point we found the first item that matches the given item.
                // Return the index.
                return i;
            }
        }

        // If we got this far, no item is found in the array.
        return -1;
    }

    /// <summary>
    /// Finds the last occurrence of the given item with in the given array.
    /// </summary>
    /// <param name="items">The array to search.</param>
    /// <param name="item">The item to locate.</param>
    /// <returns>Returns the index of the last occurrence of a given value in an array.
    /// Returns -1 when no items were found.</returns>
    public static int LastIndexOf<T>(T?[] items, T? value) where T : IEquatable<T?>
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        // Apply linear search starting from the ending of the array to find an item that corresponds to the given item.
        for (var i = items.Length - 1; i > 0; i--)
        {
            if (items[i] != null && items[i]!.Equals(value))
            {
                // At this point we found the first item that matches the given item.
                // Return the index.
                return i;
            }
        }

        // If we got this far, no item is found in the array.
        return -1;
    }


    /// <summary>
    /// Finds all occurrences of the given item with in the given array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The array to search.</param>
    /// <param name="values">The items to locate.</param>
    /// <returns>Returns an array of indexes where each occurrence was found of the given array.
    /// Returns -1 when no items were found.</returns>
    public static int[] IndexOfAll<T>(T[] items, T[] values) where T : IEquatable<T?>
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));
        ArgumentNullException.ThrowIfNull(values, nameof(values));

        var table = new SimpleHashTable<T>(values);

        var simpleList = new SimpleArrayList<int>(capacity: table.Count);

        for (var i = 0; i < items.Length; i++)
        {
            if (!table.Contains(items[i]))
            {
                continue;
            }

            simpleList.Add(i);
        }

        return simpleList.AsArray();
    }

    public static T?[] Trim<T>(T[] items, int newCapacity)
    {
        var newItems = new T?[newCapacity];

        var length = Math.Min(items.Length, newCapacity);

        Array.Copy(items, newItems, length);

        return newItems;
    }

    /// <summary>
    /// Tries to locate the given item in the items array.
    /// From the name, this method does binary search so the items array MUST be sorted.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="locate"></param>
    /// <returns></returns>
    public static int BinarySearch<T>(T[] items, T locate) where T : IComparable<T?>
    {
        var startIndex = 0;
        var lastIndex = items.Length - 1;

        while (startIndex <= lastIndex)
        {
            var mid = (startIndex + lastIndex) / 2;
            var comparison = items[mid].CompareTo(locate);

            if (comparison == 0)
            {
                return mid;
            }

            if (comparison < 0)
            {
                startIndex = mid + 1;
            }
            else
            {
                lastIndex = mid - 1;
            }
        }

        return -1;
    }

    /// <summary>
    /// This methods sorts the given items using a very expense sort algorithm called bubble.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    public static void BubbleSort<T>(T[] items) where T : IComparable<T?>
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        if (items.Length <= 1)
        {
            return;
        }

        for (var x = 0; x < items.Length; x++)
        {
            for (var y = 0; y < items.Length - 1; y++)
            {
                if (items[y].CompareTo(items[y + 1]) > 0)
                {
                    // At this point, we know that the left value is larger that the right value,
                    // Let's swap the values.

                    var temp = items[y + 1];

                    items[y + 1] = items[y];

                    items[y] = temp;
                }
            }
        }
    }

    public static T[] Distinct<T>(T[] items) where T : IEquatable<T?>
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        if (items.Length == 0)
        {
            return items;
        }

        var hashTable = new SimpleHashTable<T>();

        for (var i = 0; i < items.Length; i++)
        {
            hashTable.Add(items[i]);
        }

        return hashTable.AsArray()!;
    }

    public static void ForEach<T>(T[] items, Action<int, T?> callback)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        for (var i = 0; i < items.Length; i++)
        {
            callback(i, items[i]);
        }
    }

    public static T[] Reduce<T>(T[] items, Func<int, T?, bool> callback) where T : IEquatable<T?>
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        var list = new SimpleList<T>();

        for (var i = 0; i < items.Length; i++)
        {
            if (!callback(i, items[i]))
            {
                continue;
            }

            list.Add(items[i]);
        }

        return list.AsArray()!;
    }

    public static T[] Range<T>(T min, T max) where T : struct, INumber<T>, IConvertible
    {
        if (min >= max)
        {
            throw new OverflowException($"The given value of {nameof(min)} must be greater than the value of {nameof(max)}.");
        }

        var size = GetRangeSize(min, max);

        if (size >= MaxCapacity)
        {
            throw new OverflowException();
        }

        var items = new T[size + 1];
        var index = 0;
        for (var i = min; i <= max; i++)
        {
            items[index++] = i;
        }

        return items;
    }

    private static int GetRangeSize<T>(T min, T max) where T : struct, INumber<T>, IConvertible
    {
        if (T.IsZero(min) || T.IsPositive(min))
        {
            return Convert.ToInt32(max - min);
        }

        if (T.IsNegative(min) && T.IsNegative(max))
        {
            return Convert.ToInt32(T.Abs(min - max));
        }

        // This could throw an OverflowException when the range is too large for an array to handle.
        return Convert.ToInt32(T.Abs(min) + max);
    }
}
