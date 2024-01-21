using SimpleDataStructures.Models;

namespace SimpleDataStructures.Structures;

/// <summary>
/// In min heap, the root is always smaller that it's children.
/// </summary>
/// <typeparam name="T"></typeparam>
public class SimpleBinaryMinHeapTree<T> : SimpleBinaryTree<T> where T : IComparable<T?>
{
    public override bool Exists(T value)
    {
        throw new NotImplementedException();
    }

    protected override SimpleBinaryTreeNode<T>? Find(SimpleBinaryTreeNode<T>? start, T locate)
    {
        throw new NotImplementedException();
    }

    protected override SimpleBinaryTreeNode<T> Insert(SimpleBinaryTreeNode<T> parent, SimpleBinaryTreeNode<T> newNode)
    {
        var binaryChars = GetTheBinaryPositionOnNextNode();

        // Skip the first character.
        for (var i = 1; i < binaryChars.Length; i++)
        {
            if (binaryChars[i] == '0')
            {
                if (parent.LeftChild == null)
                {
                    newNode.Parent = parent;
                    parent.LeftChild = newNode;

                    BubbleUpLeftChild(parent, newNode);

                    break;
                }

                parent = parent.LeftChild;

                continue;
            }

            if (parent.RightChild == null)
            {
                newNode.Parent = parent;
                parent.RightChild = newNode;

                BubbleUpRightChild(parent, newNode);

                break;
            }

            parent = parent.RightChild;
        }

        return newNode;
    }

    private char[] GetTheBinaryPositionOnNextNode()
    {
        return Convert.ToString(Count + 1, 2).ToCharArray();
    }

    private char[] GetTheBinaryPositionOnLastNode()
    {
        return Convert.ToString(Count, 2).ToCharArray();
    }

    private SimpleBinaryTreeNode<T> GetLastChild()
    {
        var binaryArray = GetTheBinaryPositionOnLastNode();

        if (binaryArray.Length <= 1)
        {
            return Root!;
        }

        // Assume it it the root node;

        var lastChild = Root;

        // Skip the first character.
        for (var i = 1; i < binaryArray.Length; i++)
        {
            if (binaryArray[i] == '0')
            {
                lastChild = lastChild!.LeftChild;

                continue;
            }

            lastChild = lastChild!.RightChild;
        }

        return lastChild!;
    }

    private static void BubbleUpLeftChild(SimpleBinaryTreeNode<T> parent, SimpleBinaryTreeNode<T> child)
    {
        if (parent == null)
        {
            return;
        }

        if (parent.Value.CompareTo(child.Value) < 0)
        {
            return;
        }

        child.Parent = parent.Parent;
        child.LeftChild = parent;
        child.RightChild = parent.RightChild;
        parent.Parent = child;
        parent.RightChild = null;
        parent.LeftChild = null;

        BubbleUpLeftChild(child.Parent!, child);
    }

    private static void BubbleUpRightChild(SimpleBinaryTreeNode<T> parent, SimpleBinaryTreeNode<T> child)
    {
        if (parent == null)
        {
            return;
        }

        if (parent.Value.CompareTo(child.Value) < 0)
        {
            return;
        }

        child.Parent = parent.Parent;
        child.RightChild = parent;
        child.LeftChild = parent.RightChild;
        parent.Parent = child;
        parent.LeftChild = null;
        parent.RightChild = null;

        BubbleUpRightChild(child.Parent!, child);
    }

    private static void BubbleDownLeftChild(SimpleBinaryTreeNode<T> parent, SimpleBinaryTreeNode<T> child)
    {
        if (parent == null)
        {
            return;
        }

        if (parent.Value.CompareTo(child.Value) < 0)
        {
            return;
        }

        var originalParent = parent;
        var originalChild = child;

        child.LeftChild = originalParent;
        child.RightChild = originalParent.RightChild;

        parent.Parent = child;
        parent.Parent.LeftChild = originalChild.LeftChild;
        parent.Parent.RightChild = originalChild.RightChild;

        if (parent.Parent.LeftChild != null)
        {
            BubbleDownLeftChild(parent.Parent, parent.Parent.LeftChild);
        }
    }

    protected override SimpleBinaryTreeNode<T>? Remove(SimpleBinaryTreeNode<T> nodeToRemove)
    {
        if (nodeToRemove == Root)
        {
            if (Count == 1)
            {
                Clear();

                return nodeToRemove;
            }


            // removing the root
            var newRoot = GetLastChild();
            newRoot.LeftChild = nodeToRemove.LeftChild;
            newRoot.RightChild = nodeToRemove.RightChild;

            // bubble down before setting the root.


            Root = newRoot;
        }

        return null; // this is temp value for now.
    }

}
