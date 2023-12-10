using AdventofCode2023.Core;
using AdventOfCode2023.Day10;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 10 - Pipe Maze");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample2.txt");
//var input = Utility.ReadProjectFile("sample.txt");

var result = 0;

(int line, int chr) = Helper.FindStart(input);
var direction = Helper.SetInitialDirection(line, chr, input);
Marker m = new Marker();
m.line = line;
m.chr = chr;
m.c = input[line][chr];
m.direction = direction;

var next = Helper.GetNextMarker(m, input);
int steps = 1;
while (next.c != 'S')
{
    next = Helper.GetNextMarker(next, input);
    steps++;
}

result = steps / 2;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");


//Part 2
watch.Start();
result = 0;



watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");