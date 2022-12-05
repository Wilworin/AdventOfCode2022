public static class Day5
{
    public static void Challenge1()
    {
        string fileName = "Day5\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        var stacks = new List<string>[10];
        for (int i = 1; i < 10; i++)
            stacks[i] = new List<string>();
        
        for (int row = 7; row >= 0; row--)
        {
            var stack = 1;
            for (int col = 1; col < input[row].Length; col+=4)
            {
                //Console.WriteLine(stack);
                var crate = input[row].Substring(col,1);
                if (crate != " ")
                {
                    stacks[stack].Add(crate);
                }
                stack++;
            }
            //Console.WriteLine();
            //Console.WriteLine(input[row] + "o");
        }
        
        //for (int i = 1; i < 10; i++)
        //{
        //    Console.Write(i + ": ");
        //    foreach (var crate in stacks[i])
        //        Console.Write(crate);
        //    Console.WriteLine();
        //}

        for (int i = 10; i < input.Length; i++)
        {
            var ins = input[i].Split(' ');
            MoveFromTo(int.Parse(ins[1]), int.Parse(ins[3]), int.Parse(ins[5]), stacks);
            Console.WriteLine(input[i]);
        }

        for (int i = 1; i < 10; i++)
        {
            Console.Write(i + ": ");
            foreach (var crate in stacks[i])
                Console.Write(crate);
            Console.WriteLine();
        }

        Console.WriteLine("Day 5. Challenge 1: TGWSMRBPN");
    }

    public static void Challenge2()
    {
        string fileName = "Day5\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        var stacks = new List<string>[10];
        for (int i = 1; i < 10; i++)
            stacks[i] = new List<string>();

        for (int row = 7; row >= 0; row--)
        {
            var stack = 1;
            for (int col = 1; col < input[row].Length; col += 4)
            {
                //Console.WriteLine(stack);
                var crate = input[row].Substring(col, 1);
                if (crate != " ")
                {
                    stacks[stack].Add(crate);
                }
                stack++;
            }
            //Console.WriteLine();
            //Console.WriteLine(input[row] + "o");
        }

        //for (int i = 1; i < 10; i++)
        //{
        //    Console.Write(i + ": ");
        //    foreach (var crate in stacks[i])
        //        Console.Write(crate);
        //    Console.WriteLine();
        //}

        for (int i = 10; i < input.Length; i++)
        {
            var ins = input[i].Split(' ');
            MoveAmountFromTo(int.Parse(ins[1]), int.Parse(ins[3]), int.Parse(ins[5]), stacks);
            Console.WriteLine(input[i]);
        }

        for (int i = 1; i < 10; i++)
        {
            Console.Write(i + ": ");
            foreach (var crate in stacks[i])
                Console.Write(crate);
            Console.WriteLine();
        }

        Console.WriteLine("Day 5. Challenge 2: TZLTLWRNF");
    }

    private static void MoveFromTo (int amount, int from, int to, List<string>[] stacks)
    {
        for (int i = 1; i <= amount; i++)
        {
            stacks[to].Add(stacks[from].Last());
            stacks[from].RemoveAt(stacks[from].Count - 1);
        }
        //Console.WriteLine(amount + " " + from +" "+ to);
    }

    private static void MoveAmountFromTo(int amount, int from, int to, List<string>[] stacks)
    {
        stacks[to].AddRange(stacks[from].GetRange(stacks[from].Count - amount, amount));
        //stacks[to].Add(stacks[from].Last());
        stacks[from].RemoveRange(stacks[from].Count - amount, amount);
    }
}
