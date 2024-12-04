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
        //foreach (var sequence in numberSequences)
        //{
        //    string result = CheckSafety(sequence) ? "Safe" : "Unsafe";
        //    Console.WriteLine($"Sequence {string.Join(" ", sequence)} is {result}.");
        //}

        Console.WriteLine(safeCount);
    }

    static int count = 0;
    static bool CheckSafety(int[] levels)
    {
        Console.Write($">> Checking pattern: {string.Join(" ", levels)}");
        int[] checkedLevels = CheckPattern2(levels, true);
        if (checkedLevels.Length < 1)
        {
            Console.WriteLine($"...{count}");
            //Console.WriteLine();
            return false;
        }

        count++;
        Console.WriteLine($"...{count}");
        //Console.WriteLine("...safe");
        //Console.WriteLine();
        return true;
    }

    static int[] CheckPattern2(int[] levels, bool damper)
    {
        //if (!damper)
        //{
        //    Console.Write($"    >> Checking pattern: {string.Join(" ", levels)}");
        //}
        //else
        //{
        //    Console.Write($">> Checking pattern: {string.Join(" ", levels)}");
        //}

        List<int> diffs = [];
        for (int i = 0; i < levels.Length - 1; i++)
        {
            diffs.Add(levels[i + 1] - levels[i]);
        }

        bool isIncreasing = diffs.All(IsInRangeP);
        bool isDecreasing = diffs.All(IsInRangeN);
        if (isIncreasing || isDecreasing)
        {
            return levels;
        }

        if (!damper)
        {
            return [];
        }

        int countN = diffs.Count(diff => diff < 1);
        int countP = diffs.Count(diff => diff > 0);
        isIncreasing = countP > countN;
        isDecreasing = countP < countN;

        //Console.WriteLine($"{string.Join(" ", diffs)}");
        //Console.Write($" diffs: {string.Join(" ", diffs)}");

        int excludeIndex;
        if (isDecreasing && (excludeIndex = GetOutlierN(diffs)) > -1)
        {
            int[] newLevels = levels.Take(excludeIndex).Concat(levels.Skip(excludeIndex + 1)).ToArray();
            var res = CheckPattern2(newLevels, false);
            if (res.Length > 0)
            {
                return res;
            }

            excludeIndex++;
            newLevels = levels.Take(excludeIndex).Concat(levels.Skip(excludeIndex + 1)).ToArray();
            return CheckPattern2(newLevels, false);
        }

        if (isIncreasing && (excludeIndex = GetOutlierP(diffs)) > -1)
        {
            int[] newLevels = levels.Take(excludeIndex).Concat(levels.Skip(excludeIndex + 1)).ToArray();
            var res = CheckPattern2(newLevels, false);
            if (res.Length > 0)
            {
                return res;
            }

            excludeIndex++;
            newLevels = levels.Take(excludeIndex).Concat(levels.Skip(excludeIndex + 1)).ToArray();
            return CheckPattern2(newLevels, false);
        }

        return [];

        static bool IsInRangeN(int value) => value >= -3 && value <= -1;

        static bool IsInRangeP(int value) => value >= 1 && value <= 3;

        static int GetOutlierN(List<int> values)
        {
            int[] outliers = values.Where(value => value >= 0).ToArray();
            if (outliers.Length == 1)
            {
                return values.IndexOf(outliers[0]);
            }
            else if (outliers.Length > 1)
            {
                return -1;
            }

            outliers = values.Where(value => value < -3).ToArray();
            if (outliers.Length == 1)
            {
                return values.IndexOf(outliers[0]);
            }

            return -1;
        }

        static int GetOutlierP(List<int> values)
        {
            int[] outliers = values.Where(value => value <= 0).ToArray();
            if (outliers.Length == 1)
            {
                return values.IndexOf(outliers[0]);
            }
            else if (outliers.Length > 1)
            {
                return -1;
            }

            outliers = values.Where(value => value > 3).ToArray();
            if (outliers.Length == 1)
            {
                return values.IndexOf(outliers[0]);
            }

            return -1;
        }
    }
}
