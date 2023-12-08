using AdventofCode2023.Core;


using System.Diagnostics;
using System.Runtime.ExceptionServices;
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

//Search through diretions list repeatedly until destination is found
Tuple<string, string> current = map["AAA"];
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
        if (location == "ZZZ")
        {
            finished = true;
            break;
        }

        current = map[location];
    }
}



var result = steps;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

//Part 2
watch.Start();


watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");