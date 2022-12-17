using System.Runtime.CompilerServices;

class Day15
{
    public static void Challenge1()
    {
        List<Sensor> sensors = new();
        HashSet<int> setOfXs = new HashSet<int>();
        string fileName = "Day15\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        foreach (var item in input )
        {
            var sensor = ParseInputIntoSensorBeacon(item);
            sensor.MinX = MinX(sensor);
            sensor.MaxX = MaxX(sensor);
            sensors.Add(sensor);
        }
        var output = 0;

        foreach (var sensor in sensors)
        {
            for (int i = sensor.MinX; i <= sensor.MaxX; i++)
            {
                setOfXs.Add(i);
            }
        }
        output = setOfXs.Count() - 1;
        Console.WriteLine("Challenge 1: " + output);
    }

    //public static void Challenge2()
    //{
    //    List<Sensor> sensors = new();
    //    string fileName = "Day15\\input.txt";
    //    var input = Helper.ReadFileIntoStringArray(fileName);
    //    foreach (var item in input)
    //    {
    //        var sensor = ParseInputIntoSensorBeacon(item);
    //        sensors.Add(sensor);
    //    }
    //    var output = 0;

    //    for (int y = 0; y <= 4000000; y++)
    //    {
    //        HashSet<int> setOfXs = new HashSet<int>();
    //        for (int i = 0; i < sensors.Count; i++)
    //        {
    //            sensors[i].MinX = Math.Max(MinX(sensors[i], y), 0);
    //            sensors[i].MaxX = Math.Min(MaxX(sensors[i], y), 4000000);
    //        }
    //        for (int i = 0; i < sensors.Count; i++)
    //        {
    //            for (int j = sensors[i].MinX; j <= sensors[i].MaxX; j++)
    //            {
    //                setOfXs.Add(j);
    //            }
    //        }
    //        for (int x = 0; x <= 4000000; x++)
    //        {
    //            if(!setOfXs.Contains(x))
    //            {
    //                Console.WriteLine("X: " + x + "  Y: " + y);
    //            }
    //        }
    //        if (y % 100 == 0)
    //            Console.WriteLine(y);
    //    }
    //    Console.WriteLine("Challenge 2: " + output);
    //}

    public static void Challenge2()  // 13743542639657  !!!!! FOUND IT AT X: 3435885  Y: 2639657 !!!!!
    {
        List<Sensor> sensors = new();
        List<Sensor> sensors2 = new();
        List<Sensor> sensors3 = new();
        List<Sensor> sensors4 = new();
        List<Sensor> sensors5 = new();
        List<Sensor> sensors6 = new();
        List<Sensor> sensors7 = new();
        List<Sensor> sensors8 = new();
        List<Sensor> sensors9 = new();
        List<Sensor> sensors10 = new();
        List<Sensor> sensors11 = new();
        //HashSet<int> setOfXs = new HashSet<int>();
        string fileName = "Day15\\input.txt";
        var input = Helper.ReadFileIntoStringArray(fileName);
        foreach (var item in input)
        {
            var sensor = ParseInputIntoSensorBeacon(item);
            sensors.Add(sensor);
            sensors2.Add(new Sensor(sensor));
            sensors3.Add(new Sensor(sensor));
            sensors4.Add(new Sensor(sensor));
            sensors5.Add(new Sensor(sensor));
            sensors6.Add(new Sensor(sensor));
            sensors7.Add(new Sensor(sensor));
            sensors8.Add(new Sensor(sensor));
            sensors9.Add(new Sensor(sensor));
            sensors10.Add(new Sensor(sensor));
            sensors11.Add(new Sensor(sensor));
        }
        var output = 0;

        Parallel.Invoke(() =>
        {
            Console.WriteLine("Begin first task...");
            SearchParallell(sensors.ToList(), 2000000, 4000000, 4000, 400000, 1);
        },  // close first Action
        () =>
        {
            Console.WriteLine("Begin second task...");
            SearchParallell(sensors2, 2000000, 4000000, 400000, 800000, 2);
        },
        () =>
        {
            Console.WriteLine("Begin third task...");
            SearchParallell(sensors3, 2000000, 4000000, 800000, 1200000, 3);
        },
        () =>
        {
            Console.WriteLine("Begin fourth task...");
            SearchParallell(sensors4, 2000000, 4000000, 1200000, 1600000, 4);
        },
        () =>
        {
            Console.WriteLine("Begin fift task...");
            SearchParallell(sensors5, 2000000, 4000000, 1600000, 2000000, 5);
        },
        () =>
        {
            Console.WriteLine("Begin sixt task...");
            SearchParallell(sensors6, 2000000, 4000000, 2000000, 2400000, 6);
        },
        () =>
        {
            Console.WriteLine("Begin seventh task...");
            SearchParallell(sensors7, 2000000, 4000000, 2400000, 2800000, 7);
        },
        () =>
        {
            Console.WriteLine("Begin eighth task...");
            SearchParallell(sensors8, 2000000, 4000000, 2800000, 3100000, 8);
        },
        () =>
        {
            Console.WriteLine("Begin ninth task...");
            SearchParallell(sensors9, 2000000, 4000000, 3100000, 3300000, 9);
        },
        () =>
        {
            Console.WriteLine("Begin tenth task...");
            SearchParallell(sensors10, 2000000, 4000000, 3300000, 3600000, 10);
        },
        () =>
        {
            Console.WriteLine("Begin eleventh task...");
            SearchParallell(sensors11, 2000000, 4000000, 3700000, 4000000, 11);
        });

        Console.WriteLine("Challenge 2: " + output);
    }

