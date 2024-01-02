using SimpleDataStructures.Models;

namespace SimpleDataStructures.Structures;

public class SimpleBinarySearchTree<T> : SimpleBinaryTree<T> where T : IComparable<T?>
{
    public override bool Exists(T value)
    {
        if (Root == null || value == null)
        {
            return default;
        }

        return Find(Root, value) != null;
    }

    protected override SimpleBinaryTreeNode<T>? Remove(SimpleBinaryTreeNode<T> nodeToRemove)
    {
        if (nodeToRemove.Equals(Root))
        {
            RemoveRoot();

            return nodeToRemove;
        }

        if (nodeToRemove.IsLeaf())
        {
            // At this point, we are removing a leaf.
            if (nodeToRemove.Parent!.LeftChild == nodeToRemove)
            {
                nodeToRemove.Parent.LeftChild = null;
            }
            else if (nodeToRemove.Parent!.RightChild == nodeToRemove)
            {
                nodeToRemove.Parent.RightChild = null;
            }

            return nodeToRemove;
        }

        if (nodeToRemove.HasOneChild())
        {
            // Since the node has a single child, we can remove it and point the parent of the node we are removing to the only child. 
            var newChild = nodeToRemove.LeftChild ?? nodeToRemove.RightChild;

            if (nodeToRemove.Parent!.RightChild == nodeToRemove)
            {
                nodeToRemove.Parent.RightChild = newChild;
            }
            else if (nodeToRemove.Parent!.LeftChild == nodeToRemove)
            {
                nodeToRemove.Parent.LeftChild = newChild;
            }

            newChild!.Parent = nodeToRemove.Parent;

            return nodeToRemove;
        }

        if (nodeToRemove!.LeftChild is null && nodeToRemove.RightChild is null)
        {
            // Removing a leaf node.
            if (nodeToRemove.Parent is not null && nodeToRemove.Parent.Equals(nodeToRemove.LeftChild))
            {
                // Remove the parent's left child.
                nodeToRemove.Parent.LeftChild = null;
            }
            else if (nodeToRemove.Parent is not null && nodeToRemove.Parent.Equals(nodeToRemove.RightChild))
            {
                // Remove the parent's right child.
                nodeToRemove.Parent.RightChild = null;
            }

            return nodeToRemove;
        }

        // find the next root.
        if (nodeToRemove.LeftChild is not null)
        {
            // Swap left leaf with the current root.
            nodeToRemove.Parent!.LeftChild = nodeToRemove.LeftChild!;
            nodeToRemove.LeftChild.Parent = nodeToRemove.Parent;

            return nodeToRemove;
        }

        // At this point, we do not have left branch, lets locate the last node in the right branch.

        // find the next root.
        // Swap left leaf with the current root.

        // Swap left leaf with the current root.
        nodeToRemove.Parent!.RightChild = nodeToRemove.RightChild!;
        nodeToRemove.RightChild!.Parent = nodeToRemove.Parent;

        return nodeToRemove;
    }

    private void RemoveRoot()
    {
        // Removing root.
        if (Root!.IsLeaf())
        {
            // At this point, we clear out the tree since the root is the only node.
            Root = null;

            return;
        }

        // find the next root.
        if (Root.LeftChild is not null)
        {
            var leftLeaf = GetHighestLeaf(Root.LeftChild);

            // Swap left leaf with the current root.
            DetachFromParent(leftLeaf);

            leftLeaf.LeftChild = Root.LeftChild!;
            leftLeaf.LeftChild.Parent = leftLeaf;
            if (Root.RightChild is not null)
            {
                leftLeaf.RightChild = Root.RightChild;
                leftLeaf.RightChild.Parent = leftLeaf;
            }

            leftLeaf.Parent = null;
            Root = leftLeaf;

            return;
        }

        // At this point, we do not have left branch, lets locate the last node in the right branch.

        // find the next root.
        var rightLeaf = GetHighestLeaf(Root.RightChild!);

        DetachFromParent(rightLeaf);

        // Swap left leaf with the current root.
        if (!Root.RightChild!.Equals(rightLeaf))
        {
            rightLeaf.RightChild = Root.RightChild;
            Root.RightChild.Parent = rightLeaf;
        }

        rightLeaf.Parent = null;
        Root = rightLeaf;
    }

    protected override SimpleBinaryTreeNode<T>? Find(SimpleBinaryTreeNode<T>? start, T locate)
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
            return Find(start.LeftChild, locate);
        }

        // At this pointy, the number to locate is greater than the parent key.
        // Search right.
        return Find(start.RightChild, locate);
    }

    protected override SimpleBinaryTreeNode<T> Insert(SimpleBinaryTreeNode<T> parent, SimpleBinaryTreeNode<T> child)
    {
        if (parent.Value.CompareTo(child.Value) >= 1)
        {
            // Place on the left.
            if (parent.LeftChild == null)
            {
                parent.LeftChild = child;
                child.Parent = parent;

                return child;
            }

            return Insert(parent.LeftChild, child);
        }

        // Place on the right.
        if (parent.RightChild == null)
        {
            parent.RightChild = child;
            child.Parent = parent;

            return child;
        }

        return Insert(parent.RightChild, child);
    }
}
