using AdventofCode2023.Core;
using AdventOfCode2023.Day12;
using System.Diagnostics;
using System.Text;

Console.WriteLine("Advent of Code Day 12 - Hot Springs");

//Part 1
var watch = Stopwatch.StartNew();
//var input = Utility.ReadProjectFile("input.txt");
var input = Utility.ReadProjectFile("sample.txt");
var result = 0;

List<SpringLine> springLines = Helper.GetSpringLines(input);
StringBuilder sb = new StringBuilder();
foreach (var line in springLines)
{
    Tree<SpringLine> tree = Helper.BuildTree(line);
    TreeNode<SpringLine> node = tree.Root;
    Action<SpringLine> action = (SpringLine line) => Helper.PrintStringline(line);
    tree.PreOrderTraversal(tree.Root, nodeValue => sb.Append(Helper.PrintStringline(nodeValue)));
}
Console.WriteLine(sb.ToString());

watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}\n");


//Part 2
watch.Start();
result = 0;



watch.Stop();
Console.WriteLine($"Answer: {result}");
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}");
