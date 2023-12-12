using AdventofCode2023.Core;
using AdventOfCode2023.Day11;
using System.Diagnostics;
using System.Linq;
using System.Text;

Console.WriteLine("Advent of Code Day 11 - Cosmic Expansion");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample.txt");
var darkEnergy = 1;
long result = 0;

Spacetime space = Helper.GetDistanceMap(input, darkEnergy);
List<Coordinate> galaxies = Helper.FindGalaxies(input);
var pathTotal = Helper.MapPaths(galaxies, input, space);

result = pathTotal;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

//Part 2
watch.Start();
result = 0;
darkEnergy = 1000000;

space = Helper.GetDistanceMap(input, darkEnergy);
galaxies = Helper.FindGalaxies(input);
pathTotal = Helper.MapPaths(galaxies, input, space);

result = pathTotal;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");