class Day10
{
    public static void Challenge1()
    {
        string fileName = "Day10\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var output = 0;
        int cycle = 1;
        int X = 1;

        for (int row = 0; row < input.Length; row++)
        {
            var split = input[row].Split(' ');
            var instructionLength = split.Length;
            //Console.WriteLine(instructionLength);
            var addxWait = false;
            var newInstruction = true;
            var value = 0;
            for (int i = 0; i < instructionLength; i++)
            {
                if (newInstruction)
                {
                    if(split[0] == "addx")
                    {
                        value = int.Parse(split[1]);
                        newInstruction = false;
                        addxWait = true;
                    }
                }
 
                if (cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220)
                    output += X*cycle;
                
                if(!newInstruction)
                {
                    if (addxWait)
                    {
                        addxWait = false;
                    }
                    else
                    {
                        X += value;
                        Console.WriteLine("X="+X);
                        newInstruction = true;
                    }
                }

                cycle++;
            }

        }

        Console.WriteLine("Challenge 1: " + output);
    }

    public static void Challenge2()
    {
        string fileName = "Day10\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        //var output = 0;
        int cycle = 1;
        int X = 1;
        var output1 = "";
        var output2 = "";
        var output3 = "";
        var output4 = "";
        var output5 = "";
        var output6 = "";

        for (int row = 0; row < input.Length; row++)
        {
            var split = input[row].Split(' ');
            var instructionLength = split.Length;
            var addxWait = false;
            var newInstruction = true;
            var value = 0;
            for (int i = 0; i < instructionLength; i++)
            {
                if (newInstruction)
                {
                    if (split[0] == "addx")
                    {
                        value = int.Parse(split[1]);
                        newInstruction = false;
                        addxWait = true;
                    }
                }

                //Console.WriteLine("Cycle: " + cycle + "  X: " + X + "  Cycle%40: " + cycle%40);
                var output = "";
                if (X >= cycle%40 - 2 && X <= cycle%40 + 0)
                    output = "#";
                else
                    output = ".";

                if (cycle >= 1 && cycle <= 40)
                    output1 += output;
                if (cycle >= 41 && cycle <= 80)
                    output2 += output;
                if (cycle >= 81 && cycle <= 120)
                    output3 += output;
                if (cycle >= 121 && cycle <= 160)
                    output4 += output;
                if (cycle >= 161 && cycle <= 200)
                    output5 += output;
                if (cycle >= 201 && cycle <= 240)
                    output6 += output;

                //if (cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220)
                //    output += X * cycle;

                if (!newInstruction)
                {
                    if (addxWait)
                    {
                        addxWait = false;
                    }
                    else
                    {
                        X += value;
                        //Console.WriteLine("X=" + X);
                        newInstruction = true;
                    }
                }

                cycle++;
            }

        }

        Console.WriteLine("Challenge 2: ");
        Console.WriteLine(output1);
        Console.WriteLine(output2);
        Console.WriteLine(output3);
        Console.WriteLine(output4);
        Console.WriteLine(output5);
        Console.WriteLine(output6);
    }
}