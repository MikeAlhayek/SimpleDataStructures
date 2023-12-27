namespace SimpleDataStructures;

public class Num
{
    public static int GetNextPrimeNumber(int number)
    {
        // Advance to the next number.
        number++;

        if (number <= 2)
        {
            // 2 is the first prime number.
            return 2;
        }

        var found = true;

        while (true)
        {
            var limit = (int)Math.Sqrt(number);

            for (var i = 2; i <= limit; i++)
            {
                if (number % i == 0)
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                break;
            }

            found = true;
            number++;
        }

        return number;
    }

    public static bool IsOdd(int number)
        => (number % 2) == 1;

    public static bool IsEven(int number)
        => (number % 2) == 0;
}
