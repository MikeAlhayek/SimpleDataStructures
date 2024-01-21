using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleQueueTests
{
    [Fact]
    public void Size_WhenEmpty_ReturnsZero()
    {
        var stack = new SimpleQueue<int>();

        Assert.Equal(0, stack.Size());
    }

    [Fact]
    public void Size_WhenInitializedWithArray_ReturnsCorrectCount()
    {
        var stack = new SimpleQueue<int>([1, 2, 3, 4]);

        Assert.Equal(4, stack.Size());
    }

    [Fact]
    public void Enqueue_WhenEnqueuing_ReturnsCorrectCount()
    {
        var queue = new SimpleQueue<int>();

        var items = new[] { 10, 20, 30, 40, 50, 60 };

        foreach (var item in items)
        {
            queue.Enqueue(item);
        }

        Assert.Equal(items.Length, queue.Size());
    }

    [Fact]
    public void Dequeue_WhenCalled_ReturnsFirstElement()
    {
        var queue = new SimpleQueue<int>();

        var items = new[] { 10, 20, 30 };

        foreach (var item in items)
        {
            queue.Enqueue(item);
        }

        var first = queue.Dequeue();

        Assert.Equal(items[0], first);
    }

    [Fact]
    public void Rear_WhenCalledWhileEmpty_ReturnsIndexOutOfRangeException()
    {
        var queue = new SimpleQueue<int>();

        Assert.Throws<IndexOutOfRangeException>(() => queue.Rear());
    }

    [Fact]
    public void Rear_WhenCalled_ReturnsLastElement()
    {
        var queue = new SimpleQueue<int>();

        var items = new[] { 10, 20, 30 };

        foreach (var item in items)
        {
            queue.Enqueue(item);
        }

        var rear = queue.Rear();

        Assert.Equal(items[items.Length - 1], rear);
    }

    [Fact]
    public void Front_WhenCalledWhileEmpty_ReturnsIndexOutOfRangeException()
    {
        var queue = new SimpleQueue<int>();

        Assert.Throws<IndexOutOfRangeException>(() => queue.Front());
    }

    [Fact]
    public void Front_WhenCalled_ReturnsFirstElement()
    {
        var queue = new SimpleQueue<int>();

        var items = new[] { 10, 20, 30 };

        foreach (var item in items)
        {
            queue.Enqueue(item);
        }

        var front = queue.Front();

        Assert.Equal(items[0], front);
    }

    [Fact]
    public void Dequeue_WhenDequeuing_ReturnsCorrectCount()
    {
        var queue = new SimpleQueue<int>();

        var items = new[] { 10, 20, 30, 40, 50, 60 };

        foreach (var item in items)
        {
            queue.Enqueue(item);
        }

        queue.Dequeue();

        Assert.Equal(items.Length - 1, queue.Size());
    }

    [Fact]
    public void Dequeue_WhenEmpty_ReturnsIndexOutOfRangeException()
    {
        var queue = new SimpleQueue<int>();

        Assert.Throws<IndexOutOfRangeException>(() => queue.Dequeue());
    }
}
