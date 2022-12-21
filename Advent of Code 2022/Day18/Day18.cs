class Day18
{
    public static void Challenge1()
    {
        string fileName = "Day18\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var output = 0;// input.Length * 4 + 2;

        char[,,] map = new char[30, 30, 30];

        FillMap(input, map);

        output = CountSides(map);

        Console.WriteLine("Challenge 1: " + output);
    }

    public static void Challenge2()
    {
        string fileName = "Day18\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var output = 0;

        Console.WriteLine("Challenge 2: " + output);
    }

    private static void FillMap(string[] input, char[,,] map)
    {
        for (int i = 0; i < input.Length; i++)
        {
            var split = Array.ConvertAll(input[i].Split(','), int.Parse);
            map[split[0], split[1], split[2]] = '#';
        }
    }

    private static int CountSides(char[,,] map)
    {
        int result = 0;
        int counter = 0;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int z = 0; z < map.GetLength(2); z++)
                {
                    if (map[x, y, z] != '#') { continue; } // Point is not a cube, return
                    //Console.WriteLine("Checking: " + x + "," + y + "," + z);
                    counter++;
                    if (x == 0) // First zero checks to handle if axis is zero
                        { result++; }
                    else 
                        { if (map[x - 1, y, z] != '#')
                        { result++; } }
                    if (y == 0) 
                        { result++; } 
                    else 
                        { if (map[x, y - 1, z] != '#') 
                        { result++; } }
                    if (z == 0)
                        { result++; }
                    else 
                        { if (map[x, y, z - 1] != '#') 
                        { result++; } }
                    if (map[x + 1, y, z] != '#') { result++; } // Checks in all directions
                    if (map[x, y + 1, z] != '#') { result++; } // if any sides are free.
                    if (map[x, y, z + 1] != '#') { result++; }
                }
            }
        }
        //Console.WriteLine(counter + " boxes checked.");
        return result;
    }


    private static void Print(char[,,] chars)
    {
        for (int y = 1; y < 7; y++)
        {
            for (int x= 1; x < 7; x++)
            {
                Console.Write(chars[x, y, 2]);
            }
            Console.WriteLine();
        }
    }
}