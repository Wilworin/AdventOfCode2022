
public static class Day3
{
    public static void Challenge1()
    {
        string fileName = "Day3\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        int prioritySum = 0;
        for (var backpack = 0; backpack < input.Length; backpack++)
        // for (var backpack = 0; backpack < 1; backpack++)
        {
            int size = (input[backpack].Length) / 2;

            // Console.WriteLine(input[backpack]);
            var first = input[backpack].Substring(0, size);
            var second = input[backpack].Substring(size);
            // Console.WriteLine(first + "   " + second);
            var uniqueFirst = first.Distinct();
            var uniqueSecond = second.Distinct();
            // var uniqueFirst = (from c in first group c by c into g where g.Count()==1 select g.Key);
            // var uniqueSecond = (from c in second group c by c into g where g.Count()==1 select g.Key);

            char hit = ' ';
            foreach (var v in uniqueFirst)
            {
                if (uniqueSecond.Any(c => c == v))
                {
                    hit = v;
                    break;
                }
                
            }
            Console.WriteLine("HIT: " + hit + "  " + CalcPriority(hit));
            prioritySum += CalcPriority(hit);
            // Console.WriteLine(w.GetEnumerator().ToString());
        }

        Console.WriteLine("Day. Challenge 1: " + prioritySum);
    }

    public static void Challenge2()
    {
        string fileName = "Day3\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        int prioritySum = 0;
        for (var backpack = 0; backpack < input.Length; backpack+=3)
            // for (var backpack = 0; backpack < 1; backpack++)
        {
            int size = (input[backpack].Length) / 2;

            // Console.WriteLine(input[backpack]);
            var first = input[backpack].Distinct();
            var second = input[backpack+1].Distinct();
            var third = input[backpack+2].Distinct();
            // Console.WriteLine(first + "   " + second + "   " + third);

            char hit = ' ';
            foreach (var f in first)
            {
                if (second.Any(s => s == f))
                {
                    if (third.Any(t => t == f))
                    {
                        hit = f;
                        break;
                    }
                }
            
            }
            
            Console.WriteLine("HIT: " + hit + "  " + CalcPriority(hit));
            prioritySum += CalcPriority(hit);
        }

        Console.WriteLine("Day. Challenge 2: " + prioritySum);
    }

    private static int CalcPriority(char c)
    {
        if (Char.IsLower(c))
            return (int)c - 96;
        else
            return (int)c - 38;
    }
}