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
    public void DoubeTheCapacityAsNeeded()
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
}
