class Day14
{
    public static void Challenge1()
    {
        string fileName = "Day14\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        var output = 0;
        char[,] caveSystem = new char[600, 200];
        BuildCaveSystem(caveSystem, input);
        caveSystem[500, 0] = '+';

        int sandCounter = 0;
        while (SpawnSand(caveSystem))
        {
            sandCounter++;
        }

        //PrintCaveSystem(caveSystem, 440, 0, 580, 199);

        Console.WriteLine("Challenge 1: " + sandCounter);
    }

    public static void Challenge2()
    {
        string fileName = "Day14\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        char[,] caveSystem = new char[1000, 200];
        BuildCaveSystem(caveSystem, input);
        BuildFloor(caveSystem);

        int sandCounter = 0;
        while (SpawnSand(caveSystem))
        {
            sandCounter++;
        }
        //PrintCaveSystem(caveSystem, 440, 0, 580, 199);
        Console.WriteLine("Challenge 2: " + sandCounter); // 27155
    }

    public static bool SpawnSand(char[,] cave, int x = 500, int y = 0)
    {
        if (cave[500, 0] == 'o')
        { return false; }
        while (true)
        {
            // Can it move down?
            if (cave[x, y + 1] == '.')
            {
                y++;
            }

            // Can it move down and to the left?
            else if (cave[x -1, y + 1] == '.')
            {
                x--;
                y++;
            }

            // Can it move down and to the right?
            else if (cave[x + 1, y + 1] == '.')
            {
                x++;
                y++;
            }

            // STUCK!
            else
            {
                cave[x, y] = 'o';
                return true;
            }

            // Falling forever
            if (y >= 198)
            {
                return false;
            }
        };
    }

    private static void BuildFloor(char[,] cave)
    {
        int y = (FindFloor(cave)) + 2;
        for (int x = 0; x < 1000; x++)
        {
            cave[x, y] = '#';
        }
    }

    private static int FindFloor(char[,] cave)
    {
        for (int y = 199; y > 100; y--)
        {
            for (int x = 450; x < 550; x++)
            {
                if (cave[x, y] == '#')
                    return y;
            }
        }
        return 0;
    }

    private static void BuildCaveSystem(char[,] caveSystem, string[] input)
    {
        FillCaveWithAir(caveSystem);
        for (int row = 0; row < input.Length; row++)
        {
            var splitRow = input[row].Split(" -> ");
            var startX = int.Parse(splitRow[0].Split(',')[0]);
            var startY = int.Parse(splitRow[0].Split(',')[1]);
            for (int i = 1; i < splitRow.Length; i++)
            {
                var endX = int.Parse(splitRow[i].Split(',')[0]);
                var endY = int.Parse(splitRow[i].Split(',')[1]);
                if (startX == endX) // Vertical line
                {
                    for (int j = Math.Min(startY, endY); j <= Math.Max(startY, endY); j++)
                    {
                        caveSystem[startX, j] = '#';
                    }
                }
                if (startY == endY) // Horisontal line
                {
                    for (int j = Math.Min(startX, endX); j <= Math.Max(startX, endX); j++)
                    {
                        caveSystem[j, startY] = '#';
                    }
                }
                startX = endX;
                startY = endY;
            }
        }
        return;
    }

    private static void FillCaveWithAir(char[,] caveSystem)
    {
        for (int y = 0; y < 200; y++)
        {
            for (int x = 0; x < 1000; x++)
                caveSystem[x, y] = '.'; 
        }
        return;
    }

    private static void PrintCaveSystem(char[,] caveSystem, int startX, int startY, int endX, int endY)
    {
        for (int y = startY; y <= endY; y++)
        {
            for (int x = startX; x <= endX; x++)
                Console.Write(caveSystem[x, y]);
            Console.WriteLine();
        }
    }
}