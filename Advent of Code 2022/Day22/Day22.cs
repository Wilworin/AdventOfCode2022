class Day22
{
    public static void Challenge1()
    {
        var output = 0;
        List<string> instructions;
        string fileName = "Day22\\input2.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        var map = new char[18, 13];
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0;y < map.GetLength(1); y++)
            {
                map[x, y] = ' ';
            }
        } // Init map with spaces
        for (int row = 0; row < input.Length - 2; row++) // Fill map with indata
        {
            for (int col = 0; col < input[row].Length; col++)
            {
                map[col, row] = input[row][col];
            }
        }
        Print(map);
        instructions = SplitInstructions(input.Last());
        foreach (var ins in instructions)
        {
            Console.WriteLine(ins);
        }
        var coord = GetStartingPosition(input);
        Console.WriteLine("Challenge 1: " + output);
    }

    public static void Challenge2()
    {
        string fileName = "Day22\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var output = 0;

        Console.WriteLine("Challenge 2: " + output);
    }

    private static List<string> SplitInstructions (string input)
    {
        List<string> output = new();
        var lastOp = "digit";
        var lastIndex = 0;
        for (int i = 0; i <= input.Length; i++)
        {
            if (i == input.Length)
            {
                output.Add(input.Substring(lastIndex));
                break;
            }
            if (char.IsDigit(input[i]))
            {
                if (lastOp == "digit") { continue; }
                else // letter
                {
                    output.Add(input.Substring(lastIndex, i - lastIndex));
                    lastIndex = i;
                    lastOp = "digit";
                    continue;
                }
            }
            if (char.IsLetter(input[i]))
            {
                if (lastOp == "letter") { continue; }
                else // digit
                {
                    output.Add(input.Substring(lastIndex, i - lastIndex));
                    lastIndex = i;
                    lastOp = "letter";
                    continue;
                }
            }
        }
        return output;
    }

    private static Day22_Pos GetStartingPosition(string[] input)
    {
        return new Day22_Pos() { X = input[0].IndexOfAny(new char[] { '.', '#' }), Y = 0, Direction = "Right" };
    }

    public static void Print (char[,] map)
    {
        for (int row = 0; row < map.GetLength(1); row++)
        {
            for (int col = 0; col < map.GetLength(0); col++)
            {
                Console.Write(map[col, row]);
            }
            Console.WriteLine();
        }
    }
}

class Day22_Pos
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Direction { get; set; } = "Right";
}