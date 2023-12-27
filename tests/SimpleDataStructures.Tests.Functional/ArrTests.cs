namespace SimpleDataStructures.Tests.Functional;

public class ArrTests
{
    [Theory]
    [InlineData(new int[] { 10, 20, 40, 20, 30, 40, 50 }, 40, 2)]
    [InlineData(new int[] { 50, 20, 40, 20, 30, 40, 50 }, 50, 0)]
    [InlineData(new int[] { 50, 20, 40, 20, 30, 40, 50 }, 100, -1)]
    public void IndexOfReturnsTheCorrectValue(int[] items, int locate, int correctIndex)
    {
        var index = Arr.IndexOf(items, locate);

        Assert.Equal(correctIndex, index);
    }

    [Theory]
    [InlineData(new int[] { 10, 20, 40, 20, 30, 40, 50 }, 40, 5)]
    [InlineData(new int[] { 50, 20, 40, 20, 30, 40, 50 }, 50, 6)]
    [InlineData(new int[] { 50, 20, 40, 20, 30, 40, 50 }, 100, -1)]
    public void LastIndexOfReturnsTheCorrectValue(int[] items, int locate, int correctIndex)
    {
        var index = Arr.LastIndexOf(items, locate);

        Assert.Equal(correctIndex, index);
    }

    [Theory]
    [InlineData(new int[] { 10, 20, 40, 20, 30, 40, 50 }, new int[] { 20, 40, 20 }, 4)]
    [InlineData(new int[] { 50, 20, 40, 20, 30, 40, 50 }, new int[] { 1 }, 0)]
    [InlineData(new int[] { 10, 20, 50, 40, 50, 60, 70 }, new int[] { 70 }, 1)]
    public void IndexOfAllReturnsTheCorrectCount(int[] items, int[] locate, int correctIndex)
    {
        var index = Arr.IndexOfAll(items, locate);

        Assert.Equal(correctIndex, index.Length);
    }

    [Theory]
    [InlineData(new int[] { 10, 20, 40, 20, 30, 40, 50 }, 5)]
    [InlineData(new int[] { 50, 20, 40, 20, 30, 40, 50, 60, 70, }, 6)]
    [InlineData(new int[] { 10, 20, 40, 30, 50 }, 5)]
    public void DistinctReturnsTheCorrectValues(int[] items, int correctLength)
    {
        var distinctValues = Arr.Distinct(items);

        Assert.Equal(correctLength, distinctValues.Length);
        foreach (var item in items)
        {
            Assert.Contains(item, distinctValues);
        }
    }

    [Theory]
    [InlineData(10, 50, 41)]
    [InlineData(0, 10, 11)]
    [InlineData(-50, -10, 41)]
    [InlineData(-10, 10, 21)]
    [InlineData(-10, 0, 11)]
    [InlineData(-20, -10, 11)]
    [InlineData(1, Arr.MaxCapacity, Arr.MaxCapacity)]
    public void RangeReturnsCollectLength(int min, int max, int actualLength)
    {
        var items = Arr.Range(min, max);

        Assert.Equal(actualLength, items.Length);
    }

    [Theory]
    [InlineData(50, 10)]
    [InlineData(10, -10)]
    public void RangeThrowExceptionWhenMaxAndMinAreBad(int min, int max)
    {
        Assert.Throws<OverflowException>(() =>
        {
            Arr.Range(min, max);
        });
    }

    [Theory]
    [InlineData(int.MinValue, Arr.MaxCapacity)]
    [InlineData(0, Arr.MaxCapacity)]
    public void RangeThrowExceptionWhenMaxAndMinIsOutOfRange(int min, int max)
    {
        Assert.Throws<OverflowException>(() =>
        {
            Arr.Range(min, max);
        });
    }

    [Fact]
    public void ReduceReturnsOddNumberOnly()
    {
        var items = Arr.Range(0, 100);
        var oddNumber = Arr.Reduce(items, (i, item) => item % 2 == 1);

        Assert.Equal(50, oddNumber.Length);
    }

    [Fact]
    public void ReduceReturnsEvenNumberOnly()
    {
        var items = Arr.Range(0, 100);
        var evenNumbers = Arr.Reduce(items, (i, item) => item % 2 == 0);

        Assert.Equal(51, evenNumbers.Length);
    }

    [Theory]
    [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10, 9)]
    [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 9, 8)]
    [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1, 0)]
    [InlineData(new int[] { 10, 20, 30, 40, 50, 60, 70 }, 40, 3)]
    [InlineData(new int[] { 10, 20, 30, 40, 50, 60, 70 }, 1000, -1)]
    public void BinarySearchReturnsCorrectValues(int[] items, int locate, int correctValue)
    {
        Assert.Equal(correctValue, Arr.BinarySearch(items, locate));
    }

    [Theory]
    [InlineData(new int[] { 70, 60, 50, 40, 30, 20, 10 }, new int[] { 10, 20, 30, 40, 50, 60, 70 })]
    [InlineData(new int[] { 70, 20, 30, 40, 50, 60, 10 }, new int[] { 10, 20, 30, 40, 50, 60, 70 })]
    [InlineData(new int[] { 70 }, new int[] { 70 })]
    [InlineData(new int[] { }, new int[] { })]
    public void BubbleSortArrangesCorrectly(int[] items, int[] sorted)
    {
        Arr.BubbleSort(items);

        Assert.Equal(items.Length, sorted.Length);

        for (var i = 0; i < items.Length; i++)
        {
            Assert.Equal(items[i], sorted[i]);
        }
    }
}
