using System.Numerics;
class Day11
{
    public static void Challenge1()
    {
        string fileName = "Day11\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        List<Monkey> monkeys = new();

        for (int row = 0; row < input.Length; row += 7)
        {
            string name = input[row];
            //Console.WriteLine("!"+input[row + 1].Substring(18)+"!");
            List<Item> items = ParseItems(input[row + 1][18..]);
            string operation = ParseOperation(input[row + 2], out long param);
            long test = long.Parse(input[row + 3][21..]);
            int ifTrue = int.Parse(input[row + 4][29..]);
            //Console.WriteLine(input[row + 4].Substring(29));
            int ifFalse = int.Parse(input[row + 5][30..]);
            monkeys.Add(new Monkey(name, items, operation, param, test, ifTrue, ifFalse));
        }

        for (int i = 0; i < 20; i++)
        {
            foreach (Monkey monkey in monkeys)
            {
                monkey.DoRound(monkeys);
                //monkey.Print();
            }

        }

        foreach (Monkey monkey in monkeys)
        {
            monkey.Print();
        }
        var a = monkeys.Select(m => { return m.Thrown; }).OrderByDescending(m => m).Take(2);
        var output = a.First() * a.Last();

        Console.WriteLine("Challenge 1: " + output); // 342 + 347 = 118674
    }

    public static void Challenge2()
    {
        string fileName = "Day11\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        List<Monkey2> monkeys = new();

        for (int row = 0; row < input.Length; row += 7)
        {
            string name = input[row];
            List<Item> items = ParseItems(input[row + 1][18..]);
            string operation = ParseOperation(input[row + 2], out long param);
            long test = long.Parse(input[row + 3][21..]);
            int ifTrue = int.Parse(input[row + 4][29..]);
            int ifFalse = int.Parse(input[row + 5][30..]);
            monkeys.Add(new Monkey2(name, items, operation, param, test, ifTrue, ifFalse));
        }

        for (int i = 0; i < 10000; i++)
        {
            Console.WriteLine("Round: " + i);
            foreach (Monkey2 monkey in monkeys)
            {
                monkey.DoRound(monkeys);
            }

        }

        foreach (Monkey2 monkey in monkeys)
        {
            monkey.Print();
        }

        var a = monkeys.Select(m => { return m.Thrown; }).OrderByDescending(m => m).Take(2);
        var output = a.First() * a.Last();
        foreach (var item in a)
            Console.WriteLine(item);

        Console.WriteLine("Challenge 2: " + output); // 32333418600
    }

    private static List<Item> ParseItems(string items)
    {
        List<Item> output = new();
        var split = items.Split(", ");
        foreach (var item in split)
        {
            //Console.WriteLine(item);
            output.Add(new Item(long.Parse(item)));
        }
        return output;
    }

    private static string ParseOperation(string items, out long param)
    {
        var output = "";
        var split = items[19..].Split(' ');
        output = split[1];
        if (split[2] == "old")
        {
            param = 0;
        }
        else
            param = long.Parse(split[2]);

        return output;
    }
}

class Monkey
{
    string Name { get; set; }
    string Operation { get; set; }
    long OperationParam { get; set; }
    long Test { get; set; }
    int ThrowToIfTrue { get; set; }
    int ThrowToIfFalse { get; set; }

    public long Thrown { get; set; } = 0;
    List<Item> Items { get; set; } = new();

    public Monkey(string name, List<Item> items, string operation, long operationParam, long test, int throwIfTrue, int throwIfFalse)
    {
        Name = name;
        Items = items;
        Operation = operation;
        Test = test;
        OperationParam = operationParam;
        ThrowToIfTrue = throwIfTrue;
        ThrowToIfFalse = throwIfFalse;
    }

    public void DoRound(List<Monkey> monkeys)
    {
        while (Items.Count > 0)
        {
            var item = Items[0];
            item.Worry = PerformOperation(item.Worry, Operation, OperationParam);
            item.Worry /= 3;
            if (item.Worry % Test == 0)
            {
                monkeys[ThrowToIfTrue].RecieveItem(item);
                Items.Remove(item);
            }
            else
            {
                monkeys[ThrowToIfFalse].RecieveItem(item);
                Items.Remove(item);
            }
            Thrown++;
        };
    }

    public void RecieveItem(Item item)
    {
        Items.Add(item);
    }

    private long PerformOperation(long worry, string operation, long param)
    {
        if (operation == "+")
        {
            worry += param;
        }
        else
        {
            if (param == 0)
            {
                worry *= worry;
            }
            else
            {
                worry *= param;
            }
        }

        return worry;
    }
    public void Print()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Operation: " + Operation);
        Console.WriteLine("Test: " + Test);
        Console.WriteLine("ThrowIfTrue: " + ThrowToIfTrue);
        Console.WriteLine("ThrowIfFalse: " + ThrowToIfFalse);
        Console.WriteLine("Thrown # times: " + Thrown);
        foreach (Item item in Items)
        {
            Console.Write(item.Worry + ", ");
        }
        Console.WriteLine('\n');
    }
}

class Monkey2
{
    string Name { get; set; }
    string Operation { get; set; }
    long OperationParam { get; set; }
    long Test { get; set; }
    int ThrowToIfTrue { get; set; }
    int ThrowToIfFalse { get; set; }

    public long Thrown { get; set; } = 0;
    List<Item> Items { get; set; } = new();

    public Monkey2(string name, List<Item> items, string operation, long operationParam, long test, int throwIfTrue, int throwIfFalse)
    {
        Name = name;
        Items = items;
        Operation = operation;
        Test = test;
        OperationParam = operationParam;
        ThrowToIfTrue = throwIfTrue;
        ThrowToIfFalse = throwIfFalse;
    }

