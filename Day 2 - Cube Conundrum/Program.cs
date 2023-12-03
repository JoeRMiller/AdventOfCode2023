// See https://aka.ms/new-console-template for more information
using Day_2___Cube_Conundrum;

Console.WriteLine("Advent of Code Day 2 - Cube Conundrum");

var input = File.OpenText("..\\..\\..\\input.txt");

int total = 0;
string? line;
while ((line = input.ReadLine()) != null)
{
    Game game = new Game(line);
    if (game.IsValid())
    {
        total += game.ID;
    }
}
Console.WriteLine($"Sum of Game IDs: {total}");
