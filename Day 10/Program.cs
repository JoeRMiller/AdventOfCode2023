using AdventofCode2023.Core;
using AdventOfCode2023.Day10;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

Console.WriteLine("Advent of Code Day 10 - Pipe Maze");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample2.txt");
//var input = Utility.ReadProjectFile("sample.txt");
//var input = Utility.ReadProjectFile("sample6.txt");


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
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}\n");

//====================================================================
//Part 2
watch.Start();
result = 0;

//Map of pipe chain locations
List<List<bool>> boolMap = [];
for (int i = 0; i < input.Count; i++)
{
    boolMap.Add(new List<bool>());
    for (int j = 0; j < input[i].Length; j++)
    {
        boolMap[i].Add(false);
    }
}


List<Marker> chain = [];
(line, chr) = Helper.FindStart(input);
direction = Helper.SetInitialDirection(line, chr, input);

m = new Marker();
m.line = line;
m.chr = chr;
m.c = input[line][chr];
m.direction = direction;
m.previous = Direction.None;
boolMap[m.line][m.chr] = true;

chain.Add(m);
next = Helper.GetNextMarker(m, input);
chain.Add(next);
boolMap[next.line][next.chr] = true;

while (next.c != 'S')
{
    next = Helper.GetNextMarker(next, input);
    boolMap[next.line][next.chr] = true;
    chain.Add(next);
}

var total = 0;
int rows = boolMap.Count;
int cols = boolMap[0].Count;

//PrintMap(boolMap);

for (int currentLine = 1; currentLine < rows - 1; currentLine++)
{
    //Console.WriteLine($"Testing Row {currentLine + 1}");
    for (int currentChr = 1; currentChr < cols - 1; currentChr++)
    {
        if (!boolMap[currentLine][currentChr])
        {
            //Check wall crossings from open point
            string inputLine = input[currentLine];
            List<bool> mapLine = boolMap[currentLine];

            //Count all crossed pipes from this point
            var crossed = Helper.CountCrossedWalls(currentLine, currentChr, mapLine, chain, inputLine);
            if (crossed %2 != 0)
            {
                total++;
            }
        }
    }
}

result = total;
watch.Stop();
Console.WriteLine($"Inner Tiles: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