    //public static void Challenge2()
    //{
    //    List<Sensor> sensors = new();
    //    HashSet<int> setOfXs = new HashSet<int>();
    //    string fileName = "Day15\\input.txt";
    //    var input = Helper.ReadFileIntoStringArray(fileName);
    //    foreach (var item in input)
    //    {
    //        var sensor = ParseInputIntoSensorBeacon(item);
    //        sensors.Add(sensor);
    //    }
    //    var output = 0;

    //    for (int y = 4000; y <= 4000000; y++)
    //    {
    //        //foreach (var sensor in sensors)
    //        for (int i = 0; i < sensors.Count; i++)
    //        {
    //            sensors[i].MinX = Math.Max(MinX(sensors[i], y), 0);
    //            sensors[i].MaxX = Math.Min(MaxX(sensors[i], y), 100000);
    //            //sensor.MinX = Math.Max(MinX(sensor, y), 0);
    //            //sensor.MaxX = Math.Min(MaxX(sensor, y), 4000000);
    //        }

    //        for (int x = 4000; x <= 100000; x++)
    //        {
    //            //bool found = false;
    //            //foreach (var sensor in sensors)
    //            //{
    //            //    if (x >= sensor.MinX && x <= sensor.MaxX)
    //            //    //if(x is >= sensor.MinX and <= sensor.MaxX)
    //            //    {
    //            //        found = true;
    //            //        break;
    //            //    }
    //            //}
    //            //if (!found)
    //            if (!(x >= sensors[0].MinX && x <= sensors[0].MaxX ||
    //                x >= sensors[1].MinX && x <= sensors[1].MaxX ||
    //                x >= sensors[2].MinX && x <= sensors[2].MaxX ||
    //                x >= sensors[3].MinX && x <= sensors[3].MaxX ||
    //                x >= sensors[4].MinX && x <= sensors[4].MaxX ||
    //                x >= sensors[5].MinX && x <= sensors[5].MaxX ||
    //                x >= sensors[6].MinX && x <= sensors[6].MaxX ||
    //                x >= sensors[7].MinX && x <= sensors[7].MaxX ||
    //                x >= sensors[8].MinX && x <= sensors[8].MaxX ||
    //                x >= sensors[9].MinX && x <= sensors[9].MaxX ||
    //                x >= sensors[10].MinX && x <= sensors[10].MaxX ||
    //                x >= sensors[11].MinX && x <= sensors[11].MaxX ||
    //                x >= sensors[12].MinX && x <= sensors[12].MaxX ||
    //                x >= sensors[13].MinX && x <= sensors[13].MaxX ||
    //                x >= sensors[14].MinX && x <= sensors[14].MaxX ||
    //                x >= sensors[15].MinX && x <= sensors[15].MaxX ||
    //                x >= sensors[16].MinX && x <= sensors[16].MaxX ||
    //                x >= sensors[17].MinX && x <= sensors[17].MaxX ||
    //                x >= sensors[18].MinX && x <= sensors[18].MaxX ||
    //                x >= sensors[19].MinX && x <= sensors[19].MaxX ||
    //                x >= sensors[20].MinX && x <= sensors[20].MaxX ||
    //                x >= sensors[21].MinX && x <= sensors[21].MaxX ||
    //                x >= sensors[22].MinX && x <= sensors[22].MaxX ||
    //                x >= sensors[23].MinX && x <= sensors[23].MaxX ||
    //                x >= sensors[24].MinX && x <= sensors[24].MaxX ||
    //                x >= sensors[25].MinX && x <= sensors[25].MaxX ||
    //                x >= sensors[26].MinX && x <= sensors[26].MaxX ||
    //                x >= sensors[27].MinX && x <= sensors[27].MaxX ||
    //                x >= sensors[28].MinX && x <= sensors[28].MaxX ||
    //                x >= sensors[29].MinX && x <= sensors[29].MaxX ||
    //                x >= sensors[30].MinX && x <= sensors[30].MaxX ||
    //                x >= sensors[31].MinX && x <= sensors[31].MaxX ||
    //                x >= sensors[32].MinX && x <= sensors[32].MaxX ||
    //                x >= sensors[33].MinX && x <= sensors[33].MaxX
    //                ))
    //            {
    //                Console.WriteLine("X: " + x + "  Y: " + y);
    //                output = x * 4000000 + y;
    //                break;
    //            }
    //        }
    //        if (y % 1000 == 0)
    //            Console.WriteLine(y);
    //    }
    //    Console.WriteLine("Challenge 2: " + output);
    //}

