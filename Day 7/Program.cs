using AdventofCode2023.Core;
using AdventOfCode2023.Day7;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 7 - ");

var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");

List<Hand> hands = Hand.GetAllHands(input);
hands.Sort();

int winTotal = 0;
for (int i = 0; i < hands.Count; i++)
{
    winTotal = winTotal + (hands[i].Bid * (i + 1));
}

var result = winTotal;
watch.Stop();
Console.WriteLine($"Ways to win product: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

//Part2
watch.Start();



result = 0;
watch.Stop();
Console.WriteLine($"Ways to win product: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

