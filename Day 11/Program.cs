using AdventofCode2023.Core;
using AdventOfCode2023.Day11;
using System.Diagnostics;
using System.Linq;
using System.Text;

Console.WriteLine("Advent of Code Day 10 - CHALLENGE NAME");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample.txt");
var result = 0;

List<string> universe = Helper.GetUniverse(input);
List<Coordinate> galaxies = Helper.FindGalaxies(universe);
Helper.PrintUniverse(universe);
var pathTotal = Helper.MapPaths(galaxies, universe);

result = pathTotal;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");



//Part 2
watch.Start();
result = 0;



watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");