    public void DoRound(List<Monkey2> monkeys)
    {
        while (Items.Count > 0)
        {
            var item = Items[0];
            item.Worry = PerformOperation(item.Worry, Operation, OperationParam);
            //item.Worry /= 3;

            if (item.Worry % Test == 0)
            {
                monkeys[ThrowToIfTrue].RecieveItem(item);
                Items.Remove(item);
            }
            else
            {
                monkeys[ThrowToIfFalse].RecieveItem(item);
                Items.Remove(item);
            }
            Thrown++;
        };
    }

    public void RecieveItem(Item item)
    {
        Items.Add(item);
    }

    private long PerformOperation(long worry, string operation, long param)
    {
        if (operation == "+")
        {
            worry += param;
        }
        else
        {
            if (param == 0)
            {
                worry *= worry;
            }
            else
            {
                worry *= param;
            }
        }
        //if (worry >= 28132416)
        //    worry %= 28132416;
        
        worry %= 9699690;

        return worry;
    }
    public void Print()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Operation: " + Operation);
        Console.WriteLine("Test: " + Test);
        Console.WriteLine("ThrowIfTrue: " + ThrowToIfTrue);
        Console.WriteLine("ThrowIfFalse: " + ThrowToIfFalse);
        Console.WriteLine("Thrown # times: " + Thrown);
        foreach (Item item in Items)
        {
            Console.Write(item.Worry + ", ");
        }
        Console.WriteLine('\n');
    }
}

class Item
{
    public long Worry { get; set; }

    public Item (long worry)
    {
        this.Worry = worry;
    }
}


/*
public static void Challenge2()
{
    string fileName = "Day11\\input.txt";
    var input = Helper.ReadFileIntoStringArray(fileName);

    List<Monkey2> monkeys = new();

    for (int row = 0; row < input.Length; row += 7)
    {
        string name = input[row];
        List<Item> items = ParseItems(input[row + 1][18..]);
        string operation = ParseOperation(input[row + 2], out long param);
        long test = long.Parse(input[row + 3][21..]);
        int ifTrue = int.Parse(input[row + 4][29..]);
        int ifFalse = int.Parse(input[row + 5][30..]);
        monkeys.Add(new Monkey2(name, items, operation, param, test, ifTrue, ifFalse));
    }

    for (int i = 0; i < 10000; i++)
    {
        Console.WriteLine("Round: " + i);
        foreach (Monkey2 monkey in monkeys)
        {
            monkey.DoRound(monkeys);
        }

    }

    foreach (Monkey2 monkey in monkeys)
    {
        monkey.Print();
    }

    var a = monkeys.Select(m => { return m.Thrown; }).OrderByDescending(m => m).Take(2);
    var output = a.First() * a.Last();
    foreach (var item in a)
        Console.WriteLine(item);
    // 29387694902 too low
    // 29410328972 too low
    // 29434738200 too low
    // 31648982336 no answer
    Console.WriteLine("Challenge 2: " + output);
}

private static List<Item> ParseItems(string items)
{
    List<Item> output = new();
    var split = items.Split(", ");
    foreach (var item in split)
    {
        //Console.WriteLine(item);
        output.Add(new Item(long.Parse(item)));
    }
    return output;
}

private static string ParseOperation(string items, out long param)
{
    var output = "";
    var split = items[19..].Split(' ');
    output = split[1];
    if (split[2] == "old")
    {
        param = 0;
    }
    else
        param = long.Parse(split[2]);

    return output;
}
}
class Monkey2
{
    string Name { get; set; }
    string Operation { get; set; }
    long OperationParam { get; set; }
    long Test { get; set; }
    int ThrowToIfTrue { get; set; }
    int ThrowToIfFalse { get; set; }

    public ulong Thrown { get; set; } = 0;
    List<Item> Items { get; set; } = new();

    public Monkey2(string name, List<Item> items, string operation, long operationParam, long test, int throwIfTrue, int throwIfFalse)
    {
        Name = name;
        Items = items;
        Operation = operation;
        Test = test;
        OperationParam = operationParam;
        ThrowToIfTrue = throwIfTrue;
        ThrowToIfFalse = throwIfFalse;
    }

    public void DoRound(List<Monkey2> monkeys)
    {
        while (Items.Count > 0)
        {
            var item = Items[0];
            item.Worry = PerformOperation(item.Worry, Operation, OperationParam);
            //item.Worry /= 3;
            if (item.Worry % Test == 0)
            {
                monkeys[ThrowToIfTrue].RecieveItem(item);
                Items.Remove(item);
            }
            else
            {
                monkeys[ThrowToIfFalse].RecieveItem(item);
                Items.Remove(item);
            }
            Thrown++;
        };
    }

    public void RecieveItem(Item item)
    {
        Items.Add(item);
    }

    private BigInteger PerformOperation(BigInteger worry, string operation, long param)
    {
        if (operation == "+")
        {
            worry += param;
        }
        else
        {
            if (param == 0)
            {
                worry *= worry;
            }
            else
            {
                worry *= param;
            }
        }
        //if (worry >= 221)
        //    worry -= 221;

        return worry;
    }
    public void Print()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Operation: " + Operation);
        Console.WriteLine("Test: " + Test);
        Console.WriteLine("ThrowIfTrue: " + ThrowToIfTrue);
        Console.WriteLine("ThrowIfFalse: " + ThrowToIfFalse);
        Console.WriteLine("Thrown # times: " + Thrown);
        foreach (Item item in Items)
        {
            Console.Write(item.Worry + ", ");
        }
        Console.WriteLine('\n');
    }
}

class Item
{
    public BigInteger Worry { get; set; }

    public Item(BigInteger worry)
    {
        this.Worry = worry;
    }
}
*/