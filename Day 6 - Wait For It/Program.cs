using AdventofCode2023.Core;
using System.Diagnostics;

Console.WriteLine("Day 6 - Wait For It");
var input = Utility.ReadProjectFile("input.txt");

//Part 1
var watch = Stopwatch.StartNew();



var result = 0;
watch.Stop();
Console.WriteLine($"Ways to win product: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");



