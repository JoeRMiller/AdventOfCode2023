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

int total = hashList.Sum(hash => hash.WordHash);


result = total;
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}\n");


//Part 2
watch.Start();
result = 0;

var boxes = Helper.GetBoxes();

hashList = Helper.GetHashList(input[0]);
foreach (var hash in hashList)
{
    //apply command to box
    int box = hash.BoxHash;
    var list = boxes[box];

    if (hash.Operation == OperationEnum.Add)
    {
        if (list.Any(h => h.LensName == hash.LensName))
        {
            //Lens already exists, replace it
            var index = list.FindIndex(h => h.LensName == hash.LensName);
            list[index] = hash;
        }
        else
        {
            //Lens doesnt exist, add it
            boxes[box].Add(hash);
        }
    }
    else if (hash.Operation == OperationEnum.Remove)
    {
        if (list.Any(h => h.LensName == hash.LensName))
        {
            //Lens exists, remove it
            var index = list.FindIndex(h => h.LensName == hash.LensName);
            list.RemoveAt(index);
        }
    }
    else
    {
        throw new InvalidDataException();
    }
}

result = Helper.CalculateTotalLensPower(boxes);
watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");
