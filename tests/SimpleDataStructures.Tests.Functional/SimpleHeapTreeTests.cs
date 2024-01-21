using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleHeapTreeTests
{
    [Fact]
    public void Insert_InsertionOrderInMinHeap_ReturnsSameSequenceAsBreadthFirstTraverse()
    {
        var heap = new SimpleBinaryMinHeapTree<int>();

        var values = new[] { 2, 4, 8, 9, 7, 10, 9, 15, 20, 13 };

        foreach (var value in values)
        {
            heap.Insert(value);
        }

        var items = new int[values.Length];

        var index = 0;
        heap.BreadthFirstOrderTraverse((nodeValue) => items[index++] = nodeValue);

        // Asset that the two arrays are equals and the elements are in the same order.
        Assert.Equal(values, items);
    }
}
