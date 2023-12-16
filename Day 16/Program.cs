using AdventofCode2023.Core;
using AdventOfCode2023.Day16;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 16 - The Floor Will Be Lava");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample.txt");
var charMap = Utility.GetInputArray<char>(input);
var result = 0;

var tileMap = Helper.BuildMap(charMap);
Helper.TraverseMap(tileMap, 0, 0, Direction.Right);

result = Helper.CountEnergized(tileMap);
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}\n");


//Part 2
watch.Start();
result = 0;



watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");
