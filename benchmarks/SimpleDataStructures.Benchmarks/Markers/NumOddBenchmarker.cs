using BenchmarkDotNet.Attributes;

namespace SimpleDataStructures.Benchmarks.Markers;

public class NumOddBenchmarker
{
    private const int _num = int.MaxValue;

    [Benchmark(Baseline = true)]
    public void IsOdd()
    {
        Num.IsOdd(_num);
    }

    [Benchmark]
    public bool IsOddBitwise()
    {
        return (_num & 1) == 1;
    }
}
