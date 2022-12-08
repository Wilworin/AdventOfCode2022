class Day7
{
    static Dictionary<string, int> sumsOfDirs = new Dictionary<string, int>();

    public static void Challenge1()
    {
        string fileName = "Day7\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var path = "";
        for (int i = 0; i < input.Length; i++)
        {
            Console.WriteLine(input[i]);
            var split = input[i].Split(' ');

            if (int.TryParse(split[0], out int number))
            {
                UpdateSumOfPath(number, path);
                continue;
            }
            if (split[1] == "cd")
            {
                path = ChangeDirectory(split[2], path);
                UpdateSumOfPath(0, path);
                Console.WriteLine("Path: " + path);
            }
            continue;
        }

        var totalSum = 0;
        Console.WriteLine("Sums in Dirs");
        foreach (var current in sumsOfDirs)
        {
            var sum = 0;
            foreach (var compare in sumsOfDirs)
            {
                if (compare.Key.Contains(current.Key))
                {
                    sum += compare.Value;
                }
            }
            Console.WriteLine(current.Key+ ": " + sum);
            if (sum <= 100000)
                totalSum+= sum;
        }

        Console.WriteLine("Day7. Challenge 1: " + totalSum);
    }

    public static void Challenge2()
    {
        string fileName = "Day7\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var output = 0;
        var path = "";
        for (int i = 0; i < input.Length; i++)
        {
            var split = input[i].Split(' ');

            if (int.TryParse(split[0], out int number))
            {
                UpdateSumOfPath(number, path);
                continue;
            }
            if (split[1] == "cd")
            {
                path = ChangeDirectory(split[2], path);
                UpdateSumOfPath(0, path);
            }
            continue;
        }

        var sums = new SortedList<int, string>();
        var totalSum = 0;
        foreach (var current in sumsOfDirs)
        {
            var sum = 0;
            foreach (var compare in sumsOfDirs)
            {
                if (compare.Key.Contains(current.Key))
                {
                    sum += compare.Value;
                }
            }
            if (current.Key == "C:/")
            {
                totalSum = sum;
            }
            sums.TryAdd(sum, current.Key);
        }
        Console.WriteLine("Sums in Dirs: " + totalSum);
        var free = 70000000 - totalSum;
        var sumToDelete = 30000000 - free;
        Console.WriteLine("Free: " + free);
        Console.WriteLine("To Delete: " + sumToDelete);

        foreach (var i in sums)
        {
            if (i.Key >= sumToDelete)
            {
                output = i.Key;
                break;
            }
        }

        Console.WriteLine("Day7. Challenge 2: " + output);
    }

    private static string ChangeDirectory(string command, string path)
    {
        if (command == "/")
            return "C:/";
        if (command == "..")
        {
            if (path == "C:/") return "C:/";
            var lastIndex = path.LastIndexOf('/',(path.Length-2));
            return lastIndex > 0 ? path.Substring(0, lastIndex+1) : "C:/";
        }
        return path + command + "/";
    }

    private static void UpdateSumOfPath (int sum, string path)
    {
        if (sumsOfDirs.TryGetValue(path, out var oldSum)) 
        {
            sumsOfDirs[path] = oldSum + sum;
        }
        else sumsOfDirs.TryAdd(path, sum);
    }
}