class Day17
{
    //public static void Challenge1()
    //{
    //    string fileName = "Day17\\input.txt";
    //    var input = Helper.ReadFileIntoStringArray(fileName)[0].ToCharArray();
    //    //var input = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>".ToCharArray();
    //    var chamber = new char[9, 10000];
    //    for (int c = 1; c < 8; c++) { chamber[c, 0] = '*'; }
    //    int highest = 0;
    //    int y = 4;
    //    int x = 0;
    //    int shape = 0;
    //    int jetInputPos = 0;

    //    for (int rocks = 1; rocks <= 2022; rocks++)
    //    {
    //        y = highest + 4;
    //        x = 3;
    //        bool falling = true;
    //        while (falling)
    //        {
    //            JetBeam(chamber, shape, ref x, y, input, ref jetInputPos);
    //            Falling(chamber, shape, x, ref y, ref highest, ref falling);
    //        }
    //        shape++;
    //        if (shape == 5) { shape = 0; }
    //    }

    //    //Print(chamber, 55, 0);

    //    Console.WriteLine("Challenge 1: " + highest);
    //}

    public static void Challenge2()
    {
        string fileName = "Day17\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName)[0].ToCharArray();
        //var input = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>".ToCharArray();
        //var chamber = new char[9, 30];
        //var chamber2 = new char[9, 30];

        var chamber = new string[110000000];
        for (int i = 0; i < 110000000; i++)
        {
            chamber[i] = ".........";
        }
        //for (int c = 1; c < 8; c++) { chamber[c, 0] = '*'; }
        int highest = 0;
        int y = 4;
        int x = 0;
        int shape = 0;
        int jetInputPos = 0;
        long total = 0;
        //Array.Copy(chamber, 5, chamber2, 5, 10);

        for (long rocks = 1; rocks <= 1000000; rocks++)
        {
            y = highest + 4;
            x = 3;
            bool falling = true;
            while (falling)
            {
                JetBeam(chamber, shape, ref x, y, input, ref jetInputPos);
                Falling(chamber, shape, x, ref y, ref highest, ref falling);
            }
            shape++;
            if (shape == 5) { shape = 0; }
            if (highest > 105000000)
            {
                total += 100000000; // Convert.ToInt64(highest);
                highest -= 100000000;
                var tempChamber = new string[110000000];
                for (int i = 0; i < 110000000; i++)
                {
                    tempChamber[i] = ".........";
                }
                Array.Copy(chamber, 100000001, tempChamber, 1, 6000000);
                chamber = tempChamber;
                Print(chamber, 50480, 50455);
                Console.WriteLine();
                Print(chamber, 26, 1);
                Console.WriteLine("Total rows so far: " + total);
            }
        }

        //Array.Copy(chamber, 5, chamber2, 0, 10);
        //Print(chamber, 50, 1);
        Print(chamber, 201835, 201820);
        Console.WriteLine();
        Print(chamber, 151380, 151365);
        Console.WriteLine();
        Print(chamber, 100925, 100910);
        Console.WriteLine();
        Print(chamber, 50460, 50455);
        Console.WriteLine();
        Print(chamber, 15, 1);
        Console.WriteLine(total);
        

        Console.WriteLine("Challenge 2: " + (total + (long)highest));
    }