    public static int SearchParallell(List<Sensor> sensors, int startX, int endX, int startY, int endY, int task)
    {
        var output = 0;

        for (int y = startY; y <= endY; y++)
        {
            for (int i = 0; i < sensors.Count; i++)
            {
                sensors[i].MinX = Math.Max(MinX(sensors[i], y), startX);
                sensors[i].MaxX = Math.Min(MaxX(sensors[i], y), endX);
            }

            for (int x = startX; x <= endX; x++)
            {
                if (!(x >= sensors[0].MinX && x <= sensors[0].MaxX ||
                    x >= sensors[1].MinX && x <= sensors[1].MaxX ||
                    x >= sensors[2].MinX && x <= sensors[2].MaxX ||
                    x >= sensors[3].MinX && x <= sensors[3].MaxX ||
                    x >= sensors[4].MinX && x <= sensors[4].MaxX ||
                    x >= sensors[5].MinX && x <= sensors[5].MaxX ||
                    x >= sensors[6].MinX && x <= sensors[6].MaxX ||
                    x >= sensors[7].MinX && x <= sensors[7].MaxX ||
                    x >= sensors[8].MinX && x <= sensors[8].MaxX ||
                    x >= sensors[9].MinX && x <= sensors[9].MaxX ||
                    x >= sensors[10].MinX && x <= sensors[10].MaxX ||
                    x >= sensors[11].MinX && x <= sensors[11].MaxX ||
                    x >= sensors[12].MinX && x <= sensors[12].MaxX ||
                    x >= sensors[13].MinX && x <= sensors[13].MaxX ||
                    x >= sensors[14].MinX && x <= sensors[14].MaxX ||
                    x >= sensors[15].MinX && x <= sensors[15].MaxX ||
                    x >= sensors[16].MinX && x <= sensors[16].MaxX ||
                    x >= sensors[17].MinX && x <= sensors[17].MaxX ||
                    x >= sensors[18].MinX && x <= sensors[18].MaxX ||
                    x >= sensors[19].MinX && x <= sensors[19].MaxX ||
                    x >= sensors[20].MinX && x <= sensors[20].MaxX ||
                    x >= sensors[21].MinX && x <= sensors[21].MaxX ||
                    x >= sensors[22].MinX && x <= sensors[22].MaxX ||
                    x >= sensors[23].MinX && x <= sensors[23].MaxX ||
                    x >= sensors[24].MinX && x <= sensors[24].MaxX ||
                    x >= sensors[25].MinX && x <= sensors[25].MaxX ||
                    x >= sensors[26].MinX && x <= sensors[26].MaxX ||
                    x >= sensors[27].MinX && x <= sensors[27].MaxX ||
                    x >= sensors[28].MinX && x <= sensors[28].MaxX ||
                    x >= sensors[29].MinX && x <= sensors[29].MaxX ||
                    x >= sensors[30].MinX && x <= sensors[30].MaxX ||
                    x >= sensors[31].MinX && x <= sensors[31].MaxX ||
                    x >= sensors[32].MinX && x <= sensors[32].MaxX ||
                    x >= sensors[33].MinX && x <= sensors[33].MaxX
                    ))
                {
                    Console.WriteLine("!!!!! FOUND IT AT X: " + x + "  Y: " + y + " !!!!!");
                    output = x * 4000000 + y;
                    break;
                }
            }
            if (y % 10000 == 0)
                Console.WriteLine("Task" + task + ": " + y);
        }
        Console.WriteLine("Thread " + task + " DONE!");
        return output;
    }

