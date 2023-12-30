using BenchmarkDotNet.Attributes;
using SimpleDataStructures.Alternatives.Structures;
using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Benchmarks.Markers;

[MemoryDiagnoser]
public class HashTableBenchmark
{
    private static readonly int[] _data = Arr.Range(1, 100_000);

    [Benchmark(Baseline = true)]
    public void AddRange()
    {
        var table = new SimpleHashTable<int>();

        for (var i = 0; i < _data.Length; i++)
        {
            table.Add(_data[i]);
        }

        table.Remove(1_000);

        table.Contains(10_000);
    }

    [Benchmark]
    public void AddRangeNoHashCache()
    {
        var table = new SimpleHashTableNoHashCache<int>();

        for (var i = 0; i < _data.Length; i++)
        {
            table.Add(_data[i]);
        }

        table.Remove(1_000);

        table.Contains(10_000);
    }
}
