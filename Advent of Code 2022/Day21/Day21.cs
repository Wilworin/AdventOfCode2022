class Day21
{
    public static void Challenge1()
    {
        long output = 0;
        string fileName = "Day21\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        List<MathMonkey> monkeys = new();
        foreach (var item in input)
        {
            var split = item.Split(' ');
            if(split.Length == 2) // Monkey hands out a number
            {
                monkeys.Add(new MathMonkey(split[0], split[1]));
            }
            if (split.Length == 4) // Monkey hands out an equation
            {
                monkeys.Add(new MathMonkey(split[0], split[1], split[3], split[2]));
            }
        }

        while (true)
        {
            foreach (var monkey in monkeys)
            {
                monkey.Solve(monkeys);
            }
            var root = monkeys.Find(x => x.Name == "root");
            if (root.IsSolved)
            {
                output = root.Value;
                break;
            }
        }
        //foreach (var monkey in monkeys) 
        //{
        //    monkey.Print();
        //}
        Console.WriteLine("Challenge 1: " + output);
    }

    // This solution is just sooooo ugly. Brute force deluxe!
    public static void Challenge2()
    {
        long output = 0;
        long rootStart = 0;
        bool isSolved = false;

        for (long i = 3099532691300; i < 11000000000000; i++)
        {
            List<MathMonkey> monkeys = InitMonkeyList();
            var humn = monkeys.Find(x => x.Name == "humn");
            humn.Value = i;
            while (!isSolved)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.Solve(monkeys);
                }
                var root = monkeys.Find(x => x.Name == "root");
                if (root.IsSolved)
                {
                    root.PrintRoot(monkeys);
                    if (root.Equality == 0)
                    {
                        output = i;
                        isSolved = true;
                        goto end;
                    }
                    break;
                }
            }
        }

        end:

        Console.WriteLine("Challenge 2: " + output); // 3099532691300
    }

    private static List<MathMonkey> InitMonkeyList()
    {
        string fileName = "Day21\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        List<MathMonkey> monkeys = new();
        foreach (var item in input)
        {
            var split = item.Split(' ');
            if (split.Length == 2) // Monkey hands out a number
            {
                monkeys.Add(new MathMonkey(split[0], split[1]));
            }
            if (split.Length == 4) // Monkey hands out an equation
            {
                monkeys.Add(new MathMonkey(split[0], split[1], split[3], split[2]));
            }
        }

        return monkeys;
    }
}

class MathMonkey
{
    public string? Name { get; set; }
    public bool IsSolved { get; set; } = false;
    public long Value { get; set; }
    public string? Monkey1 { get; set; }
    public string? Monkey2 { get; set;}
    public string? Operator { get; set; }
    public long Equality { get; set; }

    public MathMonkey ()
    {

    }

    public MathMonkey(string name, bool isSolved, long value, string? monkey1, string? monkey2, string @operator)
    {
        Name = name;
        IsSolved = isSolved;
        Value = value;
        Monkey1 = monkey1;
        Monkey2 = monkey2;
        Operator = @operator;
    }

    public MathMonkey(string name, string value)
    {
        Name = name.Substring(0,4); // Removes initial colon
        IsSolved = true;
        Value = long.Parse(value);
    }

    public MathMonkey (string name, string? monkey1, string? monkey2, string @operator)
    {
        Name = name.Substring(0,4); // Removes initial colon
        Monkey1 = monkey1;
        Monkey2 = monkey2;
        Operator = @operator;
    }

    public void Solve (List<MathMonkey> monkeys)
    {
        if (IsSolved) { return; }

        var monkey1 = monkeys.Find(x => x.Name == Monkey1);
        var monkey2 = monkeys.Find(x => x.Name == Monkey2);
        if (monkey1.IsSolved && monkey2.IsSolved)
        {
            switch (Operator)
            {
                case "+":
                    Value = monkey1.Value + monkey2.Value; 
                    break;
                case "-":
                    Value = monkey1.Value - monkey2.Value;
                    break;
                case "*":
                    Value = monkey1.Value * monkey2.Value;
                    break;
                case "/":
                    Value = monkey1.Value / monkey2.Value;
                    break;
            }
            IsSolved = true;
            if (Name == "root")
            {
                //Print();
                Equality = monkey1.Value - monkey2.Value;
            }
        }
    }

    public void Print ()
    {
        if (IsSolved)
        {
            Console.WriteLine(Name + ": " + Value + " " + Equality);
        }
        else
        {
            Console.WriteLine(Name + ": " + Monkey1 + " " + Operator + " " + Monkey2);
        }
    }    
    
    public void PrintRoot (List<MathMonkey> monkeys)
    {
        if (IsSolved)
        {
            var monkey1 = monkeys.Find(x => x.Name == Monkey1);
            var monkey2 = monkeys.Find(x => x.Name == Monkey2);
            Console.WriteLine(Name + ": " + Value + ", Monkey1: " + monkey1.Value + ", Monkey2: " + monkey2.Value);
        }
        else
        {
            Console.WriteLine(Name + ": " + Monkey1 + " " + Operator + " " + Monkey2);
        }
    }
}