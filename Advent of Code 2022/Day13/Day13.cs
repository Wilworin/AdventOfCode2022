using System.Net.Sockets;
using static System.Net.Mime.MediaTypeNames;

class Day13
{
    public static void Challenge1()
    {
        string fileName = "Day13\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        var output = 0;
        var pair = 1;
        bool test;
        //CompareNumbers("[[[],[3,2,10]]]", "[[[],2,[],1,[4,1]],[3,[[8,4,0,7,8],4,2]],[[9,[2,1,8,2],[6,0,3,1,1]],4],[10,2,2]]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[],[3,2,10]", "[],2,[],1,[4,1]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[1,1,3,1,1]", "[1,1,5,1,1]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[[1],[2,3,4]]", "[[1],4]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[9]", "[[8,7,6]]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[[4,4],4,4]", "[[4,4],4,4,4]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[7,7,7,7]", "[7,7,7]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[]", "[3]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[[[]]]", "[[]]", out test);
        //Console.WriteLine(test);
        //CompareNumbers("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", out test);
        //Console.WriteLine(test);
        //Console.WriteLine(TrimStartEnd("[1,1,3,1,1]"));
        //Console.WriteLine(":" +TrimStartEnd("[]") + ":");
        //Console.WriteLine(int.Parse("".Split(',')[0]));
        for (int row = 0; row < input.Length; row += 3)
        {
            var result = ParsePacketPairs(input[row], input[row + 1]);
            if (result)
            {
                output += pair;
            }
            Console.WriteLine(pair + " : " + result);
            pair++;
        }
        // 5735 Too high
        Console.WriteLine("Challenge 1: " + output);
    }

    public static void Challenge2()
    {
        string fileName = "Day13\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        var pairs = new List<Packet>();
        var output = 0;

        for (int row = 0; row < input.Length; row += 3)
        {
            pairs.Add(new Packet(input[row]));
            pairs.Add(new Packet(input[row+1]));
        }
        pairs.Add(new Packet("[[2]]"));
        pairs.Add(new Packet("[[6]]"));
        var ordered = pairs.OrderBy(a => a).ToList();

        var counter = 1;
        var pair1 = 0;
        var pair2 = 0;
        foreach (var pair in ordered)
        {
            if (pair.Text == "[[2]]")
                pair1 = counter;
            if (pair.Text == "[[6]]")
                pair2 = counter;
            counter++;
        }
        output = pair1 * pair2;

        //foreach (var packet in ordered)
        //    Console.WriteLine(packet.Text);
        //Console.WriteLine(pairs.Count);
        Console.WriteLine("Challenge 2: " + output); // 22110
    }

    private static bool ParsePacketPairs (string left, string right)
    {
        CompareNumbers(left, right, out bool result);
        return result;
    }

    private static bool SortPacketPairs(string left, string right)
    {
        CompareNumbers(left, right, out bool result);
        return result;
    }

    private static bool CompareNumbers (string left, string right, out bool result) // v4
    {
        Console.WriteLine("Left: " + left);
        Console.WriteLine("Right: " + right);
        if (string.IsNullOrEmpty(left) && string.IsNullOrEmpty(right)) { result = true; return true; }
        if (string.IsNullOrEmpty(left)) { result = true; return false; }
        if (string.IsNullOrEmpty(right)) { result = false; return false; }
        if (left[0] == '[' && right[0] == '[') // Both are lists
        {
            var truncatedLeft = GetInnerList(left, out var lRest);
            var truncatedRight = GetInnerList(right, out var rRest);

            if (!CompareNumbers(truncatedLeft, truncatedRight, out result)) { return false; }
            else
            {
                if (!CompareNumbers(lRest, rRest, out result)) { return false; }
                else { return true; }
            }
        }
        if (left[0] == '[') // Only left is a list
        {
            var split = GetInnerElement(right, out string rest);
            split = "[" + split+ "]" + rest;
            if (!CompareNumbers(left, split, out result)) { return false; }
                else { return true; }
        }
        if (right[0] == '[') // Only right is a list
        {
            var split = GetInnerElement(left, out string rest);
            split = "[" + split + "]" + rest;
            if (!CompareNumbers(split, right, out result)) { return false; }
            else { return true; }
        }

        // Neither side is a list
        //Console.WriteLine("LeftSplit: " + leftSplit + "   LeftRest: " + lRest);
        //Console.WriteLine("RightSplit: "+ rightSplit + "   RightRest: " + rRest);
        var leftSplit = GetInnerElement(left, out string leftRest);
        var rightSplit = GetInnerElement(right, out string rightRest);
        int.TryParse(leftSplit, out int leftInt);
        int.TryParse(rightSplit, out int rightInt);
        if (leftInt > rightInt)
        {
            result = false;
            return false;
        }
        if (leftInt < rightInt)
        {
            result = true;
            return false;
        }
        if (!CompareNumbers(leftRest, rightRest, out result)) { return false; }
        else { return true; }
    }

