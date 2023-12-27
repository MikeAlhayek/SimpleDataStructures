namespace SimpleDataStructures;

public class HashTableOptions
{
    public const int DefaultTotalCollisions = 8;

    /// <summary>
    /// Up to how many collisions to allow before we rehash the table.
    /// The smaller the number the faster the lookup, but the slower the add/remove.
    /// This number must be grater than 1.
    /// </summary>
    public int TotalCollisions { get; set; }
}