    public static int CalcDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    private static Sensor ParseInputIntoSensorBeacon(string input)
    {
        var output = new Sensor();
        input = input.Substring(12);
        var pos = input.IndexOf(',');
        output.X = int.Parse(input.Substring(0, pos));
        input = input.Substring(pos + 4);
        pos = input.IndexOf(":");
        output.Y = int.Parse(input.Substring(0, pos));
        input = input.Substring(pos + 25);
        pos = input.IndexOf(',');
        output.BeaconX = int.Parse(input.Substring(0, pos));
        input = input.Substring(pos + 4);
        output.BeaconY = int.Parse(input);
        output.Distance = CalcDistance(output.X, output.Y, output.BeaconX, output.BeaconY);
        //output.Print();
        return output;
    }

    private static int MinX (Sensor sensor, int y = 2000000)
    {
        return Math.Abs(sensor.Y - y) - sensor.Distance + sensor.X;
        //-| sensor.y - y | +d + sensor.x
    }

    private static int MaxX(Sensor sensor, int y = 2000000)
    {
        return sensor.Distance + sensor.X - Math.Abs(sensor.Y - y);
        //-| sensor.y - y | +d + sensor.x
    }
}

class Sensor
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int BeaconX { get; set; }
    public int BeaconY { get; set; }
    public int Distance { get; set; }
    public int MinX { get; set; }
    public int MaxX { get; set; }


    public Sensor (int sensorX, int sensorY, int beaconX, int beaconY, int distance)
    {
        X = sensorX;
        Y = sensorY;
        BeaconX = beaconX;
        BeaconY = beaconY;
        Distance = distance;
    }

    public Sensor(Sensor sensor)
    {
        X = sensor.X;
        Y = sensor.Y;
        BeaconX = sensor.BeaconX;
        BeaconY = sensor.BeaconY;
        Distance = sensor.Distance;
    }

    public Sensor ()
    {

    }

    public void Print ()
    {
        Console.WriteLine("ID:" + Id + ", X:" + X + ", Y:" + Y + ", BeaconX:" + BeaconX + ", BeaconY:" + BeaconY + ", Dist:" + Distance);
    }
}