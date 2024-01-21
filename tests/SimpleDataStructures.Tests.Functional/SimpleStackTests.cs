using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleStackTests
{
    [Fact]
    public void Size_WhenCalledOnEmptyStack_ReturnZero()
    {
        var stack = new SimpleStack<int>();

        Assert.Equal(0, stack.Size());
    }

    [Fact]
    public void Size_WhenCalledOnInitializedStack_ReturnCorrectCount()
    {
        var stack = new SimpleStack<int>([1, 2, 3, 4, 5]);

        Assert.Equal(5, stack.Size());
    }

    [Fact]
    public void Push_WhenCalled_ReturnCorrectCount()
    {
        var stack = new SimpleStack<int>();

        stack.Push(10);
        stack.Push(20);
        stack.Push(30);
        stack.Push(40);
        stack.Push(50);
        stack.Push(60);

        Assert.Equal(6, stack.Size());
    }

    [Fact]
    public void Pop_WhenCalledOnInitializedStack_ReturnTheLastItemsAndTheCorrectCount()
    {
        var stack = new SimpleStack<int>([1, 2, 3, 4, 5]);

        var five = stack.Pop();

        Assert.Equal(5, five);
        Assert.Equal(4, stack.Size());
    }

    [Fact]
    public void Pop_WhenCalledOnEmptyStack_ThrowsIndexOutOfRangeException()
    {
        var stack = new SimpleStack<int>();

        Assert.Throws<IndexOutOfRangeException>(() => stack.Pop());
    }

    [Fact]
    public void Peek_WhenCalledOnEmptyStack_ThrowsIndexOutOfRangeException()
    {
        var stack = new SimpleStack<int>();

        Assert.Throws<IndexOutOfRangeException>(() => stack.Peek());
    }

    [Fact]
    public void Peek_WhenCalled_ReturnLastItem()
    {
        var stack = new SimpleStack<int>();

        stack.Push(10);
        stack.Push(20);
        stack.Push(30);
        stack.Push(40);
        stack.Push(50);
        stack.Push(60);

        Assert.Equal(60, stack.Peek());
    }
}
