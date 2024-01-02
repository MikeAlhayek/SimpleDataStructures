using BenchmarkDotNet.Attributes;
using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Benchmarks.Markers;

[MemoryDiagnoser]
public class QueueBenchmark
{
    private static readonly int[] _data = Arr.Range(1, 100_000);

    [Benchmark(Baseline = true)]
    public void Enqueue()
    {
        var queue = new SimpleQueue<int>();

        for (var i = 0; i < _data.Length; i++)
        {
            queue.Enqueue(_data[i]);
        }
    }

    [Benchmark]
    public void EnqueueUsingSimpleLinkedList()
    {
        var queue = new SimpleQueueSimpleLinkedList<int>();

        for (var i = 0; i < _data.Length; i++)
        {
            queue.Enqueue(_data[i]);
        }
    }

    [Benchmark]
    public void EnqueueUsingLinkedList()
    {
        var queue = new SimpleQueueLinkedList<int>();

        for (var i = 0; i < _data.Length; i++)
        {
            queue.Enqueue(_data[i]);
        }
    }
}
