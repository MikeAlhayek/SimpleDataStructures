using System.Collections;
using SimpleDataStructures.Models;

namespace SimpleDataStructures.Structures;

public class SimpleLinkedList<T> : IEnumerable<T?>
{
    private SimpleLinkedListNode<T>? _root = null;

    private SimpleLinkedListNode<T>? _last = null;

    private int _count = 0;

    public int Count
        => _count;

    public SimpleLinkedListNode<T>? GetFirstOrDefault()
        => _root;

    public SimpleLinkedListNode<T>? GetLastOrDefault()
        => _last ?? _root;

    public bool ValueExists(T value)
    {
        return FindByValueInternal(_root, value) is not null;
    }

    public SimpleLinkedListNode<T>? Find(T value)
    {
        return FindByValueInternal(_root, value);
    }

    public bool Exists(SimpleLinkedListNode<T> locate)
    {
        return ExistsInternal(_root, locate);
    }

    private static bool ExistsInternal(SimpleLinkedListNode<T>? root, SimpleLinkedListNode<T> locate)
    {
        if (root is null)
        {
            return false;
        }

        if (root.Equals(locate))
        {
            return true;
        }

        return ExistsInternal(root.Next, locate);
    }

    private static SimpleLinkedListNode<T>? FindByValueInternal(SimpleLinkedListNode<T>? node, T locate)
    {
        if (node is null)
        {
            return null;
        }

        if (node.Value is not null && node.Value.Equals(locate))
        {
            return node;
        }

        return FindByValueInternal(node.Next, locate);
    }

    public bool IsEmpty()
        => _count > 0;

    public void Clear()
    {
        while (!IsEmpty())
        {
            Remove(GetLastOrDefault()!);
        }

        _count = 0;
    }

    public SimpleLinkedListNode<T> InsertFirst(T item)
    {
        if (_root is null)
        {
            _root = new SimpleLinkedListNode<T>(item);
            _count++;

            return _root;
        }

        var previouslyRoot = _root;

        _root = new SimpleLinkedListNode<T>(item)
        {
            Next = previouslyRoot
        };

        previouslyRoot.Previous = _root;
        _count++;

        return _root;
    }

    public SimpleLinkedListNode<T> InsertNext(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (_root is null)
        {
            return InsertFirst(item);
        }

        if (_last is null)
        {
            _last = new SimpleLinkedListNode<T>(item)
            {
                Previous = _root,
            };

            _root.Next = _last;

            _count++;

            return _last;
        }

        var previouslyLast = _last;

        _last = new SimpleLinkedListNode<T>(item)
        {
            Previous = previouslyLast,
        };

        previouslyLast.Next = _last;
        _count++;
        return _last;
    }

    public SimpleLinkedListNode<T> InsertAfter(T itemToInsert, SimpleLinkedListNode<T> item)
    {
        ArgumentNullException.ThrowIfNull(itemToInsert, nameof(itemToInsert));
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (_root is null)
        {
            InsertFirst(item.Value!);

            return InsertNext(itemToInsert);
        }

        if (item.Equals(_root) || item.Equals(_last))
        {
            // This lookup is done quicker than having to loop.
            return InsertNext(itemToInsert);
        }

        if (!Exists(item))
        {
            throw new ArgumentOutOfRangeException("Unable to find an item to insert after.");
        }

        _count++;

        return new SimpleLinkedListNode<T>(itemToInsert)
        {
            Previous = item,
        };
    }

    public SimpleLinkedListNode<T> InsertBefore(T itemToInsert, SimpleLinkedListNode<T> item)
    {
        ArgumentNullException.ThrowIfNull(itemToInsert, nameof(itemToInsert));
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (_root is null)
        {
            InsertFirst(itemToInsert);

            return InsertNext(item.Value!);
        }

        if (item.Equals(_root) || item.Equals(_last))
        {
            // This lookup is done quicker than having to loop.
            return InsertFirst(itemToInsert);
        }

        if (!Exists(item))
        {
            throw new ArgumentOutOfRangeException("Unable to find an item to insert after.");
        }

        _count++;

        return new SimpleLinkedListNode<T>(itemToInsert)
        {
            Next = item,
        };
    }

    public SimpleLinkedListNode<T>? Remove(SimpleLinkedListNode<T> item)
    {
        if (item == _root)
        {
            // Remove the root.
            _root = _root.Next;
            if (_root is not null)
            {
                _root.Previous = null;
            }
        }
        else if (item == _last)
        {
            // Remove the last.
            _last = _last.Previous;
            _last!.Next = null;

            if (_last is not null && _last == _root)
            {
                // At this point, we know that the new last node is the root.
                // Eliminate the last node.
                _last = null;
            }
        }
        else
        {
            if (!Exists(item))
            {
                return null;
            }

            // Remove a middle node.
            // The next node of the previous node, will become the next node of the node we are removing.
            item.Previous!.Next = item.Next;
        }

        _count--;

        return item;
    }

    public IEnumerator<T?> GetEnumerator()
        => new SimpleLinkedListNodeEnumerator<T>(_root);

    IEnumerator IEnumerable.GetEnumerator()
        => new SimpleLinkedListNodeEnumerator<T>(_root);
}
