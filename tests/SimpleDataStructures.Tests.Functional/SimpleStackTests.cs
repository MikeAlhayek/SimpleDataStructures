using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleStackTests
{
    [Fact]
    public void AbleToPushPopAndVerifySize()
    {
        var stack = new SimpleStack<int>();

        stack.Push(10);
        stack.Push(20);
        stack.Push(30);
        stack.Push(40);
        stack.Push(50);
        stack.Push(60);

        Assert.Equal(6, stack.Size());
        stack.Pop();
        stack.Pop();
        Assert.Equal(4, stack.Size());
        Assert.Equal(40, stack.Peek());

        stack.Pop();
        stack.Pop();
        stack.Pop();
        stack.Pop();
        Assert.Equal(0, stack.Size());

        Assert.Throws<IndexOutOfRangeException>(() => stack.Pop());
        Assert.Throws<IndexOutOfRangeException>(() => stack.Peek());
    }
}
