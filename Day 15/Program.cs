using AdventofCode2023.Core;
using AdventOfCode2023.Day15;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 15 - Lens Library");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample.txt");
var result = 0;

var hashList = Helper.GetHashList(input[0]);

int total = hashList.Sum(hash => hash.Value);


result = total;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}\n");


//Part 2
watch.Start();
result = 0;



watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");
