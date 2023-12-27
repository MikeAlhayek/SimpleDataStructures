using SimpleDataStructures.Structures;

namespace SimpleDataStructures.Tests.Functional;

public class SimpleHashTableTests
{
    [Fact]
    public void AddCorrectAmount()
    {
        var hashTable = new SimpleHashTable<int>();
        // The following numbers should force the table to rehash.
        hashTable.Add(1);
        hashTable.Add(1);
        hashTable.Add(18);
        hashTable.Add(35);
        hashTable.Add(52);
        hashTable.Add(69);
        hashTable.Add(86);
        hashTable.Add(103);
        hashTable.Add(120);
        hashTable.Add(137);

        hashTable.Add(10);
        hashTable.Add(20);
        hashTable.Add(30);

        Assert.Equal(12, hashTable.Count);
    }
}
