﻿/******************************
 * Advent of Code  Year 2022  *
 ******************************/

Day1.Challenge1();
Day1.Challenge2();

static class Helper
{
    public static string[] ReadFileIntoStringArray(string fileName)
    {
        string[] result = File.ReadAllLines("..\\..\\..\\" + fileName);
        return result;
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