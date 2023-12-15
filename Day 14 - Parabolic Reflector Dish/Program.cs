using AdventofCode2023.Core;
using AdventOfCode2023.Day14;
using System.Diagnostics;

Console.WriteLine("Advent of Code Day 14 - Parabolic Reflector Dish");

//Part 1
var watch = Stopwatch.StartNew();
//var input = Utility.ReadProjectFile("input.txt");
var input = Utility.ReadProjectFile("sample.txt");
var result = 0;
var dish = Utility.GetInputArray<char>(input);

var reps = 27;

//Helper.PrintDish(dish);
Helper.TiltDishNorth(dish);
//Helper.PrintDish(dish);

var weight = Helper.CalculateWeight(dish);

result = weight;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}\n");

//Part 2
watch.Start();
result = 0;

dish = Utility.GetInputArray<char>(input);

watch.Start();
//Helper.PrintDish(dish);
int previous = 0;
var iterations = 0;
Dictionary<int, int> weightData = [];
List<int> weights = [];
for (int x = 0; x < reps; x++)
{
    iterations = x;
    Helper.TiltDishNorth(dish);
    //Console.WriteLine("Tilted North");
    //Console.WriteLine($"Rocks: {Helper.CountRocks(dish)}");
    //Helper.PrintDish(dish);
    //Console.WriteLine(); ;
    Helper.TiltDishWest(dish);
    //Console.WriteLine("Tilted West");
    //Console.WriteLine($"Rocks: {Helper.CountRocks(dish)}");
    //Helper.PrintDish(dish);
    //Console.WriteLine(); ;
    Helper.TiltDishSouth(dish);
    //Console.WriteLine("Tilted South");
    //Console.WriteLine($"Rocks: {Helper.CountRocks(dish)}");
    //Helper.PrintDish(dish);
    //Console.WriteLine(); ;
    Helper.TiltDishEast(dish);
    //Console.WriteLine("Tilted East");
    //Console.WriteLine($"Rocks: {Helper.CountRocks(dish)}");
    weight = Helper.CalculateWeight(dish);
    weights.Add(weight);
    if (weightData.ContainsKey(weight))
    {
        weightData[weight]++;
    }
    else
    {
        weightData.Add(weight, 1);
    }
    Console.WriteLine($"{weight}\t{x}");
}
//Helper.PrintDish(dish);
//Console.WriteLine($"Iterations:{iterations}");



foreach (var w in weightData.Keys)
{
    if (weightData[w] > 20)
    {
        Console.WriteLine($"Weight:{w} Times:{weightData[w]}");
    }
}

var list = Helper.CalculatePositionFromRepeats(weights, weightData, 15);

result = weight;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");
