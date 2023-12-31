﻿using AdventofCode2023.Core;
using AdventOfCode2023.Day13;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 13 - Point of Incidence");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample.txt");
//var input = Utility.ReadProjectFile("sample2.txt");
var result = 0;

List<List<string>> patterns = Helper.GetPatterns(input);

var runningTotal = 0;
foreach (var pattern in patterns)
{
    runningTotal += Helper.FindReflections(pattern);
}

result = runningTotal;

watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}\n");

//Part 2
watch.Start();
result = 0;

runningTotal = 0;
//var current = 0;
foreach (var pattern in patterns)
{
    //var score = Helper.FindSmudgedReflections(pattern);
    //Console.WriteLine($"Checking Pattern {current + 1}");
    
    //runningTotal += score;
    var patternCopy = pattern;
    runningTotal += Helper.FindSmudgedReflections(ref patternCopy);
    //runningTotal += Helper.FindReflections(patternCopy);
    //Console.WriteLine("Checked");
    //Console.WriteLine();
    //current++;
}

result = runningTotal;



watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

