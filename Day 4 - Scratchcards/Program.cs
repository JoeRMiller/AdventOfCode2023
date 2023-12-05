// See https://aka.ms/new-console-template for more information
using Day_4___Scratchcards;

Console.WriteLine("Advent of Code Day 4 - Scratchcards");

using var input = File.OpenText("..\\..\\..\\input.txt");

List<Card> cards = new List<Card>();
int totalValue = 0;
string line = "";
while ((line = input.ReadLine()) != null)
{
    Card card = Card.Parse(line); ;
    var value = card.GetValue();
    totalValue += value;
    cards.Add(card);
}


Console.WriteLine($"Total Value: {totalValue}");