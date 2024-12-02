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


        int sum = 0;
        for (int i = 0; i < column1.Count; i++)
        {
            int count = column2.Count(x => x == column1[i]);
            Console.WriteLine($"{column1[i]} {count}");

            sum += (column1[i] * count);
        }

        Console.WriteLine(sum);
    }
}
