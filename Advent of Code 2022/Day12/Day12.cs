using System.Drawing;
using System.Security.Cryptography.X509Certificates;

class Day12
{
    public static void Challenge1()
    {
        string fileName = "Day12\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        var output = 0;
        var cols = input[0].Length;
        var rows = input.Length;
        var data = new int[cols, rows];
        var check = new int[cols, rows];
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                data[x, y] = (int)input[y][x];
                check[x, y] = 0;
            }
        }
        data[0, 20] = 97;
        data[119, 20] = 122;
        List<AStar> open = new();
        List<AStar> closed = new ();

        AStar start = new AStar() { X = 0, Y = 20 }; // 20, 0  a=97
        AStar end = new AStar() { X = 119, Y = 20 }; // 20, 119  z=122
        start.ToEnd = CalcCostToEnd(start, end);
        start.Sum = start.Cost + start.ToEnd;
        open.Add(start);
        Console.WriteLine(cols + " " + rows);

        //for (int i= 0; i < cols; i++)
        //    Console.Write(data[i, 20]);
        bool pathFound = false;
        while (!pathFound)
        {
            AStar current = open.OrderBy(x => x.Sum).First();
            open.Remove(current);
            closed.Add(current);
            check[current.X, current.Y]++;
            //Console.WriteLine("Current: " + current.X + "," + current.Y + " (" + current.Cost + ")");
            var neighbours = GetNeighbours(current, data, cols, rows, start, end);
            foreach (var neigh in neighbours)
            {
                if (neigh.X == end.X && neigh.Y == end.Y) // First check if end
                {
                    pathFound = true;
                    output = current.Cost + 1;
                    break;
                }
                var inOpen = false;
                var inClosed = false;
                foreach (var c in closed)
                {
                    //if (c.X == neigh.X && c.Y == neigh.Y && c.Cost == neigh.Cost)
                    if (c.X == neigh.X && c.Y == neigh.Y)
                    {
                        inClosed = true;
                        break;
                    }
                }
                foreach (var c in open)
                {
                    if (c == neigh)
                    //if (c.X == neigh.X && c.Y == neigh.Y && c.Cost == neigh.Cost)
                    //if (c.X == neigh.X && c.Y == neigh.Y)
                    {
                        inOpen = true;
                        break;
                    }
                }
                if (!inOpen && !inClosed)
                {
                    open.Add(neigh);
                }
                //if (closed.Any(f => f == neigh))
                //{
                //    continue;
                //}
                //if (!open.Any(f => f == neigh))
                //{
                //    open.Add(neigh);
                //}
            }
            //Console.WriteLine(closed.Count(a => a.X == 1 && a.Y == 20 && a.Cost == 1));
        }

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Console.Write(check[x, y]);
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nChallenge 1: " + output);
    }


    // Challenge 2 is soooo ugly done. Since I was using A* and couldn't figure out a way to calculate the weight
    // of each node since I don't know the start, I simply ran a check from every single a on the board. ;)
    public static void Challenge2()
    {
        string fileName = "Day12\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        var output = 9999999999999;

        var cols = input[0].Length;
        var rows = input.Length;
        var data = new int[cols, rows];
        var check = new int[cols, rows];
        List<AStar> listOfAs = new();
        AStar end = new AStar() { X = 119, Y = 20 }; // 20, 119  z=122
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                data[x, y] = (int)input[y][x];
                if (data[x, y] == 97)
                {
                    var temp = new AStar() { X = x, Y = y };
                    temp.ToEnd = CalcCostToEnd(temp, end);
                    temp.Sum = temp.Cost + temp.ToEnd;
                    listOfAs.Add(temp);
                }
                //check[x, y] = 0;
            }
        }
        data[0, 20] = 97;
        data[119, 20] = 122;
        int count = 0;
        foreach (var start in listOfAs)
        {
            List<AStar> open = new();
            List<AStar> closed = new();

            open.Add(start);

            bool pathFound = false;
            while (!pathFound)
            {
                if (open.Count == 0)
                    { break; }
                AStar current = open.OrderBy(x => x.Sum).First();
                open.Remove(current);
                closed.Add(current);
                check[current.X, current.Y]++;
                var neighbours = GetNeighbours(current, data, cols, rows, start, end);
                foreach (var neigh in neighbours)
                {
                    if (neigh.X == end.X && neigh.Y == end.Y) // First check if end
                    {
                        pathFound = true;
                        output = Math.Min(current.Cost + 1, output);
                        break;
                    }
                    var inOpen = false;
                    var inClosed = false;
                    foreach (var c in closed)
                    {
                        if (c.X == neigh.X && c.Y == neigh.Y)
                        {
                            inClosed = true;
                            break;
                        }
                    }
                    foreach (var c in open)
                    {
                        if (c == neigh)
                        {
                            inOpen = true;
                            break;
                        }
                    }
                    if (!inOpen && !inClosed)
                    {
                        open.Add(neigh);
                    }
                }
            }
            count++;
            Console.WriteLine(count);
        }

        Console.WriteLine("Challenge 2: " + output);
    }

    private static List<AStar> GetNeighbours (AStar current, int[,] data, int cols, int rows, AStar start, AStar end)
    {
        var output = new List<AStar>();
        if (current.Y > 0 ) // Above
        {
            var n = data[current.X, current.Y - 1];
            var c = (data[current.X, current.Y] + 1);
            if (data[current.X, current.Y - 1] <= (data[current.X, current.Y] + 1))
            {
                //Console.WriteLine(data[current.X, current.Y - 1] + ":" + (data[current.X, current.Y] + 1));
                var temp = new AStar() { X = current.X, Y = current.Y - 1, Cost = current.Cost + 1 };
                temp.ToEnd = CalcCostToEnd(temp, end);
                temp.Sum = temp.Cost + temp.ToEnd;
                output.Add(temp);
            }
        }
        if (current.Y < rows - 1) // Below
        {
            if (data[current.X, current.Y + 1] <= (data[current.X, current.Y] + 1))
            {
                var temp = new AStar() { X = current.X, Y = current.Y + 1, Cost = current.Cost + 1 };
                temp.ToEnd = CalcCostToEnd(temp, end);
                temp.Sum = temp.Cost + temp.ToEnd;
                output.Add(temp);
            }
        }
        if (current.X < cols - 1) // Right
        {
            if (data[current.X + 1, current.Y] <= (data[current.X, current.Y] + 1))
            {
                var temp = new AStar() { X = current.X + 1, Y = current.Y, Cost = current.Cost + 1 };
                temp.ToEnd = CalcCostToEnd(temp, end);
                temp.Sum = temp.Cost + temp.ToEnd;
                output.Add(temp);

            }
        }
        if (current.X > 0) // Left
        {
            if (data[current.X - 1, current.Y] <= (data[current.X, current.Y] + 1))
            {
                var temp = new AStar() { X = current.X - 1, Y = current.Y, Cost = current.Cost + 1 };
                temp.ToEnd = CalcCostToEnd(temp, end);
                temp.Sum = temp.Cost + temp.ToEnd;
                output.Add(temp);
            }
        }
        return output;
    }

    private static int CalcCostToEnd(AStar current, AStar end)
    {
        //return Math.Abs(end.X - current.X) + Math.Abs(end.Y - current.Y);
        return (int)Math.Sqrt(Math.Pow(end.X - current.X, 2) + Math.Pow(end.Y - current.Y, 2));
    }
}

class AStar
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Cost { get; set; } = 0;  // Distance from starting G
    public int ToEnd { get; set; } = 0; // Distance to end H
    public int Sum { get; set; } = 0;   // Sum F

    public static bool operator ==(AStar b1, AStar b2)
    {
        if ((object)b1 == null)
            return (object)b2 == null;

        return b1.Equals(b2);
    }

    public static bool operator !=(AStar b1, AStar b2)
    {
        return !(b1 == b2);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var b2 = (AStar)obj;
        return (X == b2.X && Y == b2.Y);
        //return (X == b2.X && Y == b2.Y && Cost == b2.Cost);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() ^ Cost.GetHashCode();
    }

    //public static bool operator >= (AStar b1, AStar b2)
    //{
    //    return b1.X >= b2.X );
    //}

}
