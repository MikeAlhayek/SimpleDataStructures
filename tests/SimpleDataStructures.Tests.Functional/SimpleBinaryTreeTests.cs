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
}