    private static string GetInnerElement(string text, out string rest)
    {
        rest = string.Empty;
        if (string.IsNullOrEmpty(text)) { return text; }
        var split = text;
        var pos = text.IndexOf(',');
        if (pos != -1)
        {
            split = text.Substring(0, pos);
            if (text.Length > split.Length + 1)
                rest = text.Substring(pos + 1);
        }
        return split;
    }

    private static string TrimStartEnd (string text)
    {
        if (string.IsNullOrEmpty(text)) { return text; }
        if (text[0] == '[')
            text = text.Substring(1);
        if (text.Length > 0 && text[text.Length - 1] == ']')
            text = text.Substring(0, text.Length - 1);
        return text;
    }

    private static string GetInnerList (string text, out string rest)
    {
        if (string.IsNullOrEmpty(text)) { rest = string.Empty; return text; }
        if (text[0] != '[') { rest = string.Empty; return text; }
        var counter = 0;
        var pos = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '[') 
            {
                counter++; 
            }
            if (text[i] == ']')
            {
                counter--;
                if (counter == 0)
                {
                    pos = i;
                    break;
                }
            }
        }
        var output = text.Substring(1, pos - 1);
        if (text.Length > output.Length + 2)
            rest = text.Substring(pos + 2);
        else 
            rest = string.Empty;
        return output;

    }

    //private static bool CompareNumbers(string left, string right, out bool result)  // v3
    //{
    //    //Console.WriteLine("Left: " + left);
    //    //Console.WriteLine("Right: " + right);
    //    //left = TrimStartEnd(left);
    //    //right = TrimStartEnd(right);
    //    if (string.IsNullOrEmpty(left) && string.IsNullOrEmpty(right)) { result = true; return true; }
    //    if (string.IsNullOrEmpty(left)) { result = true; return false; }
    //    if (string.IsNullOrEmpty(right)) { result = false; return false; }
    //    if (left[0] == '[' || right[0] == '[')
    //    {
    //        var truncatedLeft = GetInnerList(left, out var leftRest);
    //        var truncatedRight = GetInnerList(right, out var rightRest);

    //        if (!CompareNumbers(truncatedLeft, truncatedRight, out result)) { return false; }
    //        else
    //        {
    //            if (!CompareNumbers(leftRest, rightRest, out result)) { return false; }
    //            else { return true; }
    //        }
    //    }

    //    var lRest = "";
    //    var rRest = "";
    //    var leftSplit = left;
    //    var rightSplit = right;
    //    var leftPos = left.IndexOf(',');
    //    var rightPos = right.IndexOf(',');
    //    if (leftPos != -1)
    //    {
    //        leftSplit = left.Substring(0, leftPos);
    //        if (left.Length > leftSplit.Length + 1)
    //            lRest = left.Substring(leftPos + 1);
    //    }
    //    if (rightPos != -1)
    //    {
    //        rightSplit = right.Substring(0, rightPos);
    //        if (right.Length > rightSplit.Length + 1)
    //            rRest = right.Substring(rightPos + 1);
    //    }
    //    //Console.WriteLine("LeftSplit: " + leftSplit + "   LeftRest: " + lRest);
    //    //Console.WriteLine("RightSplit: "+ rightSplit + "   RightRest: " + rRest);
    //    int.TryParse(leftSplit, out int leftInt);
    //    int.TryParse(rightSplit, out int rightInt);
    //    if (leftInt > rightInt)
    //    {
    //        result = false;
    //        return false;
    //    }
    //    if (leftInt < rightInt)
    //    {
    //        result = true;
    //        return false;
    //    }
    //    if (!CompareNumbers(lRest, rRest, out result)) { return false; }
    //    else { return true; }


    //    //var leftSplit = left.Split(',');
    //    //var rightSplit = right.Split(',');
    //    //var leftLength = leftSplit.Length;
    //    //var rightLenght = rightSplit.Length;
    //    //for (int i = 0; i < leftLength; i++)
    //    //{
    //    //    if (i >= rightLenght)
    //    //    {
    //    //        result = false;
    //    //        return false;
    //    //    }
    //    //    if (!int.TryParse(leftSplit[i], out int leftInt) || !int.TryParse(rightSplit[i], out int rightInt))
    //    //    {
    //    //        if (!CompareNumbers(leftSplit[i], rightSplit[i], out result))
    //    //        { return result; }
    //    //        continue;
    //    //    }
    //    //    if (leftInt > rightInt)
    //    //    {
    //    //        result = false;
    //    //        return false;
    //    //    }
    //    //    if (leftInt < rightInt)
    //    //    {
    //    //        result = true;
    //    //        return false;
    //    //    }
    //    //}
    //    //result = true;
    //    //return true;
    //    result = true;
    //    return true;
    //}

    //private static bool CompareNumbers(string left, string right, out bool result)  // v2
    //{
    //    left = TrimStartEnd(left);
    //    right = TrimStartEnd(right);
    //    if (string.IsNullOrEmpty(left)) { result = true; return false; }
    //    if (string.IsNullOrEmpty(right)) { result = false; return false; }
    //    if (left[0] == '[' || right[0] == '[')
    //    {
    //        CompareNumbers(left, right, out result);
    //        return result;
    //        //if (!CompareNumbers(left, right, out result))
    //        //{ return result; }
    //    }
    //    var leftSplit = left.Split(',');
    //    var rightSplit = right.Split(',');
    //    var leftLength = leftSplit.Length;
    //    var rightLenght = rightSplit.Length;
    //    for (int i = 0; i < leftLength; i++)
    //    {
    //        if (i >= rightLenght)
    //        {
    //            result = false;
    //            return false;
    //        }
    //        if (!int.TryParse(leftSplit[i], out int leftInt) || !int.TryParse(rightSplit[i], out int rightInt))
    //        {
    //            if (!CompareNumbers(leftSplit[i], rightSplit[i], out result))
    //            { return result; }
    //            continue;
    //        }
    //        if (leftInt > rightInt)
    //        {
    //            result = false;
    //            return false;
    //        }
    //        if (leftInt < rightInt)
    //        {
    //            result = true;
    //            return false;
    //        }
    //    }
    //    result = true;
    //    return true;
    //}

    //private static bool CompareNumbers(string left, string right) // v1
    //{
    //    var output = true;
    //    left = TrimStartEnd(left);
    //    right = TrimStartEnd(right);
    //    if (string.IsNullOrEmpty(left)) { return true; }
    //    if (string.IsNullOrEmpty(right)) { return false; }
    //    if (left[0] == '[' || right[0] == '[')
    //    {
    //        output = CompareNumbers(left, right);
    //        if (!output) { return false; }
    //    }
    //    var leftSplit = left.Split(',');
    //    var rightSplit = right.Split(',');
    //    var leftLength = leftSplit.Length;
    //    var rightLenght = rightSplit.Length;
    //    for (int i = 0; i < leftLength; i++)
    //    {
    //        if (i >= rightLenght)
    //            return false;
    //        if (!int.TryParse(leftSplit[i], out int leftInt) || !int.TryParse(rightSplit[i], out int rightInt))
    //        {
    //            if (!CompareNumbers(leftSplit[i], rightSplit[i]))
    //            { return false; }
    //            continue;
    //        }
    //        if (leftInt > rightInt)
    //            return false;
    //    }
    //    return output;
    //}
}

