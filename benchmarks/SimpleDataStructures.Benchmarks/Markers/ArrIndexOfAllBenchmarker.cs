using BenchmarkDotNet.Attributes;
using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Benchmarks.Markers;

public class ArrIndexOfAllBenchmarker
{
    private readonly int[] _items = Arr.Range(1, 5_000);
    private readonly int[] _locate = Arr.Range(100, 200);

    [Benchmark(Baseline = true)]
    public void IndexOfAll()
    {
        Arr.IndexOfAll(_items, _locate);
    }

    [Benchmark]
    public void IndexOfAllUsingHashTable()
    {
        IndexOfAllUsingHashTable(_items, _locate);
    }

    public int[] IndexOfAllUsingHashTable<T>(T[] items, T[] values) where T : IEquatable<T?>
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));
        ArgumentNullException.ThrowIfNull(values, nameof(values));

        // Apply linear search starting from the beginning of the array to find an item that corresponds to the given items.
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
}
