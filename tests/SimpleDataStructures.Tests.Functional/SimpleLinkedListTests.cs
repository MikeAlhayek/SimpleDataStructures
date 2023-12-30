using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleLinkedListTests
{
    [Fact]
    public void AbleToLoopOverTheNodesCorrectly()
    {
        var list = new SimpleLinkedList<int>();

        list.InsertFirst(10);
        var fifteen = list.InsertNext(15);
        var five = list.InsertFirst(5);
        list.InsertBefore(1, five);
        list.InsertAfter(20, fifteen);
        list.InsertNext(25);
        list.InsertNext(30);

        var correctOrder = new[] { 1, 5, 10, 15, 20, 25, 30 };

        Assert.Null(list.GetFirstOrDefault()!.Previous);
        Assert.NotNull(list.GetFirstOrDefault()!.Next);
        Assert.Null(list.GetLastOrDefault()!.Next);

        var position = 0;

        foreach (var node in list)
        {
            Assert.Equal(correctOrder[position++], node);
        }
    }

    [Fact]
    public void RemoveMiddleNodesCorrectly()
    {
        var list = new SimpleLinkedList<int>();

        list.InsertNext(10);
        list.InsertNext(20);
        var thirty = list.InsertNext(30);
        list.InsertNext(40);
        list.InsertNext(50);
        list.Remove(thirty);

        var correctOrder = new[] { 10, 20, 40, 50 };

        Assert.Equal(correctOrder.Length, list.Count);

        var position = 0;

        foreach (var node in list)
        {
            Assert.Equal(correctOrder[position++], node);
        }
    }

    [Fact]
    public void RemoveRootNodesCorrectly()
    {
        var list = new SimpleLinkedList<int>();

        var ten = list.InsertNext(10);
        list.InsertNext(20);
        list.InsertNext(30);
        list.InsertNext(40);
        list.InsertNext(50);
        list.Remove(ten);

        var correctOrder = new[] { 20, 30, 40, 50 };

        Assert.Equal(correctOrder.Length, list.Count);
        Assert.Equal(correctOrder[0], list.GetFirstOrDefault()!.Value);
        Assert.Equal(correctOrder[correctOrder.Length - 1], list.GetLastOrDefault()!.Value);

        var position = 0;

        foreach (var node in list)
        {
            Assert.Equal(correctOrder[position++], node);
        }
    }

    [Fact]
    public void RemoveLastNodeCorrectly()
    {
        var list = new SimpleLinkedList<int>();

        var ten = list.InsertNext(10);

        Assert.Equal(1, list.Count);

        list.Remove(ten);

        Assert.Equal(0, list.Count);
        Assert.Null(list.GetFirstOrDefault());
        Assert.Null(list.GetLastOrDefault());
    }
}
