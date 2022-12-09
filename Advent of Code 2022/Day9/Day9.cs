class Day9
{
    public static void Challenge1()
    {
        var rope = new Rope();
        string fileName = "Day9\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        for (int row = 0; row < input.Length; row++)
        {
            Console.WriteLine("Move " + input[row]);
            var split = input[row].Split(' ');
            rope.Movement(split[0], int.Parse(split[1]));
        }
        rope.PrintTailPositions();

        Console.WriteLine("Challenge 1: " + rope.CountTailPositions()); // 6376
    }

    public static void Challenge2()
    {
        var rope = new LongRope();
        string fileName = "Day9\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        //for (int row = 0; row < 10; row++)
        for (int row = 0; row < input.Length; row++)
        {
            //Console.WriteLine("Move " + input[row]);
            var split = input[row].Split(' ');
            rope.Movement(split[0], int.Parse(split[1]));
        }

        Console.WriteLine("Challenge 2: " + rope.CountTailPositions());
    }


}

class Rope
{
    private int Head_X { get; set; } = 1;
    private int Head_Y { get; set; } = 1;
    private int Tail_X { get; set; } = 1;
    private int Tail_Y { get; set; } = 1;
    private List<TailPosition> tailPositions = new();

    public void Movement(string direction, int amount)
    {
        for (int step = 0; step < amount; step++)
        {
            MoveHead(direction);
            Console.WriteLine("Hyp: " + (Math.Sqrt(Math.Pow(Head_X - Tail_X, 2) + Math.Pow(Head_Y - Tail_Y, 2))));
            MoveTail();
            UpdateTailPositionList();
            Print();
        }
        //Console.WriteLine("Hyp: " + (Math.Sqrt(Math.Pow(Head_X - Tail_X, 2) + Math.Pow(Head_Y - Tail_Y, 2))));
        //Console.WriteLine("Head_X: " + Head_X + "  Head_Y: " + Head_Y + "  Tail_X: " + Tail_X + "  Tail_Y: " + Tail_Y);
    }

    private void MoveHead(string direction)
    {
        switch (direction)
        {
            case "U":
                Head_Y++;
                break;
            case "D":
                Head_Y--;
                break;
            case "R":
                Head_X++;
                break;
            case "L":
                Head_X--;
                break;
        }
    }

    private void MoveTail ()
    {
        if (Head_X == Tail_X && Head_Y == Tail_Y) // Same position
            return;
        if (Math.Sqrt(Math.Pow(Head_X - Tail_X, 2) + Math.Pow(Head_Y - Tail_Y, 2)) < 2)  // Adjacent
            return;
        if (Head_X == Tail_X) // Move vertical
        {
            if (Head_Y > Tail_Y)
                Tail_Y++;
            else
                Tail_Y--;
            return;
        }
        if (Head_Y == Tail_Y) // Move horisontal
        {
            if (Head_X > Tail_X)
                Tail_X++;
            else
                Tail_X--;
            return;
        }
        // Diagonal
        if (Head_X > Tail_X)
            Tail_X++;
        else
            Tail_X--;

        if (Head_Y > Tail_Y)
            Tail_Y++;
        else
            Tail_Y--;

        return;
    }

    private void UpdateTailPositionList()
    {
        tailPositions.Add(new TailPosition() { X = Tail_X, Y = Tail_Y });
    }
    public void Print()
    {
        Console.WriteLine("Head_X: " + Head_X + "  Head_Y: " + Head_Y + "  Tail_X: " + Tail_X + "  Tail_Y: " + Tail_Y);
    }

    public void PrintTailPositions()
    {
        Console.WriteLine("Tailpositions:");
        foreach(var t in tailPositions.DistinctBy(m => new {m.X, m.Y}).ToList())
        {
            Console.WriteLine(t.X + " " + t.Y);
        }
    }
    public int CountTailPositions()
    {
        return tailPositions.DistinctBy(m => new { m.X, m.Y }).ToList().Count();
    }
}

class LongRope
{
    private int Head_X { get; set; } = 1;
    private int Head_Y { get; set; } = 1;
    private List<Tail> tails = new();

    public LongRope()
    {
        for (int i = 0; i < 9; i++)
            tails.Add(new Tail());
    }

    public void Movement(string direction, int amount)
    {
        for (int step = 0; step < amount; step++)
        {
            MoveHead(direction);
            //Console.WriteLine("Hyp: " + (Math.Sqrt(Math.Pow(Head_X - tails[0].Tail_X, 2) + Math.Pow(Head_Y - tails[0].Tail_Y, 2))));
            tails[0].MoveTail(Head_X, Head_Y);
            tails[0].UpdateTailPositionList();
            //Console.WriteLine("Head_X: " + Head_X + "  Head_Y: " + Head_Y + "  Tail_X: " + tails[0].Tail_X + "  Tail_Y: " + tails[0].Tail_Y);
            for (int i = 1; i < 9; i++)
            {
                tails[i].MoveTail(tails[i - 1].Tail_X, tails[i - 1].Tail_Y);
                tails[i].UpdateTailPositionList();
                //Print();
            }
        }
        //Console.WriteLine("Hyp: " + (Math.Sqrt(Math.Pow(Head_X - Tail_X, 2) + Math.Pow(Head_Y - Tail_Y, 2))));
    }

    private void MoveHead(string direction)
    {
        switch (direction)
        {
            case "U":
                Head_Y++;
                break;
            case "D":
                Head_Y--;
                break;
            case "R":
                Head_X++;
                break;
            case "L":
                Head_X--;
                break;
        }
    }

    public int CountTailPositions()
    {
        return tails[8].CountTailPositions();
        //return tails[0].CountTailPositions();
    }
}

class Tail
{
    public int Tail_X { get; set; } = 1;
    public int Tail_Y { get; set; } = 1;
    private List<TailPosition> tailPositions = new();

    public void MoveTail(int Head_X, int Head_Y)
    {
        if (Head_X == Tail_X && Head_Y == Tail_Y) // Same position
            return;
        if (Math.Sqrt(Math.Pow(Head_X - Tail_X, 2) + Math.Pow(Head_Y - Tail_Y, 2)) < 2)  // Adjacent
            return;
        if (Head_X == Tail_X) // Move vertical
        {
            if (Head_Y > Tail_Y)
                Tail_Y++;
            else
                Tail_Y--;
            return;
        }
        if (Head_Y == Tail_Y) // Move horisontal
        {
            if (Head_X > Tail_X)
                Tail_X++;
            else
                Tail_X--;
            return;
        }
        // Diagonal
        if (Head_X > Tail_X)
            Tail_X++;
        else
            Tail_X--;

        if (Head_Y > Tail_Y)
            Tail_Y++;
        else
            Tail_Y--;

        return;
    }

    public void UpdateTailPositionList()
    {
        tailPositions.Add(new TailPosition() { X = Tail_X, Y = Tail_Y });
    }
    //public void Print()
    //{
    //    Console.WriteLine("Head_X: " + Head_X + "  Head_Y: " + Head_Y + "  Tail_X: " + Tail_X + "  Tail_Y: " + Tail_Y);
    //}

    public void PrintTailPositions()
    {
        Console.WriteLine("Tailpositions:");
        foreach (var t in tailPositions.DistinctBy(m => new { m.X, m.Y }).ToList())
        {
            Console.WriteLine(t.X + " " + t.Y);
        }
    }
    public int CountTailPositions()
    {
        return tailPositions.DistinctBy(m => new { m.X, m.Y }).ToList().Count();
    }
}

class TailPosition
{
    public int X { get; set; }
    public int Y { get; set; }
}