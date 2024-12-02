using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");
        List<int> column1 = [];
        List<int> column2 = [];

        foreach (var line in lines)
        {
            var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                int key1 = int.Parse(parts[0]);
                int key2 = int.Parse(parts[1]);
                column1.Add(key1);
                column2.Add(key2);
            }
        }

        column1.Sort();
        column2.Sort();

        int sum = 0;

        for (int i = 0; i < column1.Count; i++)
        {
            Console.WriteLine($"{column1[i]} {column2[i]} {Math.Abs(column1[i] - column2[i])}");
            sum += Math.Abs(column1[i] - column2[i]);
        }

        Console.WriteLine(sum);
    }
}
