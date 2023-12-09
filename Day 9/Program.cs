using AdventofCode2023.Core;
using AdventOfCode2023.Day9;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 9 - Mirage Maintenance");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample.txt");
var result = 0;

List<List<int>> tree = [];
foreach (var line in input)
{
    string[] readings = line.Split(' ');
    List <int> r = readings.Select(s => int.Parse(s)).ToList();
    tree.Add(r);
}

foreach (var r in tree)
{
    result = result + Helper.ExtrapolateValue(r);
}


watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");



//Part 2
watch.Start();
result = 0;
List<List<int>> back = [];
foreach (var line in input)
{
    string[] readings = line.Split(' ');
    List<int> r = readings.Select(s => int.Parse(s)).ToList();
    back.Add(r);
}

foreach (var r in back)
{
    result = result + Helper.ExtrapolateReverse(r);
}

watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");
