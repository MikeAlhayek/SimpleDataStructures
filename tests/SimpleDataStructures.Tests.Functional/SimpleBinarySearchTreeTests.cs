using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleBinarySearchTreeTests
{
    [Fact]
    public void AbleToPlaceNodesCorrectly()
    {
        var tree = new SimpleBinarySearchTree<string>();

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
    public void RemoveLeaf()
    {
        var tree = new SimpleBinarySearchTree<int>();

        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(7);
        var one = tree.Insert(1);
        tree.Insert(4);
        tree.Insert(9);
        tree.Insert(2);
        tree.Insert(6);
        tree.Insert(8);

        Assert.Equal(9, tree.Count);

        tree.Remove(2);

        Assert.Null(one.LeftChild);
        Assert.Null(one.RightChild);

        Assert.Equal(8, tree.Count);
    }

    [Fact]
    public void PromoteLeftLeafToRoot()
    {
        var tree = new SimpleBinarySearchTree<int>();

        tree.Insert(5);
        var three = tree.Insert(3);
        var seven = tree.Insert(7);
        var one = tree.Insert(1);
        tree.Insert(4);
        tree.Insert(9);
        var two = tree.Insert(2);
        tree.Insert(6);
        tree.Insert(8);

        Assert.Equal(9, tree.Count);

        tree.Remove(5);

        // Assert that the two node became the new root.
        Assert.Null(two.Parent);
        Assert.Equal(two.LeftChild, three);
        Assert.Equal(two.RightChild, seven);

        // Assert one is a leaf
        Assert.True(one.IsLeaf());

        // Assert that the tree is one less node.
        Assert.Equal(8, tree.Count);
    }

    [Fact]
    public void PromoteRightLeafToRoot()
    {
        var tree = new SimpleBinarySearchTree<int>();

        tree.Insert(5);
        var nine = tree.Insert(9);
        var twentyFive = tree.Insert(25);
        var twenty = tree.Insert(20);
        tree.Insert(26);

        tree.Remove(5);

        // Assert that the two node became the new root.
        Assert.Null(twenty.Parent);
        Assert.Null(twenty.LeftChild);
        Assert.Equal(twenty.RightChild, nine);
        Assert.Null(twentyFive.LeftChild);

        // Assert that the tree is one less node.
        Assert.Equal(4, tree.Count);
    }

    [Fact]
    public void RemoveRightSingleArrowNode()
    {
        var tree = new SimpleBinarySearchTree<int>();

        var five = tree.Insert(5);
        tree.Insert(9);
        var twentyFive = tree.Insert(25);
        tree.Insert(20);
        tree.Insert(26);

        tree.Remove(9);

        // Assert that the two node became the new root.
        Assert.Equal(five.RightChild, twentyFive);
        Assert.Equal(twentyFive.Parent, five);

        // Assert that the tree is one less node.
        Assert.Equal(4, tree.Count);
    }

    [Fact]
    public void InOrderTraverse()
    {
        var tree = new SimpleBinarySearchTree<int>();

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
        tree.InOrderTraverse((nodeValue) => items[index++] = nodeValue);

        var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (var i = 0; i < items.Length; i++)
        {
            Assert.Equal(expected[i], items[i]);
        }
    }

    [Fact]
    public void PreOrderTraverse()
    {
        var tree = new SimpleBinarySearchTree<int>();

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
        tree.PreOrderTraverse((nodeValue) => items[index++] = nodeValue);

        var expected = new int[] { 5, 3, 1, 2, 4, 7, 6, 9, 8 };

        for (var i = 0; i < items.Length; i++)
        {
            Assert.Equal(expected[i], items[i]);
        }
    }


    [Fact]
    public void PostOrderTraverse()
    {
        var tree = new SimpleBinarySearchTree<int>();

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
        tree.PostOrderTraverse((nodeValue) => items[index++] = nodeValue);

        var expected = new int[] { 2, 1, 4, 3, 6, 8, 9, 7, 5 };

        for (var i = 0; i < items.Length; i++)
        {
            Assert.Equal(expected[i], items[i]);
        }
    }
}
