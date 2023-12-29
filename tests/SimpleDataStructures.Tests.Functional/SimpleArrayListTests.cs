using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleArrayListTests
{
    [Fact]
    public void CountAndCapacityAreCorrectWithDefaults()
    {
        var list = new SimpleArrayList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.Equal(3, list.Count);
        Assert.Equal(SimpleArrayList.DefaultCapacity, list.Capacity);
    }

    [Fact]
    public void CountAndCapacityAreCorrectWithPredefinedCapacity()
    {
        var list = new SimpleArrayList<int>(capacity: 4);

        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.Equal(3, list.Count);
        Assert.Equal(4, list.Capacity);
    }

    [Fact]
    public void DoubleTheCapacityAsNeeded()
    {
        var list = new SimpleArrayList<int>(capacity: 4);

        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);

        Assert.Equal(5, list.Count);
        Assert.Equal(8, list.Capacity);
    }

    [Theory]
    [InlineData(new int[] { 10, 20, 30, 40, 50, 60, 70, 80 }, 0)] // Remove from the beginning
    [InlineData(new int[] { 10, 20, 30, 40, 50, 60, 70, 80 }, 7)] // Remove from the end
    [InlineData(new int[] { 10, 20, 30, 40, 50, 60, 70, 80 }, 4)] // Remove from the middle
    public void CanItemAtSpecificIndex(int[] org, int indexToRemove)
    {
        // Get the value to remove later.
        // This should be done before modifying the array since we pass a reference of the array to the simple list.
        var valueRemoved = org[indexToRemove];
        var originalCount = org.Length;

        var list = new SimpleArrayList<int>(org);

        list.RemoveAt(indexToRemove);

        Assert.False(list.Contains(valueRemoved));

        Assert.Equal(originalCount - 1, list.Count);
        Assert.Equal(originalCount, list.Capacity);
    }

    [Fact]
    public void RemoveAtThrowsExceptionWhenTheGivenIndexIsOutOfRange()
    {
        var list = new SimpleArrayList<int>([10, 20, 30, 40, 50, 60, 70, 80]);

        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(8));

        list.RemoveAt(0);

        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(7));
    }

    [Fact]
    public void ThrowExceptionWhenFull()
    {
        var items = Arr.Range(1, 100);

        var list = new SimpleArrayList<int>(items, absoluteMaxCapacity: 100);

        Assert.Throws<OutOfMemoryException>(() => list.Add(1));
    }

    [Fact]
    public void AbleToAddItemInLastAvailableSlot()
    {
        var items = Arr.Range(1, 99);

        var list = new SimpleArrayList<int>(items, absoluteMaxCapacity: 100);

        list.Add(1);

        Assert.Equal(100, list.Count);
    }
}