    private static void Falling(char[,] chamber, int shape, int x, ref int y, ref int highest, ref bool falling)
    {
        switch ((Shape)shape)
        {
            case Shape.horisontal:
                if (chamber[x, y - 1] == '\0' && chamber[x + 1, y - 1] == '\0' && chamber[x + 2, y - 1] == '\0' && chamber[x + 3, y - 1] == '\0')
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y , highest);
                }
                break;
            case Shape.plus:
                if (chamber[x, y] == '\0' && chamber[x + 1, y - 1] == '\0' && chamber[x + 2, y] == '\0')
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y + 2, highest);
                }
                break;
            case Shape.corner:
                if (chamber[x, y - 1] == '\0' && chamber[x + 1, y - 1] == '\0' && chamber[x + 2, y - 1] == '\0')
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y + 2, highest);
                }
                break;
            case Shape.vertical:
                if (chamber[x, y - 1] == '\0')
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y + 3, highest);
                }
                break;
            case Shape.square:
                if (chamber[x, y - 1] == '\0' && chamber[x + 1, y - 1] == '\0')
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y + 1, highest);
                }
                break;
        }
    }

    private static void JetBeam(char[,] chamber, int shape, ref int x, int y, char[] input, ref int inputPos)
    {
        switch ((Shape)shape)
        {
            case Shape.horisontal:
                if (input[inputPos] == '>')
                {
                    if (chamber[x + 4, y] == '\0' && x < 4)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[x - 1, y] == '\0' && x > 1)
                    {
                        x--;
                    }
                }
                break;
            case Shape.plus:
                if (input[inputPos] == '>')
                {
                    if (chamber[x + 2, y] == '\0' && chamber[x + 3, y + 1] == '\0' && chamber[x + 2, y + 2] == '\0' && x < 5)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[x, y] == '\0' && chamber[x - 1, y + 1] == '\0' && chamber[x, y + 2] == '\0' && x > 1)
                    {
                        x--;
                    }
                }
                break;
            case Shape.corner:
                if (input[inputPos] == '>')
                {
                    if (chamber[x + 3, y] == '\0' && chamber[x + 3, y + 1] == '\0' && chamber[x + 3, y + 2] == '\0' && x < 5)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[x - 1, y] == '\0' && chamber[x + 1, y + 1] == '\0' && chamber[x + 1, y + 2] == '\0' && x > 1)
                    {
                        x--;
                    }
                }
                break;
            case Shape.vertical:
                if (input[inputPos] == '>')
                {
                    if (chamber[x + 1, y] == '\0' && chamber[x + 1, y + 1] == '\0' && chamber[x + 1, y + 2] == '\0' && chamber[x + 1, y + 3] == '\0' && x < 7)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[x - 1, y] == '\0' && chamber[x - 1, y + 1] == '\0' && chamber[x - 1, y + 2] == '\0' && chamber[x - 1, y + 3] == '\0' && x > 1)
                    {
                        x--;
                    }
                }
                break;
            case Shape.square:
                if (input[inputPos] == '>')
                {
                    if (chamber[x + 2, y] == '\0' && chamber[x + 2, y + 1] == '\0' && x < 6)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[x - 1, y] == '\0' && chamber[x - 1, y + 1] == '\0' && x > 1)
                    {
                        x--;
                    }
                }
                break;
        }
        inputPos++;
        if (inputPos >= input.Length)
            inputPos = 0;
    }

    public static void DrawShape(char[,] chamber, Shape shape, int col, int row)
    {
        switch (shape)
        {
            case Shape.horisontal:
                chamber[col, row] = '#';
                chamber[col + 1, row] = '#';
                chamber[col + 2, row] = '#';
                chamber[col + 3, row] = '#';
                break;
            case Shape.plus:
                chamber[col, row + 1] = '#';
                chamber[col + 1, row] = '#';
                chamber[col + 1, row + 1] = '#';
                chamber[col + 1, row + 2] = '#';
                chamber[col + 2, row + 1] = '#';
                break;
            case Shape.corner:
                chamber[col, row] = '#';
                chamber[col + 1, row] = '#';
                chamber[col + 2, row] = '#';
                chamber[col + 2, row + 1] = '#';
                chamber[col + 2, row + 2] = '#';
                break;
            case Shape.vertical:
                chamber[col, row] = '#';
                chamber[col, row + 1] = '#';
                chamber[col, row + 2] = '#';
                chamber[col, row + 3] = '#';
                break;
            case Shape.square:
                chamber[col, row] = '#';
                chamber[col + 1, row] = '#';
                chamber[col, row + 1] = '#';
                chamber[col + 1, row + 1] = '#';
                break;
        }
    }

    public static void Print(char[,] chamber, int start, int end)
    {
        for (int row = start; row >= end; row--)
        {
            for (int col = 1; col <= 7; col++)
            {
                var c = chamber[col, row];
                Console.Write(c != '\0' ? c : '.');
            }
            Console.WriteLine();
        }
    }

    private static void Falling(string[] chamber, int shape, int x, ref int y, ref int highest, ref bool falling)
    {
        switch ((Shape)shape)
        {
            case Shape.horisontal:
                if (chamber[y - 1][x] == '.' && chamber[y - 1][x + 1] == '.' && chamber[y - 1][x + 2] == '.' && chamber[y - 1][x + 3] == '.' && y > 1)
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y, highest);
                }
                break;
            case Shape.plus:
                if (chamber[y][x] == '.' && chamber[y - 1][x + 1] == '.' && chamber[y][x + 2] == '.' && y > 1)
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y + 2, highest);
                }
                break;
            case Shape.corner:
                if (chamber[y - 1][x] == '.' && chamber[y - 1][x + 1] == '.' && chamber[y - 1][x + 2] == '.' && y > 1)
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y + 2, highest);
                }
                break;
            case Shape.vertical:
                if (chamber[y - 1][x] == '.' && y > 1)
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y + 3, highest);
                }
                break;
            case Shape.square:
                if (chamber[y - 1][x] == '.' && chamber[y - 1][x + 1] == '.' && y > 1)
                {
                    y--;
                }
                else
                {
                    DrawShape(chamber, (Shape)shape, x, y);
                    falling = false;
                    highest = Math.Max(y + 1, highest);
                }
                break;
        }
    }

    private static void JetBeam(string[] chamber, int shape, ref int x, int y, char[] input, ref int inputPos)
    {
        switch ((Shape)shape)
        {
            case Shape.horisontal:
                if (input[inputPos] == '>')
                {
                    if (chamber[y][x + 4] == '.' && x < 4)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[y][x - 1] == '.' && x > 1)
                    {
                        x--;
                    }
                }
                break;
            case Shape.plus:
                if (input[inputPos] == '>')
                {
                    if (chamber[y][x + 2] == '.' && chamber[y + 1][x + 3] == '.' && chamber[y + 2][x + 2] == '.' && x < 5)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[y][x] == '.' && chamber[y + 1][x - 1] == '.' && chamber[y + 2][x] == '.' && x > 1)
                    {
                        x--;
                    }
                }
                break;
            case Shape.corner:
                if (input[inputPos] == '>')
                {
                    if (chamber[y][x + 3] == '.' && chamber[y + 1][x + 3] == '.' && chamber[y + 2][x + 3] == '.' && x < 5)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[y][x - 1] == '.' && chamber[y + 1][x + 1] == '.' && chamber[y + 2][x + 1] == '.' && x > 1)
                    {
                        x--;
                    }
                }
                break;
            case Shape.vertical:
                if (input[inputPos] == '>')
                {
                    if (chamber[y][x + 1] == '.' && chamber[y + 1][x + 1] == '.' && chamber[y + 2][x + 1] == '.' && chamber[y + 3][x + 1] == '.' && x < 7)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[y][x - 1] == '.' && chamber[y + 1][x - 1] == '.' && chamber[y + 2][x - 1] == '.' && chamber[y + 3][x - 1] == '.' && x > 1)
                    {
                        x--;
                    }
                }
                break;
            case Shape.square:
                if (input[inputPos] == '>')
                {
                    if (chamber[y][x + 2] == '.' && chamber[y + 1][x + 2] == '.' && x < 6)
                    {
                        x++;
                    }
                }
                else // <
                {
                    if (chamber[y][x - 1] == '.' && chamber[y + 1][x - 1] == '.' && x > 1)
                    {
                        x--;
                    }
                }
                break;
        }
        inputPos++;
        if (inputPos >= input.Length)
            inputPos = 0;
    }

    public static void DrawShape(string[] chamber, Shape shape, int col, int row)
    {
        switch (shape)
        {
            case Shape.horisontal:
                var t1 = chamber[row].ToCharArray();
                t1[col] = '#';
                t1[col + 1] = '#';
                t1[col + 2] = '#';
                t1[col + 3] = '#';
                chamber[row] = new string(t1);
                break;
            case Shape.plus:
                var t2 = chamber[row + 1].ToCharArray();
                t2[col] = '#';
                t2[col + 1] = '#';
                t2[col + 2] = '#';
                chamber[row + 1] = new string(t2);
                t2 = chamber[row].ToCharArray();
                t2[col + 1] = '#';
                chamber[row] = new string(t2);
                t2 = chamber[row + 2].ToCharArray();
                t2[col + 1] = '#';
                chamber[row + 2] = new string(t2);
                break;
            case Shape.corner:
                var t3 = chamber[row].ToCharArray();
                t3[col] = '#';
                t3[col + 1] = '#';
                t3[col + 2] = '#';
                chamber[row] = new string(t3);
                t3 = chamber[row + 1].ToCharArray();
                t3[col + 2] = '#';
                chamber[row + 1] = new string(t3);
                t3 = chamber[row + 2].ToCharArray();
                t3[col + 2] = '#';
                chamber[row + 2] = new string(t3);
                break;
            case Shape.vertical:
                var t4 = chamber[row].ToCharArray();
                t4[col] = '#';
                chamber[row] = new string(t4);
                t4 = chamber[row + 1].ToCharArray();
                t4[col] = '#';
                chamber[row + 1] = new string(t4);
                t4 = chamber[row + 2].ToCharArray();
                t4[col] = '#';
                chamber[row + 2] = new string(t4);
                t4 = chamber[row + 3].ToCharArray();
                t4[col] = '#';
                chamber[row + 3] = new string(t4); 
                break;
            case Shape.square:
                var t5 = chamber[row].ToCharArray();
                t5[col] = '#';
                t5[col + 1] = '#';
                chamber[row] = new string(t5);
                t5 = chamber[row + 1].ToCharArray();
                t5[col] = '#';
                t5[col + 1] = '#';
                chamber[row + 1] = new string(t5);
                break;
        }
    }


    public static void Print(string[] chamber, int start, int end)
    {
        for (int row = start; row >= end; row--)
        {
            Console.WriteLine(chamber[row].Substring(1, 7));
        }
    }
}

public enum Shape
{
    horisontal = 0,
    plus = 1,
    corner = 2,
    vertical = 3,
    square = 4
}