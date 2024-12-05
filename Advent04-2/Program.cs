class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");
        int originalRows = lines.Length;
        int originalCols = lines[0].Length;
        int newRows = originalRows + 6;
        int newCols = originalCols + 6;

        char[,] matrix = new char[newRows, newCols];

        // Initialize the new matrix with spaces
        for (int i = 0; i < newRows; i++)
        {
            for (int j = 0; j < newCols; j++)
            {
                matrix[i, j] = '.';
            }
        }

        // Copy the original matrix to the center of the new matrix
        for (int i = 0; i < originalRows; i++)
        {
            for (int j = 0; j < originalCols; j++)
            {
                matrix[i + 3, j + 3] = lines[i][j];
            }
        }

        int count = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] != 'A')
                {
                    Console.Write("_ ");
                    continue;
                }

                int res1 = ToInt(matrix[i - 1, j - 1] == 'M' && matrix[i + 1, j + 1] == 'S');
                int res2 = ToInt(matrix[i + 1, j + 1] == 'M' && matrix[i - 1, j - 1] == 'S');
                int res3 = ToInt(matrix[i - 1, j + 1] == 'M' && matrix[i + 1, j - 1] == 'S');
                int res4 = ToInt(matrix[i - 1, j + 1] == 'M' && matrix[i + 1, j - 1] == 'S');

                int res5 = ToInt(matrix[i - 1, j - 1] == 'S' && matrix[i + 1, j + 1] == 'M');
                int res6 = ToInt(matrix[i + 1, j + 1] == 'S' && matrix[i - 1, j - 1] == 'M');
                int res7 = ToInt(matrix[i - 1, j + 1] == 'S' && matrix[i + 1, j - 1] == 'M');
                int res8 = ToInt(matrix[i - 1, j + 1] == 'S' && matrix[i + 1, j - 1] == 'M');

                if (res1 + res2 + res3 + res4 + res5 + res6 + res7 + res8 == 4)
                {
                    Console.Write(matrix[i, j] + " ");
                    count++;
                }
                else
                {
                    Console.Write("? ");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Total: {count}");
    }

    static int ToInt(bool value) => value ? 1 : 0;
}
