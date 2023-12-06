using AdventofCode2023.Core;
using AdventofCode2023;
using System.Diagnostics;

Console.WriteLine("Day 6 - Wait For It");

//Part 1
var watch = Stopwatch.StartNew();
var input = Utility.ReadProjectFile("input.txt");
List<Race> races = RaceFactory.CreateRaces(input);
var productTotal = 1;

foreach (Race race in races)
{
    var time = race.Time;
    var distance = race.Distance;
    var wins = 0;
    for (long i = 1; i < time; i++)
    {
        var timeLeft = time - i;
        var travel = timeLeft * i;
        if (travel > distance)
        {
            //This wait time beat the current best. 
            wins++;
        }
        if ((wins > 0) && (travel < distance))
        {
            //Losing distance, quit checking
            break;
        }
    }
    productTotal *= wins;
}



var result = productTotal;
watch.Stop();
Console.WriteLine($"Ways to win product: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");

//Part2
watch.Start();
Race combo = RaceFactory.CreateRace(input);

var t = combo.Time;
var d = combo.Distance;
var w = 0;
for (long i = 1; i < t; i++)
{
    var timeLeft = t - i;
    var travel = timeLeft * i;
    if (travel > d)
    {
        //This wait time beat the current best. 
        w++;
    }
    if ((w > 0) && (travel < d))
    {
        //Losing distance, quit checking
        break;
    }
}

Console.WriteLine($"Ways to win: {w}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");




