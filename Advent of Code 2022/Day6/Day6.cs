class Day6
{
    public static void Challenge1()
    {
        string fileName = "Day6\\input.txt";
        string input = Helper.ReadFileIntoStringArray(fileName)[0];

        var output = 0;
        for (int i = 0; i < input.Length -3; i++)
        {
            var test = input.Substring(i, 4);
            if (test.Distinct().Count() == 4)
            {
                output = i + 4;
                break;
            }
        }
        Console.WriteLine("Day6. Challenge 1: " + output);
    }

    public static void Challenge2()
    {
        string fileName = "Day6\\input.txt";
        string input = Helper.ReadFileIntoStringArray(fileName)[0];

        var output = 0;
        for (int i = 0; i < input.Length - 13; i++)
        {
            var test = input.Substring(i, 14);
            if (test.Distinct().Count() == 14)
            {
                output = i + 14;
                break;
            }
        }
        Console.WriteLine("Day6. Challenge 2: " + output);
    }
}