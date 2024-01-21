using SimpleDataStructures.Models;

namespace SimpleDataStructures.Structures;

public abstract class SimpleBinaryTree<T> where T : IComparable<T?>
{
    protected SimpleBinaryTreeNode<T>? Root;

    public int Count { get; private set; }

    public void Traverse(BinaryTreeTraversalAlgorithm algorithm, Action<T> action)
    {
        if (Root == null)
        {
            return;
        }

        switch (algorithm)
        {
            case BinaryTreeTraversalAlgorithm.PostOrder:
                PostOrderTraverse(Root, action);
                break;
            case BinaryTreeTraversalAlgorithm.PreOrder:
                PreOrderTraverse(Root, action);
                break;
            case BinaryTreeTraversalAlgorithm.BreadthFirstOrder:
                BreadthFirstOrderTraverse(Root, action);
                break;
            default:
                InOrderTraverse(Root, action);
                break;
        };
    }

    public void InOrderTraverse(Action<T> action)
        => Traverse(BinaryTreeTraversalAlgorithm.InOrder, action);

    public void PreOrderTraverse(Action<T> action)
        => Traverse(BinaryTreeTraversalAlgorithm.PreOrder, action);

    public void PostOrderTraverse(Action<T> action)
        => Traverse(BinaryTreeTraversalAlgorithm.PostOrder, action);

    public void BreadthFirstOrderTraverse(Action<T> action)
        => Traverse(BinaryTreeTraversalAlgorithm.BreadthFirstOrder, action);

    public virtual void Clear()
    {
        Root = null;
        Count = 0;
    }

    public virtual bool Exists(T value)
    {
        if (Root == null || value == null)
        {
            return default;
        }

        return Find(Root, value) != null;
    }

    public virtual SimpleBinaryTreeNode<T> Insert(T value)
    {
        if (Root == null)
        {
            Root = new SimpleBinaryTreeNode<T>(value);

            Count++;

            return Root;
        }

        var newNode = new SimpleBinaryTreeNode<T>(value);

        var node = Insert(Root, newNode);

        Count++;

        return node;
    }

    public virtual SimpleBinaryTreeNode<T>? Remove(T locate)
    {
        var nodeToRemove = Find(locate);

        if (nodeToRemove is null)
        {
            return null;
        }

        Count--;

        return Remove(nodeToRemove);
    }

    public virtual SimpleBinaryTreeNode<T>? Find(T value)
    {
        if (Root == null || value == null)
        {
            return default;
        }

        return Find(Root, value);
    }

    protected abstract SimpleBinaryTreeNode<T> Insert(SimpleBinaryTreeNode<T> parent, SimpleBinaryTreeNode<T> newNode);

    protected abstract SimpleBinaryTreeNode<T>? Remove(SimpleBinaryTreeNode<T> nodeToRemove);

    protected abstract SimpleBinaryTreeNode<T>? Find(SimpleBinaryTreeNode<T>? start, T locate);

    protected static void DetachFromParent(SimpleBinaryTreeNode<T> child)
    {
        if (child.Parent!.LeftChild == child)
        {
            child.Parent!.LeftChild = null;
        }

        if (child.Parent!.RightChild == child)
        {
            child.Parent!.RightChild = null;
        }
    }

    protected static SimpleBinaryTreeNode<T> GetHighestLeaf(SimpleBinaryTreeNode<T> start)
    {
        if (start.IsLeaf())
        {
            return start;
        }

        if (start.LeftChild is not null)
        {
            return GetHighestLeaf(start.LeftChild);
        }

        return GetHighestLeaf(start.RightChild!);
    }

    protected static SimpleBinaryTreeNode<T> GetLeftLeaf(SimpleBinaryTreeNode<T> root)
    {
        if (root.LeftChild is null)
        {
            return root;
        }

        return GetLeftLeaf(root.LeftChild);
    }

    protected static SimpleBinaryTreeNode<T> GetRightLeaf(SimpleBinaryTreeNode<T> root)
    {
        if (root.RightChild is null)
        {
            return root;
        }

        return GetRightLeaf(root.RightChild);
    }


    // left, root, right
    private static void InOrderTraverse(SimpleBinaryTreeNode<T>? node, Action<T> action)
    {
        // left, root, right
        if (node == null)
        {
            return;
        }

        if (node.LeftChild != null)
        {
            InOrderTraverse(node.LeftChild, action);
        }

        action(node.Value);

        if (node.RightChild != null)
        {
            InOrderTraverse(node.RightChild, action);
        }
    }

    /// <summary>
    /// Pre-Order. Visit Root, left, then right.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="action"></param>
    private static void PreOrderTraverse(SimpleBinaryTreeNode<T>? node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        action(node.Value);

        if (node.LeftChild != null)
        {
            PreOrderTraverse(node.LeftChild, action);
        }

        if (node.RightChild != null)
        {
            PreOrderTraverse(node.RightChild, action);
        }
    }

    /// <summary>
    /// Pre-Order, Level-order or breadth-first traversal. Visit Root, left, then right.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="action"></param>
    private void BreadthFirstOrderTraverse(SimpleBinaryTreeNode<T>? node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        var queue = new SimpleQueue<SimpleBinaryTreeNode<T>>();
        queue.Enqueue(node);

        while (queue.Size() > 0)
        {
           var first = queue.Dequeue();

            action(first!.Value);

            if (first.LeftChild != null)
            {
                queue.Enqueue(first.LeftChild);
            }

            if (first.RightChild != null)
            {
                queue.Enqueue(first.RightChild);
            }
        }
    }

    // left, right, node
    private static void PostOrderTraverse(SimpleBinaryTreeNode<T>? node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        if (node.LeftChild != null)
        {
            PostOrderTraverse(node.LeftChild, action);
        }

        if (node.RightChild != null)
        {
            PostOrderTraverse(node.RightChild, action);
        }

        action(node.Value);
    }
}
