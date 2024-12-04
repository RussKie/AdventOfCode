using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        List<string> doLines = [];
        foreach (string line in lines)
        {
            var dontLines = line.Split("don't()");
            doLines.Add(dontLines[0]); // always enable from start

            // split lines by "don't()"
            foreach (string dontLine in dontLines.Skip(1))
            {
                Console.WriteLine(dontLine);
                var tokens = dontLine.Split("do()");
                if (tokens.Length > 2)
                {
                    // beginning of the line is disabled, the second token is enabled
                    doLines.AddRange(tokens.Skip(1));
                }
            }
        }

        foreach (string line in doLines)
        {
            Console.WriteLine(line);
        }

        Calculate(doLines.ToArray());
    }

    static void Calculate(string[] lines)
    {
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
