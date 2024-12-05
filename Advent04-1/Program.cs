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
                if (matrix[i, j] != 'X')
                {
                    Console.Write("_ ");
                    continue;
                }

                Console.Write(matrix[i, j] + " ");

                int result1 = FindChar(matrix, i - 1,   j - 1,    (c, r) => (c - 1, r - 1), GetNextChar('X'));
                int result2 = FindChar(matrix, i - 1,   j,        (c, r) => (c - 1, r), GetNextChar('X'));
                int result3 = FindChar(matrix, i - 1,   j + 1,    (c, r) => (c - 1, r + 1), GetNextChar('X'));
                int result4 = FindChar(matrix, i,       j + 1,    (c, r) => (c, r + 1), GetNextChar('X'));
                int result5 = FindChar(matrix, i + 1,   j + 1,    (c, r) => (c + 1, r + 1), GetNextChar('X'));
                int result6 = FindChar(matrix, i + 1,   j,        (c, r) => (c + 1, r), GetNextChar('X'));
                int result7 = FindChar(matrix, i + 1,   j - 1,    (c, r) => (c + 1, r - 1), GetNextChar('X'));
                int result8 = FindChar(matrix, i,       j - 1,    (c, r) => (c, r - 1), GetNextChar('X'));

                count += result1 + result2 + result3 + result4 + result5 + result6 + result7 + result8;
            }

            Console.WriteLine($"Total: {count}");
        }
    }

    static int FindChar(char[,] matrix, int column, int row, Func<int, int, (int, int)> advanceFunc, char charToFind)
    {
        if (charToFind == (char)0)
        {
            throw new Exception("Invalid character");
        }

        int count = 0;
        if (matrix[column, row] == charToFind)
        {
            char nextChar = GetNextChar(charToFind);
            if (nextChar == (char)0)
            {
                return 1;
            }

            (int i, int j) next = advanceFunc(column, row);
            count += FindChar(matrix, next.i, next.j, advanceFunc, nextChar);
        }

        return count;
    }

    static int FindChar(char[,] matrix, int column, int row, char charToFind)
    {
        if (charToFind == (char)0)
        {
            throw new Exception("Invalid character");
        }

        int count = 0;
        for (int i = column - 1; i <= column + 1; i++)
        {
            for (int j = row - 1; j <= row + 1; j++)
            {
                if (matrix[i, j] == charToFind)
                {
                    char nextChar = GetNextChar(charToFind);
                    if (nextChar == (char)0)
                    {
                        return 1;
                    }

                    count += FindChar(matrix, i, j, nextChar);
                }
            }
        }

        return count;
    }

    static char GetNextChar(char currentChar)
        => currentChar switch
        {
            'X' => 'M',
            'M' => 'A',
            'A' => 'S',
            'S' => (char)0,
            _ => throw new Exception("Invalid character")
        };
}
