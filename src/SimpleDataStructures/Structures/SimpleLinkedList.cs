using System.Collections;

namespace SimpleDataStructures.Structures;

public class SimpleLinkedList<T> : IEnumerable<T?> where T : IEquatable<T?>
{
    private SimpleLinkedListNode<T>? _root = null;

    private SimpleLinkedListNode<T>? _last = null;

    private readonly SimpleArrayList<SimpleLinkedListNode<T>> _items = new();

    public int Count
        => _items.Count;

    public SimpleLinkedListNode<T>[] AsReadOnly()
        => _items.AsArray()!;

    public SimpleLinkedListNode<T>? GetFirstOrDefault()
        => _root;

    public SimpleLinkedListNode<T>? GetLastOrDefault()
        => _last;

    public SimpleLinkedListNode<T> InsertFirst(T? item)
    {
        if (_root is null)
        {
            _root = new SimpleLinkedListNode<T>(item);

            _items.Add(_root);

            return _root;
        }

        var previouslyRoot = _root;

        _root = new SimpleLinkedListNode<T>(item)
        {
            Next = previouslyRoot
        };

        previouslyRoot.Previous = _root;

        _items.Add(_root);

        return _root;
    }

    public SimpleLinkedListNode<T> InsertNext(T? item)
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

            _items.Add(_last);

            return _last;
        }

        var previouslyLast = _last;

        _last = new SimpleLinkedListNode<T>(item)
        {
            Previous = previouslyLast,
        };

        previouslyLast.Next = _last;

        _items.Add(_last);

        return _last;
    }

    public SimpleLinkedListNode<T> InsertAfter(T itemToInsert, SimpleLinkedListNode<T> item)
    {
        ArgumentNullException.ThrowIfNull(itemToInsert, nameof(itemToInsert));
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (_root is null)
        {
            InsertFirst(item.Data);

            return InsertNext(itemToInsert);
        }

        if (item.Equals(_root) || item.Equals(_last))
        {
            // This lookup is done quicker than having to loop.
            return InsertNext(itemToInsert);
        }

        SimpleLinkedListNode<T>? node = null;

        for (var i = 0; i < _items.Count; i++)
        {
            if (_items[i] == null || !_items[i]!.Equals(item))
            {
                continue;
            }

            node = new SimpleLinkedListNode<T>(itemToInsert)
            {
                Previous = _items[i],
            };

            _items[i]!.Next = node;

            _items.Add(node);

            break;
        }

        return node ?? throw new ArgumentOutOfRangeException("Unable to find an item to insert after.");
    }

    public SimpleLinkedListNode<T> InsertBefore(T itemToInsert, SimpleLinkedListNode<T> item)
    {
        ArgumentNullException.ThrowIfNull(itemToInsert, nameof(itemToInsert));
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (_root is null)
        {
            InsertFirst(itemToInsert);

            return InsertNext(item.Data);
        }

        if (item.Equals(_root) || item.Equals(_last))
        {
            // This lookup is done quicker than having to loop.
            return InsertFirst(itemToInsert);
        }

        SimpleLinkedListNode<T>? node = null;

        for (var i = 0; i < _items.Count; i++)
        {
            if (_items[i] == null || !_items[i]!.Equals(item))
            {
                continue;
            }

            node = new SimpleLinkedListNode<T>(itemToInsert)
            {
                Next = _items[i],
            };

            _items[i]!.Previous = node;

            _items.Add(node);

            break;
        }

        return node ?? throw new ArgumentOutOfRangeException("Unable to find an item to insert before.");
    }

    public IEnumerator<T?> GetEnumerator()
        => new SimpleLinkedListNodeEnumerator<T>(_root);

    IEnumerator IEnumerable.GetEnumerator()
        => new SimpleLinkedListNodeEnumerator<T>(_root);
}
