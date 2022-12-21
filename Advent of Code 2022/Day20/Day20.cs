using System.Xml.Linq;

class Day20
{
    public static void Challenge1()
    {
        var output = 0;
        string fileName = "Day20\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        var mixing = new List<Node>();
        for (int i = 0; i < input.Length; i++)
        {
            mixing.Add(new Node() { Id = i, Value = int.Parse(input[i]) });
        }
        //Print(mixing);
        MixObjects(mixing);

        //Print(mixing);
        output += FindNthAfter(mixing, 1000, 0);
        output += FindNthAfter(mixing, 2000, 0);
        output += FindNthAfter(mixing, 3000, 0);

        Console.WriteLine("Challenge 1: " + output);  // 13967
    }

    private static void MixObjects(List<Node> mixing)
    {
        int max = mixing.Count - 1;
        for (int i = 0; i < mixing.Count; i++) // 
        {
            int pos = mixing.FindIndex(m => m.Id == i);
            int value = mixing[pos].Value;
            if (value == 0) { continue; }
            int newPos = pos + (value % max);
            if (newPos == 0) { Console.WriteLine("Pos zero"); }
            if (newPos >= max)
            {
                newPos = newPos % max;
                mixing.RemoveAt(pos);
                mixing.Insert(newPos, new Node() { Id = i, Value = value });
            }
            else if (newPos <= 0)
            {
                newPos = max + newPos;
                mixing.RemoveAt(pos);
                mixing.Insert(newPos, new Node() { Id = i, Value = value });
            }
            else
            {
                mixing.RemoveAt(pos);
                mixing.Insert(newPos, new Node() { Id = i, Value = value });
            }
        }
    }

    public static void Challenge2()
    {
        long output = 0;
        string fileName = "Day20\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        var mixing = new List<NodeLong>();
        for (int i = 0; i < input.Length; i++)
        {
            mixing.Add(new NodeLong() { Id = i, Value = long.Parse(input[i]) * 811589153 });
        }
        //Print(mixing);
        for(int i = 0; i < 10; i++)
        {
            MixObjects(mixing);
        }
        //Print(mixing);
        output += FindNthAfter(mixing, 1000, 0);
        output += FindNthAfter(mixing, 2000, 0);
        output += FindNthAfter(mixing, 3000, 0);
        Console.WriteLine("Challenge 2: " + output);
    }

    private static int FindNthAfter(List<Node> nodes, int nth, int value)
    {
        int pos = nodes.FindIndex(n=> n.Value == value);
        int newPos = pos + nth;
        newPos = newPos % nodes.Count;
        return nodes[newPos].Value;
    }

    private static long FindNthAfter(List<NodeLong> nodes, int nth, long value)
    {
        int pos = nodes.FindIndex(n => n.Value == value);
        int newPos = pos + nth;
        newPos = newPos % nodes.Count;
        return nodes[newPos].Value;
    }

    private static void MixObjects(List<NodeLong> mixing)
    {
        int max = mixing.Count - 1;
        for (int i = 0; i < mixing.Count; i++) // 
        {
            int pos = mixing.FindIndex(m => m.Id == i);
            long value = mixing[pos].Value;
            if (value == 0) { continue; }
            int newPos = pos + (int)(value % max);
            if (newPos == 0) { Console.WriteLine("Pos zero"); }
            if (newPos >= max)
            {
                newPos = newPos % max;
                mixing.RemoveAt(pos);
                mixing.Insert(newPos, new NodeLong() { Id = i, Value = value });
            }
            else if (newPos <= 0)
            {
                newPos = max + newPos;
                mixing.RemoveAt(pos);
                mixing.Insert(newPos, new NodeLong() { Id = i, Value = value });
            }
            else
            {
                mixing.RemoveAt(pos);
                mixing.Insert(newPos, new NodeLong() { Id = i, Value = value });
            }
        }
    }

    private static void Print(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            Console.WriteLine("ID: " + node.Id + "   Value: " + node.Value);
        }
        Console.WriteLine();
    }

    private static void Print(List<NodeLong> nodes)
    {
        foreach (NodeLong node in nodes)
        {
            Console.WriteLine("ID: " + node.Id + "   Value: " + node.Value);
        }
        Console.WriteLine();
    }
}

class Node
{
    public int Id;
    public int Value;
}

class NodeLong
{
    public int Id;
    public long Value;
}