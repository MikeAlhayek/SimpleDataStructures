using SimpleDataStructures.Models;

namespace SimpleDataStructures.Structures;

public class SimpleBinaryTree<T> where T : IComparable<T?>
{
    private SimpleBinaryTreeNode<T>? _root;

    public void Insert(T value)
    {
        if (_root == null)
        {
            _root = new SimpleBinaryTreeNode<T>(value);

            return;
        }

        var newNode = new SimpleBinaryTreeNode<T>(value);

        InsertNode(_root, newNode);
    }

    public bool Exists(T value)
    {
        if (_root == null || value == null)
        {
            return default;
        }

        return InternalFind(_root, value) != null;
    }

    public SimpleBinaryTreeNode<T>? Find(T value)
    {
        if (_root == null || value == null)
        {
            return default;
        }

        return InternalFind(_root, value);
    }

    private static SimpleBinaryTreeNode<T>? InternalFind(SimpleBinaryTreeNode<T>? start, T locate)
    {
        if (start == null)
        {
            // At this point, we are done searching and the item does not exists!
            return null;
        }

        if (locate.Equals(start.Value))
        {
            return start;
        }

        if (locate.CompareTo(start.Value) < 1)
        {
            // At this point, the number to locate is less that the parent node.

            // Search left.
            return InternalFind(start.LeftChild, locate);
        }

        // At this pointy, the number to locate is greater than the parent key.
        // Search right.
        return InternalFind(start.RightChild, locate);
    }

    private static void InsertNode(SimpleBinaryTreeNode<T> start, SimpleBinaryTreeNode<T> node)
    {
        if (node.Value.CompareTo(start.Value) < 1)
        {
            // Place left.

            if (start.LeftChild == null)
            {
                start.LeftChild = node;

                return;
            }

            InsertNode(start.LeftChild, node);

            return;
        }

        // Place right.

        if (start.RightChild == null)
        {
            start.RightChild = node;

            return;
        }

        InsertNode(start.RightChild, node);
    }
}
