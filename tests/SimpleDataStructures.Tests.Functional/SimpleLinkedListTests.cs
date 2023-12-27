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
}
