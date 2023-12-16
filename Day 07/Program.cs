using AdventofCode2023.Core;
using AdventOfCode2023.Day7;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 7 - ");

var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
//var input = Utility.ReadProjectFile("sample.txt");


List<Hand> hands = Hand.GetAllHands(input);
hands.Sort();

int winTotal = 0;
for (int i = 0; i < hands.Count; i++)
{
    winTotal = winTotal + (hands[i].Bid * (i + 1));
}

var result = winTotal;
watch.Stop();
Console.WriteLine($"Win totals: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

//Part2
watch.Start();
List<WildHand> wildHands = WildHand.GetAllHands(input);
wildHands.Sort();

winTotal = 0;
for (int i = 0; i < wildHands.Count; i++)
{
    winTotal = winTotal + (wildHands[i].Bid * (i + 1));
}

result = winTotal;
watch.Stop();
Console.WriteLine($"Win totals: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

