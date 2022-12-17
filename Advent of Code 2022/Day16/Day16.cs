using static System.Reflection.Metadata.BlobBuilder;

class Day16
{
    static List<int> result = new List<int>();
    //var visited = new HashSet<string>();
    static List<BFSItem> queue = new List<BFSItem>();
    public static void Challenge1()
    {
        string fileName = "Day16\\input2.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        List<Valve> valves = new();
        //List<Flow> flows = new();
        List<string> flows = new();
        var emptyValve = new Valve();
        foreach (var row in input)
        {
            var splitRow = row.Split(' ');
            var valve = new Valve();
            valve.Name = splitRow[1];
            valve.Flow = short.Parse(splitRow[4].Substring(5).TrimEnd(';'));
            valve.Tunnels = new Dictionary<string, Valve?>();
            for (int i = 9; i < splitRow.Length; i++)
            {
                valve.Tunnels.Add(splitRow[i].TrimEnd(','), null);
            }
            valves.Add(valve);
        }
        foreach (var valve in valves)
        {
            foreach (var tunnel in valve.Tunnels)
            {
                valve.Tunnels[tunnel.Key] = valves.Find(v => v.Name == tunnel.Key);
            }
            if (valve.Flow > 0)
            {
                flows.Add(new string(valve.Name));
                //flows.Add(new Flow() { Name= valve.Name,Value=valve.Flow });
            }
        }
        var output = FindMostPressure(valves, flows);


        //Console.WriteLine(a.Name + " " + b.Name);
        Console.WriteLine("Challenge 1: " + output);
    }

    public static void Challenge2()
    {
        string fileName = "Day16\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);

        var output = 0;

        Console.WriteLine("Challenge 2: " + output);
    }

    //public static int FindMostPressure(List<Valve> valves, List<Flow> flows)
    public static int FindMostPressure(List<Valve> valves, List<string> flows)
    {
        //var result = new List<int>();
        //var visited = new HashSet<string>();
        //var queue = new List<BFSItem>
        //{
        //    new BFSItem() { Valve = valves.Find(a => a.Name == "AA"), ActiveFlows = flows, TimeRemaining = 30, Release = 0 }
        //};
        queue.Add(new BFSItem() { Valve = valves.Find(a => a.Name == "AA"), ActiveFlows = flows, TimeRemaining = 30, Release = 0 });
        BFSItem current;
        //for (int start = 0; start < 4; start++)
        while (queue.Count > 0)
        {
            current = queue[0];
            queue.RemoveAt(0);
            if (current.TimeRemaining == 0)
            {
                result.Add(current.Release);
                continue;
            }
            if (current.ActiveFlows.Contains(current.Valve.Name))
            {
                current.ActiveFlows.Remove(current.Valve.Name);
                var temp = new BFSItem(current);
                //temp.ActiveFlows.Remove(current.Valve.Name);
                //temp.ActiveFlows = CopyWithout(current.ActiveFlows, current.Valve.Name);
                temp.Release += current.Valve.Flow * current.TimeRemaining;
                temp.TimeRemaining--;
                queue.Add((BFSItem)temp);
            }
            //for (int i = 0; i < current.Valve.Tunnels.Count; i++)
            foreach (var tunnel in current.Valve.Tunnels)
            {
                bool haveVisitedBefore = current.Visited.TryGetValue(current.Valve.Name + tunnel.Key, out int timesVisited);
                if (timesVisited >= 5)
                {
                    continue;
                }
                var temp = new BFSItem(current);
                temp.Valve = tunnel.Value;
                temp.ActiveFlows = Copy(current.ActiveFlows);
                temp.Release = current.Release;
                temp.TimeRemaining = current.TimeRemaining - 1;
                if (haveVisitedBefore)
                    temp.Visited[current.Valve.Name + tunnel.Key]++;
                else
                    temp.Visited.Add(current.Valve.Name + tunnel.Key, 1);
                queue.Add((BFSItem)temp);
            }

        };

        return result.OrderDescending().First();
    }

    public static void FindMostPressureParallell(List<Valve> valves, List<string> flows, short task)
    {
        BFSItem current;
        while (queue.Count > 0)
        {
            current = queue[0];
            queue.RemoveAt(0);
            if (current.TimeRemaining == 0)
            {
                result.Add(current.Release);
                continue;
            }
            if (current.ActiveFlows.Contains(current.Valve.Name))
            {
                current.ActiveFlows.Remove(current.Valve.Name);
                var temp = new BFSItem(current);
                temp.Release += current.Valve.Flow * current.TimeRemaining;
                temp.TimeRemaining--;
                queue.Add((BFSItem)temp);
            }
            foreach (var tunnel in current.Valve.Tunnels)
            {
                bool haveVisitedBefore = current.Visited.TryGetValue(current.Valve.Name + tunnel.Key, out int timesVisited);
                if (timesVisited >= 1)
                {
                    continue;
                }
                var temp = new BFSItem(current);
                temp.Valve = tunnel.Value;
                temp.ActiveFlows = Copy(current.ActiveFlows);
                temp.Release = current.Release;
                temp.TimeRemaining = current.TimeRemaining - 1;
                if (haveVisitedBefore)
                    temp.Visited[current.Valve.Name + tunnel.Key]++;
                else
                    temp.Visited.Add(current.Valve.Name + tunnel.Key, 1);
                queue.Add((BFSItem)temp);
            }
        };
        Console.WriteLine("Task " + task + " done!");
    }

