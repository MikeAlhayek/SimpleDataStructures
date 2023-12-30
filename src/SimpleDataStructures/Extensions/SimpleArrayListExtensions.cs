namespace SimpleDataStructures.Structures;

/// <summary>
/// These extensions are synthetics sugar to existing methods to increase code readability.
/// </summary>
public static class SimpleArrayListExtensions
{
    public static bool Contains<T>(this SimpleArrayList<T> list, T? value)
        => list.IndexOf(value) > -1;

    public static T? RemoveLast<T>(this SimpleArrayList<T> list)
        => list.RemoveAt(list.Count - 1);

    public static T? RemoveFirst<T>(this SimpleArrayList<T> list)
        => list.RemoveAt(0);

    public static T? First<T>(this SimpleArrayList<T> list)
        => list[0];

    public static T? Last<T>(this SimpleArrayList<T> list)
        => list[^1];

    public static bool IsEmpty<T>(this SimpleArrayList<T> list)
        => list.Count == 0;
}
