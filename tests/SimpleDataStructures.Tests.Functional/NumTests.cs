namespace SimpleDataStructures.Tests.Functional;

public class NumTests
{
    [Theory]
    [InlineData(-100, 2)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(3, 5)]
    [InlineData(4, 5)]
    [InlineData(5, 7)]
    [InlineData(8, 11)]
    [InlineData(13, 17)]
    [InlineData(90, 97)]
    public void FindTheNextPrimeNumber(int number, int primeNumber)
    {
        Assert.Equal(primeNumber, Num.GetNextPrimeNumber(number));
    }

    [Theory]
    [InlineData(-100, true)]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(4, true)]
    [InlineData(0, true)]
    [InlineData(13, false)]
    [InlineData(90, true)]
    public void EvaluateValidEvenNumbers(int number, bool valid)
    {
        Assert.Equal(valid, Num.IsEven(number));
    }

    [Theory]
    [InlineData(-100, false)]
    [InlineData(1, true)]
    [InlineData(2, false)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(0, false)]
    [InlineData(13, true)]
    [InlineData(90, false)]
    public void EvaluateValidOddNumbers(int number, bool valid)
    {
        Assert.Equal(valid, Num.IsOdd(number));
    }
}