    //public static List<Flow> Copy(List<Flow> flows)
    //{
    //    var output = new List<Flow>();
    //    foreach (var a in flows)
    //    {
    //        output.Add(new Flow(a));
    //    }
    //    return output;
    //}

    public static List<string> Copy(List<string> flows)
    {
        var output = new List<string>();
        foreach (var a in flows)
        {
            output.Add(a.Substring(0));
            //output.Add(new Flow(a));
        }
        return output;
    }

    public static List<string> CopyWithout(List<string> flows, string name)
    {
        var output = new List<string>();
        foreach (var a in flows)
        {
            if (a != name)
            {
                output.Add(new string(a));
                //output.Add(new Flow(a));
            }
        }
        return output;
    }
}

class Valve
{
    public string? Name { get; set; }
    public int Flow { get; set; }
    public Dictionary<string, Valve?>? Tunnels { get; set; }
}

//class Flow
//{
//    //public string Name;
//    //public short Value;
//    public string? Name { get; set; }
//    public short Value { get; set; }

//    public Flow(Flow flow)
//    {
//        Name = flow.Name;
//        Value = flow.Value;
//    }

//    public Flow()
//    {
//    }
//}

class BFSItem
{
    public Valve? Valve;// { get; set; }
    public int TimeRemaining;// { get; set; }
    public int Release = 0;// { get; set; }
    public List<string>? ActiveFlows;// { get; set; }
    //public List<Flow>? ActiveFlows { get; set; }
    public Dictionary<string, int> Visited = new Dictionary<string, int>();


    public BFSItem ()
    {
    }

    public BFSItem (BFSItem bFSItem)
    {
        Valve = bFSItem.Valve;
        TimeRemaining = bFSItem.TimeRemaining;
        Release= bFSItem.Release;
        ActiveFlows = new List<string>();
        foreach (var a in bFSItem.ActiveFlows)
        {
            ActiveFlows.Add(a.Substring(0));
        }
        Visited = new();
        foreach (var a in bFSItem.Visited)
        {
            Visited.Add(a.Key, a.Value);
        }
    }
}

//[Flags]
//public enum ValveFlags
//{
//    None = 0b00000000000000000000000000000000000000000000000000,
//    DJ = 0b00000000000000000000000000000000000000000000000001,
//    LP = 0b00000000000000000000000000000000000000000000000010,
//    GT = 0b00000000000000000000000000000000000000000000000100,
//    RO = 0b00000000000000000000000000000000000000000000001000,
//    PS = 0b00000000000000000000000000000000000000000000010000,
//    QV = 0b00000000000000000000000000000000000000000000100000,
//    MV = 0b00000000000000000000000000000000000000000001000000,
//    RN = 0b00000000000000000000000000000000000000000010000000,
//    HF = 0b00000000000000000000000000000000000000000100000000,
//    PY = 0b00000000000000000000000000000000000000001000000000,
//    AT = 0b00000000000000000000000000000000000000010000000000,
//    UY = 0b00000000000000000000000000000000000000100000000000,
//    YI = 0b00000000000000000000000000000000000001000000000000,
//    EB = 0b00000000000000000000000000000000000010000000000000,
//    ID = 0b00000000000000000000000000000000000100000000000000,
//    FY = 0b00000000000000000000000000000000001000000000000000,
//    GQ = 0b00000000000000000000000000000000010000000000000000,
//    HW = 0b00000000000000000000000000000000100000000000000000,
//    CQ = 0b00000000000000000000000000000001000000000000000000,
//    AW = 0b00000000000000000000000000000010000000000000000000,
//    BV = 0b00000000000000000000000000000100000000000000000000,
//    PB = 0b00000000000000000000000000001000000000000000000000,
//    MX = 0b00000000000000000000000000010000000000000000000000,
//    DE = 0b00000000000000000000000000100000000000000000000000,
//    AA = 0b00000000000000000000000001000000000000000000000000,
//    QN = 0b00000000000000000000000010000000000000000000000000,
//    GO = 0b00000000000000000000000100000000000000000000000000,
//    PZ = 0b00000000000000000000001000000000000000000000000000,
//    PG = 0b00000000000000000000010000000000000000000000000000,
//    FL = 0b00000000000000000000100000000000000000000000000000,
//    DS = 0b00000000000000000001000000000000000000000000000000,
//    ZH = 0b00000000000000000010000000000000000000000000000000,
//    KV = 0b00000000000000000000000000000000000000000000000000,
//    UV = 0b00000000000000000000000000000000000000000000000000,
//    WH = 0b00000000000000000000000000000000000000000000000000,
//    FD = 0b00000000000000000000000000000000000000000000000000,
//    FJ = 0b00000000000000000000000000000000000000000000000000,
//    JT = 0b00000000000000000000000000000000000000000000000000,
//    SN = 0b00000000000000000000000000000000000000000000000000,
//    KM = 0b00000000000000000000000000000000000000000000000000,
//    LQ = 0b00000000000000000000000000000000000000000000000000,
//    NO = 0b00000000000000000000000000000000000000000000000000,
//    SB = 0b00000000000000000000000000000000000000000000000000,
//    MK = 0b00000000000000000000000000000000000000000000000000,
//    YG = 0b00000000000000000000000000000000000000000000000000,
//    IJ = 0b00000000000000000000000000000000000000000000000000,
//    EP = 0b00000000000000000000000000000000000000000000000000,
//    MM = 0b00000000000000000000000000000000000000000000000000,
//    YQ = 0b00000000000000000000000000000000000000000000000000,
//    EE = 0b00000000000000000000000000000000000000000000000000
//}