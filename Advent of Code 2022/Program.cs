/******************************
 * Advent of Code  Year 2022  *
 ******************************/

// Day1.Challenge1();
// Day1.Challenge2();
//Day2.Challenge1();
//Day2.Challenge2();
//Day3.Challenge1();
//Day3.Challenge2();
//Day4.Challenge1();
//Day4.Challenge2();
//Day5.Challenge1();
//Day5.Challenge2();
//Day6.Challenge1();
//Day6.Challenge2();
//Day7.Challenge1();
//Day7.Challenge2();
//Day8.Challenge1();
//Day8.Challenge2();
//Day9.Challenge1();
//Day9.Challenge2();
//Day10.Challenge1();
//Day10.Challenge2();
//Day11.Challenge1();
//Day11.Challenge2();
//Day12.Challenge1();
//Day12.Challenge2();
//Day13.Challenge1();
//Day13.Challenge2();
//Day14.Challenge1();
Day14.Challenge2();
//Day15.Challenge1();
//Day15.Challenge2();
//Day16.Challenge1();
//Day16.Challenge2();
//Day17.Challenge1();
//Day17.Challenge2();
//Day18.Challenge1();
//Day18.Challenge2();
//Day19.Challenge1();
//Day19.Challenge2();

//Console.WriteLine(long.Parse(string.Concat(new int[] {0,1,2,3,4,5,6,7,8,9})).ToString("<000> 000-0000")); 
//string fileName = "Day8\\input.txt";
//var input = Helper.ReadFileInto2DIntArray(fileName);
//Console.WriteLine(input);

static class Helper
{
    public static string[] ReadFileIntoStringArray(string fileName)
    {
        string[] result = File.ReadAllLines("..\\..\\..\\" + fileName);
        return result;
    }

    public static string ReadFileIntoString(string fileName)
    {
        string[] result = File.ReadAllLines("..\\..\\..\\" + fileName);
        return result[0];
    }

    public static List<string> ReadFileIntoStringList(string fileName)
    {
        List<string> result = new();
        string[] inData = File.ReadAllLines("..\\..\\..\\" + fileName);
        foreach (string s in inData)
        {
            result.Add(s);
        }
        return result;
    }

    public static List<int> ReadFileIntoIntList(string fileName)
    {
        List<int> result = new();
        string[] fileInput = File.ReadAllLines("..\\..\\..\\" + fileName);
        int number = 0;
        foreach (string line in fileInput)
        {
            if (int.TryParse(line, out number))
            {
                result.Add(number);
            }
        }
        return result;
    }

    public static int[] ReadFileIntoIntArray(string fileName)
    {
        List<int> result = new();
        string[] fileInput = File.ReadAllLines("..\\..\\..\\" + fileName);
        int number = 0;
        foreach (string line in fileInput)
        {
            if (int.TryParse(line, out number))
            {
                result.Add(number);
            }
        }
        return result.ToArray();
    }

    public static int[,] ReadFileInto2DIntArray(string fileName)
    {
        string[] fileInput = File.ReadAllLines("..\\..\\..\\" + fileName);
        var xMax = fileInput[0].Length;
        var yMax = fileInput.Length;

        var result = new int[xMax, yMax];
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                var t = fileInput[y][x];
                //result[x, y] = int.Parse(t);
            }
        }
        return result;
    }

    public static int ConvertBinaryStringToDecimalInt(string binary)
    {
        return Convert.ToInt32(binary, 2);
    }

    public static void ConvertStringArrayIntoIntArray(string[] input, int[,] inData)
    {
        for (int row = 0; row < input.Length; row++)
        {
            for (int col = 0; col < input[0].Length; col++)
            {
                inData[col, row] = (int)input[row].ElementAt(col) - 48;
            }
        }
    }
}


class Day
{
    public static void Challenge1()
    {
        string fileName = "Day\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);


        Console.WriteLine("Day. Challenge 1: ");
    }

    public static void Challenge2()
    {
        string fileName = "Day\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);


        Console.WriteLine("Day. Challenge 2: ");
    }
}