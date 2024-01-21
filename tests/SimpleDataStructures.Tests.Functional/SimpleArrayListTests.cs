using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleArrayListTests
{
    [Fact]
    public void Count_WhenEmpty_ReturnsZeroAndUseDefaultCapacity()
    {
        var list = new SimpleArrayList<int>();

        Assert.Equal(0, list.Count);
        Assert.Equal(SimpleArrayList.DefaultCapacity, list.Capacity);
    }

    [Fact]
    public void Count_WhenInitializedWithItems_ReturnsTheCorrectCount()
    {
        var list = new SimpleArrayList<int>([1, 2, 3]);

        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void Add_WhenCalled_AddItemsToListAndReturnCorrectCountAndUseDefaultCapacity()
    {
        var list = new SimpleArrayList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.Equal(3, list.Count);
        Assert.Equal(SimpleArrayList.DefaultCapacity, list.Capacity);
    }

    [Fact]
    public void Add_WhenCalledWithCapacity_ReturnsCorrectCountAndCapacity()
    {
        var list = new SimpleArrayList<int>(capacity: 4);

        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.Equal(3, list.Count);
        Assert.Equal(4, list.Capacity);
    }

    [Fact]
    public void Add_WhenCalledWithCapacity_ReturnCorrectCountAndDoubleTheCapacity()
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

    [Fact]
    public void Add_WhenCalledWithMaxCapacity_ThrowsOutOfMemoryException()
    {
        var items = Arr.Range(1, 100);

        var list = new SimpleArrayList<int>(items, absoluteMaxCapacity: 100);

        Assert.Throws<OutOfMemoryException>(() => list.Add(1));
    }

    [Fact]
    public void Add_WhenCalledWithOneSlotLeft_ReturnTheAbsoluteMaxCapacity()
    {
        var items = Arr.Range(1, 99);

        var list = new SimpleArrayList<int>(items, absoluteMaxCapacity: 100);

        list.Add(1);

        Assert.Equal(100, list.Count);
    }

    [Theory]
    [InlineData(new int[] { 10, 20, 30, 40, 50, 60, 70, 80 }, 0)] // Remove from the beginning
    [InlineData(new int[] { 10, 20, 30, 40, 50, 60, 70, 80 }, 7)] // Remove from the end
    [InlineData(new int[] { 10, 20, 30, 40, 50, 60, 70, 80 }, 4)] // Remove from the middle
    public void RemoveAt_WhenCalled_RemovedItemAtGivenIndex(int[] org, int indexToRemove)
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
    public void RemoveAt_WhenCalledWithNegativeIndex_ThrowsExceptionWhenTheGivenIndexIsOutOfRange()
    {
        var list = new SimpleArrayList<int>([10, 20, 30, 40, 50, 60, 70, 80]);

        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
    }

    [Fact]
    public void RemoveAt_WhenCalledWithOutOfRangeIndex_ThrowsExceptionWhenTheGivenIndexIsOutOfRange()
    {
        var list = new SimpleArrayList<int>([10, 20, 30, 40, 50, 60, 70, 80]);

        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(10));
    }

    [Fact]
    public void IndexOf_WhenCalled_ReturnsTheCorrectIndex()
    {
        var list = new SimpleArrayList<int>([10, 20, 30, 30, 50, 60, 70, 80]);

        Assert.Equal(2, list.IndexOf(30));
    }

    [Fact]
    public void IndexOf_WhenCalledOnNotFoundItem_ReturnsNegativeOne()
    {
        var list = new SimpleArrayList<int>([10, 20, 30, 30, 50, 60, 70, 80]);

        Assert.Equal(-1, list.IndexOf(100));
    }

    [Fact]
    public void LastIndexOf_WhenCalled_ReturnsTheCorrectIndex()
    {
        var list = new SimpleArrayList<int>([10, 20, 30, 30, 50, 60, 70, 80]);

        Assert.Equal(3, list.LastIndexOf(30));
    }

    [Fact]
    public void LastIndexOf_WhenCalledOnNotFoundItem_ReturnsNegativeOne()
    {
        var list = new SimpleArrayList<int>([10, 20, 30, 30, 50, 60, 70, 80]);

        Assert.Equal(-1, list.LastIndexOf(100));
    }
}
