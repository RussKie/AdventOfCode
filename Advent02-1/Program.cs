class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        List<int[]> numberSequences = lines
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(' ').Select(int.Parse).ToArray())
            .ToList();

        int safeCount = numberSequences.Count(CheckSafety);
        foreach (var sequence in numberSequences)
        {
            string result = CheckSafety(sequence) ? "Safe" : "Unsafe";
            Console.WriteLine($"Sequence {string.Join(" ", sequence)} is {result}.");
        }

        Console.WriteLine(safeCount);
    }

    static bool CheckSafety(int[] levels)
    {
        if (!CheckPattern(levels))
        {
            return false;
        }

        for (int i = 0; i < levels.Length - 1; i++)
        {
            int difference = Math.Abs(levels[i] - levels[i + 1]);
            if (difference < 1 || difference > 3)
            {
                return false;
            }
        }

        return true;
    }

    static bool CheckPattern(int[] levels)
    {
        bool isIncreasing = true;
        bool isDecreasing = true;

        for (int i = 0; i < levels.Length - 1; i++)
        {
            if (levels[i] < levels[i + 1])
            {
                isDecreasing = false;
            }
            else if (levels[i] > levels[i + 1])
            {
                isIncreasing = false;
            }
        }

        return isIncreasing || isDecreasing;
    }
}
