using BenchmarkDotNet.Attributes;

namespace SimpleDataStructures.Benchmarks.Markers;

public class NumEvenBenchmark
{
    private const int _num = int.MaxValue;

    [Benchmark(Baseline = true)]
    public void IsEven()
    {
        Num.IsEven(_num);
    }

    [Benchmark]
    public bool IsEvenBitwise()
    {
        return (_num & 1) == 0;
    }
}
