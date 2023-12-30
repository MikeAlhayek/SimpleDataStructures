using System.Collections;

namespace SimpleDataStructures.Models;

public class SimpleLinkedListNodeEnumerator<T>(SimpleLinkedListNode<T>? root) : IEnumerator<T?>
{
    private readonly SimpleLinkedListNode<T>? _root = root;

    private SimpleLinkedListNode<T>? _current = null;

    public T? Current
        => _current!.Value;

    object? IEnumerator.Current
        => _current!.Value;

    public bool MoveNext()
    {
        if (_current == null)
        {
            _current = _root;

            return true;
        }

        var moved = _current?.Next != null;

        if (moved)
        {
            _current = _current!.Next;
        }

        return moved;
    }

    public void Reset()
    {
        _current = null;
    }

    public void Dispose()
    {

    }
}