public class Packet : IComparable
{
    public string Text { get; set; }

    public Packet (string text)
    {
        Text = text;
    }

    public int CompareTo(object x)
    {
        Packet o = x as Packet;
        CompareNumbers(this.Text, o.Text, out bool result);
        return result ? -1 : 1;
    }

    private bool CompareNumbers(string left, string right, out bool result) // v4
    {
        //Console.WriteLine("Left: " + left);
        //Console.WriteLine("Right: " + right);
        if (string.IsNullOrEmpty(left) && string.IsNullOrEmpty(right)) { result = true; return true; }
        if (string.IsNullOrEmpty(left)) { result = true; return false; }
        if (string.IsNullOrEmpty(right)) { result = false; return false; }
        if (left[0] == '[' && right[0] == '[') // Both are lists
        {
            var truncatedLeft = GetInnerList(left, out var lRest);
            var truncatedRight = GetInnerList(right, out var rRest);

            if (!CompareNumbers(truncatedLeft, truncatedRight, out result)) { return false; }
            else
            {
                if (!CompareNumbers(lRest, rRest, out result)) { return false; }
                else { return true; }
            }
        }
        if (left[0] == '[') // Only left is a list
        {
            var split = GetInnerElement(right, out string rest);
            split = "[" + split + "]" + rest;
            if (!CompareNumbers(left, split, out result)) { return false; }
            else { return true; }
        }
        if (right[0] == '[') // Only right is a list
        {
            var split = GetInnerElement(left, out string rest);
            split = "[" + split + "]" + rest;
            if (!CompareNumbers(split, right, out result)) { return false; }
            else { return true; }
        }

        // Neither side is a list
        var leftSplit = GetInnerElement(left, out string leftRest);
        var rightSplit = GetInnerElement(right, out string rightRest);
        int.TryParse(leftSplit, out int leftInt);
        int.TryParse(rightSplit, out int rightInt);
        if (leftInt > rightInt)
        {
            result = false;
            return false;
        }
        if (leftInt < rightInt)
        {
            result = true;
            return false;
        }
        if (!CompareNumbers(leftRest, rightRest, out result)) { return false; }
        else { return true; }
    }

