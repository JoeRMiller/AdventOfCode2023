using AdventofCode2023.Core;


using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

Console.WriteLine("Advent of Code Day 7 - ");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample.txt");

List<char> directions = input[0].ToList();
Dictionary<string, Tuple<string, string>> map = [];

Regex r = new Regex(@"(\w+)\s=\s\((\w+)\,\s(\w+)");
for (int i = 2; i < input.Count; i++)
{
    Match m = r.Match(input[i]);
    
    Tuple<string, string> mapping = Tuple.Create(m.Groups[2].Value, m.Groups[3].Value);
    map.Add(m.Groups[1].Value, mapping);
}

bool finished = false;
int steps = 0;

////Search through diretions list repeatedly until destination is found
//Tuple<string, string> current = map["AAA"];
//while (!finished)
//{
//    //Search through each step in directions list
//    for (int i = 0; i < directions.Count; i++)
//    {
//        var direction = directions[i];
//        var location = "";
//        if (direction == 'L')
//        {
//            location = current.Item1;
//        }
//        else
//        {
//            location = current.Item2;
//        }
//        steps++;
//        if (location == "ZZZ")
//        {
//            finished = true;
//            break;
//        }

//        current = map[location];
//    }
//}



//var result = steps;
//watch.Stop();
//Console.WriteLine($"Answer: {result}");
//Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

//Part 2
watch.Start();

Dictionary<string, Tuple<string, string>> currents = (Dictionary<string, Tuple<string, string>>)map.Where(m => m.Key[2] == 'A').ToDictionary();
steps = 0;
finished = false;
int times = 0;

List<long> mapSteps = [];
foreach (var current in currents.Keys)
{
    string start = current;
    var round = CalculateSteps(start);
    Console.WriteLine($"Starting Location {current} took {round} steps");
    mapSteps.Add(round);
}

var wtf = test(mapSteps.ToArray());
Console.WriteLine(wtf);

return;
while (!finished)
{
    //Search through each step in directions list
    for (int i = 0; i < directions.Count; i++)
    {
        var direction = directions[i];
        List<string> locations = [];
        if (direction == 'L')
        {
            locations = currents.Values.Select(c => c.Item1).ToList();
        }
        else
        {
            locations = currents.Values.Select(c => c.Item2).ToList();
        }

        //Locations now has a list of all locations pointed to by previous mapping.
        steps++;

        bool missed = false;
        //Console.Write($"Step:{steps}:");
        var hits = locations.Where(c => c[2] == 'Z');
        if (hits.Count() > 0)
        {
            foreach (var hit in hits)
            {
                Console.Write($"{hit} ");
            }
            Console.WriteLine();
        }
        
        Dictionary<string, Tuple<string, string>> newLocations = [];
        currents = [];
        foreach (var location in locations)
        {
            currents.Add(location, map[location]);
        }

        hits = locations.Where(l => l[2] == 'Z');
        if (hits.Count() == locations.Count())
        {
            finished = true;
            break;
        }
        
        //foreach (var location in locations)
        //{
        //    if (location[2] != 'Z')
        //    {
        //        //Havent reached the end of this parallel trip, break out and continue
        //        missed = true;
        //        //Console.WriteLine();
        //        break;
        //    }
        //    else
        //    {
        //        //Console.WriteLine($"{location}");
        //    }
        //    //If we got here, all locations end with Z
        //}

        //if (missed == false)
        //{
        //    finished = true;
        //    break;
        //}

        //Dictionary<string, Tuple<string, string>> newLocations = [];
        //currents = [];
        //foreach (var location in locations)
        //{
        //    currents.Add(location, map[location]);
        //}
        
    }
    
    //Console.WriteLine($"Times Through: {++times}");
}


var result = steps;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

int CalculateSteps(string start)
{
    //Search through diretions list repeatedly until destination is found
    Tuple<string, string> current = map[start];
    int steps = 0;
    finished = false;
    while (!finished)
    {
        //Search through each step in directions list
        for (int i = 0; i < directions.Count; i++)
        {
            var direction = directions[i];
            var location = "";
            if (direction == 'L')
            {
                location = current.Item1;
            }
            else
            {
                location = current.Item2;
            }
            steps++;
            if (location[2] == 'Z')
            {
                finished = true;
                break;
            }

            current = map[location];
            
        }
    }
    return steps;
}

static long gcd(long n1, long n2)
{
    if (n2 == 0)
    {
        return n1;
    }
    else
    {
        return gcd(n2, n1 % n2);
    }
}

static long test(long[] numbers)
{
    return numbers.Aggregate((S, val) => S * val / gcd(S, val));
}