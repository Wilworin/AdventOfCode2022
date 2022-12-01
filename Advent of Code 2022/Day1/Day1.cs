class Day1
{
    public static void Challenge1()
    {
        string fileName = "Day1\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        int highestCalories = 0;
        int currentCalories = 0;
        foreach (var caloriesRow in input)
        {
            if (caloriesRow != "")
            {
                currentCalories += int.Parse(caloriesRow);
            }

            if (caloriesRow == "")
            {
                if (currentCalories > highestCalories)
                    highestCalories = currentCalories;
                currentCalories = 0;
            }
        }
        Console.WriteLine("Day1. Challenge 1: " + highestCalories);
    }

    public static void Challenge2()
    {
        string fileName = "Day1\\input.txt";
        string[] input = Helper.ReadFileIntoStringArray(fileName);

        List<int> calories = new();
        int currentCalories = 0;
        foreach (var caloriesRow in input)
        {
            if (caloriesRow != "")
            {
                currentCalories += int.Parse(caloriesRow);
            }

            if (caloriesRow == "")
            {
                calories.Add(currentCalories);
                currentCalories = 0;
            }
        }

        var sortedCalories = calories.OrderByDescending(i => i);
        int threeCalories = sortedCalories.ElementAt(0) + sortedCalories.ElementAt(1) + sortedCalories.ElementAt(2);
        Console.WriteLine("Day1. Challenge 2: " + threeCalories);
    }
}
