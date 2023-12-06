using AdventofCode2023.Core;
using AdventofCode2023;
using System.Diagnostics;

Console.WriteLine("Day 6 - Wait For It");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
List<Race> races = RaceFactory.CreateRaces(input);




var result = 0;
watch.Stop();
Console.WriteLine($"Ways to win product: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");



