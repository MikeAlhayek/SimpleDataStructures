using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleListTests
{
    [Fact]
    public void ListThrowExceptionWhenFull()
    {
        var items = Arr.Range(1, 100);

        var list = new SimpleList<int>(items, absoluteMaxCapacity: 100);

        Assert.Throws<OutOfMemoryException>(() => list.AddRange([1, 2]));
    }

    [Fact]
    public void CanInsertInLastAvailableSlot()
    {
        var items = Arr.Range(1, 99);

        var list = new SimpleList<int>(items, absoluteMaxCapacity: 100);

        list.Add(1);

        Assert.Equal(100, list.Count);
        Assert.Equal(100, list.Capacity);
    }

    [Fact]
    public void CanAddOneItemAtTime()
    {
        var list = new SimpleList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(2);
        list.Add(1);

        Assert.Equal(5, list.Count);
        Assert.Equal(SimpleArrayList.DefaultCapacity, list.Capacity);
    }

    [Fact]
    public void CanAddMultipleItemsAtTime()
    {
        var list = new SimpleList<int>();

        list.AddRange([1, 2, 3, 2, 1]);

        Assert.Equal(5, list.Count);
        Assert.Equal(SimpleArrayList.DefaultCapacity, list.Capacity);
    }

    [Fact]
    public void CanRemoveMultipleItemsAtTime()
    {
        var list = new SimpleList<int>([1, 2, 3, 2, 1]);

        list.RemoveRange([1, 3]);

        Assert.Equal(2, list.Count);
        Assert.Equal(5, list.Capacity);
    }
}
