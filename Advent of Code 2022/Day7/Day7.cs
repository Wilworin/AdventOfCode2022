class Day7
{
    Dictionary<string, int> sumsOfDirs = new Dictionary<string, int>();

    public static void Challenge1()
    {
        string fileName = "Day7\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var command = "";
        var output = 0;
        var sumOfThisDir = 0;
        var path = new List<string>();
        for (int i = 0; i < input.Length; i++)
        {
            var split = input[i].Split(' ');


            //if (int.TryParse(split[0], out int number))
            //{
            //    sumOfThisDir += number;
            //}
            //else
            //{
            //    if (sumOfThisDir > 0)
            //    {
            //        if (sumOfThisDir <= 100000)
            //        {
            //            Console.WriteLine("SUM: " + sumOfThisDir);
            //            output += sumOfThisDir;
            //        }
            //        sumOfThisDir = 0;
            //    }
            //}

            if (command == "dir")
            {
                if (int.TryParse(split[0], out int number))
                {
                    Console.WriteLine(number);
                    sumOfThisDir += number;
                    continue;
                }
                else
                {
                    Console.WriteLine("Slut på LS. Sum: " + sumOfThisDir);
                    if (sumOfThisDir <= 100000)
                        output += sumOfThisDir;
                    sumOfThisDir = 0;
                    command = "";
                }
            }
            if (split[0] == "dir")
            {
                Console.WriteLine("DIR");
                command = "dir";
                continue;
            }
        }
        if (sumOfThisDir <= 100000)
            output += sumOfThisDir;


        Console.WriteLine("Day7. Challenge 1: " + output);
    }

    public static void Challenge2()
    {
        string fileName = "Day7\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var output = 0;

        Console.WriteLine("Day7. Challenge 2: " + output);
    }
}