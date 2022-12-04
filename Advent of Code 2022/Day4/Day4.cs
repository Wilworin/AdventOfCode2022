
public static class Day4
{
    public static void Challenge1()
    {
        string fileName = "Day4\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        int overlapPairs = 0;
        foreach (var line in input)
        {
            var split = line.Split(',');
            var elf1 = split[0].Split('-');
            var elf2 = split[1].Split('-');
            var elf1Start = int.Parse(elf1[0]);
            var elf1End = int.Parse(elf1[1]);
            var elf2Start = int.Parse(elf2[0]);
            var elf2End = int.Parse(elf2[1]);
            if (CalculateOverlap(elf1Start, elf1End, elf2Start, elf2End))
                overlapPairs++;
        }

        Console.WriteLine("Day. Challenge 1: " + overlapPairs);
    }

    public static void Challenge2()
    {
        string fileName = "Day4\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        int overlapPairs = 0;
        foreach (var line in input)
        {
            var split = line.Split(',');
            var elf1 = split[0].Split('-');
            var elf2 = split[1].Split('-');
            var elf1Start = int.Parse(elf1[0]);
            var elf1End = int.Parse(elf1[1]);
            var elf2Start = int.Parse(elf2[0]);
            var elf2End = int.Parse(elf2[1]);
            if (CalculateOverlapAtAll(elf1Start, elf1End, elf2Start, elf2End))
                overlapPairs++;
        }
        Console.WriteLine("Day. Challenge 2: " +overlapPairs);
    }

    private static bool CalculateOverlap(int elf1Start, int elf1End, int elf2Start, int elf2End)
    {
        if (elf1Start >= elf2Start && elf1End <= elf2End)
            return true;
        if (elf1Start <= elf2Start && elf1End >= elf2End)
            return true;
        return false;
    }
    
    private static bool CalculateOverlapAtAll(int elf1Start, int elf1End, int elf2Start, int elf2End)
    {
        if (elf1Start >= elf2Start && elf1Start <= elf2End)
            return true;
        if (elf1End >= elf2Start && elf1End <= elf2End)
            return true;
        if (elf2Start >= elf1Start && elf2Start <= elf1End)
            return true;
        if (elf2End >= elf1Start && elf2End <= elf1End)
            return true;
        return false;
    }
}