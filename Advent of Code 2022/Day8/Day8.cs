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

        for (var x = 1; x < width - 1; x++)
        //for(var x = 1; x < 3; x++)
        {
            for (var y = 1; y < height - 1; y++)
                //for (var y = 1; y < 2; y++)
            {
                if (Check(x, y, input))
                    output++;
            }
        }
        Console.WriteLine("Day8. Challenge 1: " + output +(99*4-2));
    }

    public static void Challenge2()
    {
        string fileName = "Day8\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var width = input[0].Length;
        var height = input.Length;

        var maxSight = 0;
        for (var x = 1; x < width - 1; x++)
        {
            for (var y = 1; y < height - 1; y++)
            {
                var sight = CheckSight(x, y, input);
                if (sight > maxSight)
                    maxSight = sight;
            }
        }
        Console.WriteLine("Day8. Challenge 2: " + maxSight);
    }

    private static bool Check (int x, int y, string [] input)
    {
        var toCheck = input[y].ElementAt(x);
        //Console.WriteLine("To Check: " + toCheck);
        var x1 = true;
        var x2 = true;
        var y1 = true;
        var y2 = true;

        for (int i = 0; i < x; i++)
        {
            //Console.Write(input[y][i] + " ");
            if (input[y].ElementAt(i) >= toCheck)
            {
                x1 = false;
                break;
            }
        }
        //Console.WriteLine();
        for (int i = x + 1; i < input[y].Length; i++)
        {
            //Console.Write(input[y][i] + " ");
            if (input[y].ElementAt(i) >= toCheck)
            {
                x2 = false;
                break;
            }
        }
        //Console.WriteLine();
        for (int i = 0; i < y; i++)
        {
            //Console.Write(input[i][x] + " ");
            if (input[i].ElementAt(x) >= toCheck)
            {
                y1 = false;
                break;
            }
        }
        //Console.WriteLine();
        for (int i = y + 1; i < input.Length; i++)
        {
            //Console.Write(input[i][x] + " ");
            if (input[i].ElementAt(x) >= toCheck)
            {
                y2 = false;
                break;
            }
        }
        //Console.WriteLine();
        return x1 || x2 || y1 || y2;
    }

    private static int CheckSight(int x, int y, string[] input)
    {
        var toCheck = input[y].ElementAt(x);
        //Console.WriteLine("To Check: " + toCheck);
        var x1 = 0;
        var x2 = 0;
        var y1 = 0;
        var y2 = 0;

        for (int i = x-1; i >= 0; i--)
        {
            //Console.Write(input[y][i] + " ");
            x1++;
            if (input[y].ElementAt(i) >= toCheck)
            {
                break;
            }
        }
        //Console.WriteLine();
        for (int i = x + 1; i < input[y].Length; i++)
        {
            //Console.Write(input[y][i] + " ");
            x2++;
            if (input[y].ElementAt(i) >= toCheck)
            {
                break;
            }
        }
        //Console.WriteLine();
        for (int i = y-1; i >= 0; i--)
        {
            //Console.Write(input[i][x] + " ");
            y1++;
            if (input[i].ElementAt(x) >= toCheck)
            {
                break;
            }
        }
        //Console.WriteLine();
        for (int i = y + 1; i < input.Length; i++)
        {
            //Console.Write(input[i][x] + " ");
            y2++;
            if (input[i].ElementAt(x) >= toCheck)
            {
                break;
            }
        }
        //Console.WriteLine("Sight: " + (x1 * x2 * y1 * y2));
        return (x1 * x2 * y1 * y2);
    }
}