using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleQueueTests
{
    [Fact]
    public void AbleToEnqueueDequeueAndVerifySize()
    {
        var stack = new SimpleQueue<int>();

        stack.Enqueue(10);
        stack.Enqueue(20);
        stack.Enqueue(30);
        stack.Enqueue(40);
        stack.Enqueue(50);
        stack.Enqueue(60);

        Assert.Equal(6, stack.Size());
        stack.Dequeue();
        stack.Dequeue();
        Assert.Equal(4, stack.Size());
        Assert.Equal(30, stack.Peek());

        stack.Dequeue();
        stack.Dequeue();
        stack.Dequeue();
        stack.Dequeue();
        Assert.Equal(0, stack.Size());

        Assert.Throws<IndexOutOfRangeException>(() => stack.Dequeue());
        Assert.Throws<IndexOutOfRangeException>(() => stack.Peek());
    }
}
