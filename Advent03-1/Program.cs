using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        long total = 0;
        foreach (string line in lines)
        {
            var matches = Regex.Matches(line, "mul\\((?<v1>\\d{1,3}),(?<v2>\\d{1,3})\\)");
            Console.WriteLine(line);

            foreach (Match match in matches)
            {
                Console.WriteLine($"  --> {match.Value}");

                int mul = int.Parse(match.Groups["v1"].Value) * int.Parse(match.Groups["v2"].Value);
                Console.WriteLine($"  --> {mul}");
                total += mul;
            }
        }

        Console.WriteLine($"Total: {total}");
    }
}