    private string GetInnerElement(string text, out string rest)
    {
        rest = string.Empty;
        if (string.IsNullOrEmpty(text)) { return text; }
        var split = text;
        var pos = text.IndexOf(',');
        if (pos != -1)
        {
            split = text.Substring(0, pos);
            if (text.Length > split.Length + 1)
                rest = text.Substring(pos + 1);
        }
        return split;
    }

    private string GetInnerList(string text, out string rest)
    {
        if (string.IsNullOrEmpty(text)) { rest = string.Empty; return text; }
        if (text[0] != '[') { rest = string.Empty; return text; }
        var counter = 0;
        var pos = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '[')
            {
                counter++;
            }
            if (text[i] == ']')
            {
                counter--;
                if (counter == 0)
                {
                    pos = i;
                    break;
                }
            }
        }
        var output = text.Substring(1, pos - 1);
        if (text.Length > output.Length + 2)
            rest = text.Substring(pos + 2);
        else
            rest = string.Empty;
        return output;

    }
}

public class PacketCompare : IComparer<Packet>
{
    public int Compare (Packet x, Packet y)
    {
        CompareNumbers(x.Text, y.Text, out bool result);
        return result ? 1 : -1;
    }

    private bool CompareNumbers(string left, string right, out bool result) // v4
    {
        //Console.WriteLine("Left: " + left);
        //Console.WriteLine("Right: " + right);
        if (string.IsNullOrEmpty(left) && string.IsNullOrEmpty(right)) { result = true; return true; }
        if (string.IsNullOrEmpty(left)) { result = true; return false; }
        if (string.IsNullOrEmpty(right)) { result = false; return false; }
        if (left[0] == '[' && right[0] == '[') // Both are lists
        {
            var truncatedLeft = GetInnerList(left, out var lRest);
            var truncatedRight = GetInnerList(right, out var rRest);

            if (!CompareNumbers(truncatedLeft, truncatedRight, out result)) { return false; }
            else
            {
                if (!CompareNumbers(lRest, rRest, out result)) { return false; }
                else { return true; }
            }
        }
        if (left[0] == '[') // Only left is a list
        {
            var split = GetInnerElement(right, out string rest);
            split = "[" + split + "]" + rest;
            if (!CompareNumbers(left, split, out result)) { return false; }
            else { return true; }
        }
        if (right[0] == '[') // Only right is a list
        {
            var split = GetInnerElement(left, out string rest);
            split = "[" + split + "]" + rest;
            if (!CompareNumbers(split, right, out result)) { return false; }
            else { return true; }
        }

        // Neither side is a list
        var leftSplit = GetInnerElement(left, out string leftRest);
        var rightSplit = GetInnerElement(right, out string rightRest);
        int.TryParse(leftSplit, out int leftInt);
        int.TryParse(rightSplit, out int rightInt);
        if (leftInt > rightInt)
        {
            result = false;
            return false;
        }
        if (leftInt < rightInt)
        {
            result = true;
            return false;
        }
        if (!CompareNumbers(leftRest, rightRest, out result)) { return false; }
        else { return true; }
    }

    private string GetInnerElement(string text, out string rest)
    {
        rest = string.Empty;
        if (string.IsNullOrEmpty(text)) { return text; }
        var split = text;
        var pos = text.IndexOf(',');
        if (pos != -1)
        {
            split = text.Substring(0, pos);
            if (text.Length > split.Length + 1)
                rest = text.Substring(pos + 1);
        }
        return split;
    }

    private string GetInnerList(string text, out string rest)
    {
        if (string.IsNullOrEmpty(text)) { rest = string.Empty; return text; }
        if (text[0] != '[') { rest = string.Empty; return text; }
        var counter = 0;
        var pos = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '[')
            {
                counter++;
            }
            if (text[i] == ']')
            {
                counter--;
                if (counter == 0)
                {
                    pos = i;
                    break;
                }
            }
        }
        var output = text.Substring(1, pos - 1);
        if (text.Length > output.Length + 2)
            rest = text.Substring(pos + 2);
        else
            rest = string.Empty;
        return output;

    }
}