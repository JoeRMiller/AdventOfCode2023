// See https://aka.ms/new-console-template for more information
using Day_2___Cube_Conundrum;

Console.WriteLine("Advent of Code Day 2 - Cube Conundrum");

var input = File.OpenText("..\\..\\..\\input.txt");

int totalPower = 0;
string? line;
while ((line = input.ReadLine()) != null)
{
    Game game = new Game(line);
    var power = game.GetGamePower();
    Console.WriteLine($"Power: {power}");
    totalPower += power;
}
Console.WriteLine($"Total Game Power: {totalPower}");
