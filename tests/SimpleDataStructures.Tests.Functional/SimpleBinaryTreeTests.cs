using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleBinaryTreeTests
{
    [Fact]
    public void AbleToPlaceNodesCorrectly()
    {
        var tree = new SimpleBinaryTree<string>();

        tree.Insert("Mike");
        tree.Insert("Joe");
        tree.Insert("Jaylen");
        tree.Insert("Jace");
        tree.Insert("Jack");
        tree.Insert("Andy");
        tree.Insert("John");
        tree.Insert("Nick");
        tree.Insert("Camron");
        tree.Insert("Weston");

        Assert.True(tree.Exists("Jace"));

        Assert.False(tree.Exists("Mark"));
    }


    [Fact]
    public void InOrderTraverse()
    {
        var tree = new SimpleBinaryTree<int>();

        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(7);
        tree.Insert(1);
        tree.Insert(4);
        tree.Insert(9);
        tree.Insert(2);
        tree.Insert(6);
        tree.Insert(8);

        var items = new int[9];

        var index = 0;
        tree.Traverse(Models.BinaryTreeTraversalAlgorithm.InOrder, (nodeValue) => items[index++] = nodeValue);

        var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (var i = 0; i < items.Length; i++)
        {
            Assert.Equal(expected[i], items[i]);
        }
    }


    [Fact]
    public void PreOrderTraverse()
    {
        var tree = new SimpleBinaryTree<int>();

        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(7);
        tree.Insert(1);
        tree.Insert(4);
        tree.Insert(9);
        tree.Insert(2);
        tree.Insert(6);
        tree.Insert(8);

        var items = new int[9];

        var index = 0;
        tree.Traverse(Models.BinaryTreeTraversalAlgorithm.PreOrder, (nodeValue) => items[index++] = nodeValue);

        var expected = new int[] { 5, 3, 1, 2, 4, 7, 6, 9, 8 };

        for (var i = 0; i < items.Length; i++)
        {
            Assert.Equal(expected[i], items[i]);
        }
    }


    [Fact]
    public void PostOrderTraverse()
    {
        var tree = new SimpleBinaryTree<int>();

        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(7);
        tree.Insert(1);
        tree.Insert(4);
        tree.Insert(9);
        tree.Insert(2);
        tree.Insert(6);
        tree.Insert(8);

        var items = new int[9];

        var index = 0;
        tree.Traverse(Models.BinaryTreeTraversalAlgorithm.PostOrder, (nodeValue) => items[index++] = nodeValue);

        var expected = new int[] { 2, 1, 4, 3, 6, 8, 9, 7, 5 };

        for (var i = 0; i < items.Length; i++)
        {
            Assert.Equal(expected[i], items[i]);
        }
    }
}
