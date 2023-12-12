using AdventofCode2023.Core;
using AdventOfCode2023.Day12;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 12 - Hot Springs");

//Part 1
var watch = Stopwatch.StartNew();
//var input = Utility.ReadProjectFile("input.txt");
var input = Utility.ReadProjectFile("sample.txt");
var result = 0;

List<SpringLine> springLines = Helper.GetSpringLines(input);
foreach (var line in springLines)
{
    Debug.WriteLine($"Broken Strings:{line.BrokenStrings}");
}

watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}\n");


//Part 2
watch.Start();
result = 0;



watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");
