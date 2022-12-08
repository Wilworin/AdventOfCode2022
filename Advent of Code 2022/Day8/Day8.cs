class Day8
{
    public static void Challenge1()
    {
        string fileName = "Day8\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var output = 0;
        var width = input[0].Length;
        var height = input.Length;
        //Console.WriteLine(width+" "+height);

        //for(var x = 1; x < width - 1; x++)
        for(var x = 1; x < 2; x++)
        {
            //for (var y = 1; y < height-1; y++)
            for (var y = 1; y < height-1; y++)
            {
                if (Check(x, y, input))
                    output++;
            }
        }
        Console.WriteLine("Day8. Challenge 1: " + output + ('1'<='1'));
    }

    public static void Challenge2()
    {
        string fileName = "Day8\\input.txt";
        var input = Helper.ReadFileIntoIntArray(fileName);

        var output = 0;

        Console.WriteLine("Day8. Challenge 2: " + output);
    }

    private static bool Check (int x, int y, string [] input)
    {
        var toCheck = input[y].ElementAt(x);
        Console.WriteLine("To Check: " + toCheck);
        var result = true;
        for (int i = 0; i < x; i++)
        {
            if (input[y].ElementAt(i) <= toCheck)
                result = false;
        }
        for (int i = x + 1; i < input[y].Length; i++)
        {
            if (input[y].ElementAt(i) <= toCheck)
                result = false;
        }
        for (int i = 0; i < y; i++)
        {
            Console.Write(input[i][x] + " ");
            if (input[i].ElementAt(x) <= toCheck)
                result = false;
        }
        for (int i = y + 1; i < input.Length; i++)
        {
            if (input[i].ElementAt(x) <= toCheck)
                result = false;
        }
        Console.WriteLine();
        return result;
    }
}