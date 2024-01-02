using BenchmarkDotNet.Attributes;
using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Benchmarks.Markers;

[MemoryDiagnoser]
public class StackBenchmark
{
    private static readonly int[] _data = Arr.Range(1, 100_000);

    [Benchmark(Baseline = true)]
    public void Push()
    {
        var stack = new SimpleStack<int>();

        for (var i = 0; i < _data.Length; i++)
        {
            stack.Push(_data[i]);
        }
    }

    [Benchmark]
    public void PushUsingSimpleLinkedList()
    {
        var stack = new SimpleStackSimpleLinkedList<int>();

        for (var i = 0; i < _data.Length; i++)
        {
            stack.Push(_data[i]);
        }
    }

    [Benchmark]
    public void PushUsingLinkedList()
    {
        var stack = new SimpleStackLinkedList<int>();

        for (var i = 0; i < _data.Length; i++)
        {
            stack.Push(_data[i]);
        }
    }
}
