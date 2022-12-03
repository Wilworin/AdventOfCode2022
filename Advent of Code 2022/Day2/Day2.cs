
public static class Day2
{
    public static void Challenge1()
    {
        string fileName = "Day2\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        int myScore = 0;
        foreach (var round in input)
        {
            var splitString = round.Split(' ');
            myScore += CalculateScore(splitString[0], splitString[1]);
        }

        Console.WriteLine("Day. Challenge 1: " + myScore);
    }

    public static void Challenge2()
    {
        string fileName = "Day2\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        int myScore = 0;
        foreach (var round in input)
        {
            var splitString = round.Split(' ');
            myScore += CalculateResponseScore(splitString[0], splitString[1]);
        }
        
        Console.WriteLine("Day. Challenge 2: " + myScore);
    }

    public static int CalculateScore(string opponent, string myResponse)
    {
        int score = 0;
        switch (opponent)
        {
            case "A": // Opponent chose Rock
                if (myResponse == "X") // I respond Rock
                    score = 1 + 3;
                if (myResponse == "Y") // I respond Paper
                    score = 2 + 6;
                if (myResponse == "Z") // I respond Scissor
                    score = 3;
                break;
            case "B": // Opponent chose Paper
                if (myResponse == "X") // I respond Rock
                    score = 1;
                if (myResponse == "Y") // I respond Paper
                    score = 2 + 3;
                if (myResponse == "Z") // I respond Scissor
                    score = 3 + 6;
                break;
            case "C": // Opponent chose Scissor
                if (myResponse == "X") // I respond Rock
                    score = 1 + 6;
                if (myResponse == "Y") // I respond Paper
                    score = 2;
                if (myResponse == "Z") // I respond Scissor
                    score = 3 + 3;
                break;
        };
        return score;
    }
    
    public static int CalculateResponseScore(string opponent, string myResponse)
    {
        int score = 0;
        switch (opponent)
        {
            case "A": // Opponent chose Rock
                if (myResponse == "Z") // I should win. respond Paper
                    score = 2 + 6;
                if (myResponse == "Y") // I should draw. respond Rock
                    score = 1 + 3;
                if (myResponse == "X") // I should lose. respond Scissor
                    score = 3;
                break;
            case "B": // Opponent chose Paper
                if (myResponse == "Z") // I should win. respond Scissor
                    score = 3 + 6;
                if (myResponse == "Y") // I should draw. respond Paper
                    score = 2 + 3;
                if (myResponse == "X") // I should lose. respond Rock
                    score = 1;
                break;
            case "C": // Opponent chose Scissor
                if (myResponse == "Z") // I should win. respond Rock
                    score = 1 + 6;
                if (myResponse == "Y") // I should draw. respond Scissor
                    score = 3 + 3;
                if (myResponse == "X") // I should lose. respond Paper
                    score = 2;
                break;
        };
        return score;
    }
}