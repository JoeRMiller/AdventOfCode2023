// See https://aka.ms/new-console-template for more information
using Day_4___Scratchcards;
using System.Runtime.ExceptionServices;
using System.Security;

Console.WriteLine("Advent of Code Day 4 - Scratchcards");

//using var input = File.OpenText("..\\..\\..\\input.txt");
using var input = File.OpenText("..\\..\\..\\testdata.txt");

//List<Card> deck = new List<Card>();
SortedList<int, Card> deck = new SortedList<int, Card>(new DuplicateKeyComparer<int>());

int CARD = 0;

int totalValue = 0;
string line = "";
while ((line = input.ReadLine()) != null)
{
    Card card = Card.Parse(line); ;
    var value = card.GetValue();
    totalValue += value;
    deck.Add(card.CardNumber, card);
}

foreach (Card card in deck.Values)
{
    //Console.WriteLine($"Card Number:{card.CardNumber}");
}
//List<Card> allCards = ProcessCardList(deck);
SortedList<int, Card> allCards = ProcessCardList(deck, 0);

//Console.WriteLine($"Total Value: {totalValue}");
Console.WriteLine($"Total Cards: {allCards.Count}");
foreach (Card card in allCards.Values)
{
    //Console.WriteLine($"Card Number:{card.CardNumber}");
}

//List<Card> ProcessCardList(List<Card> cards)


SortedList<int, Card> ProcessCardList(SortedList<int, Card> cards, int depth)
{
    depth++;
   
    SortedList<int, Card> addedCards = new SortedList<int, Card>(new DuplicateKeyComparer<int>());
    foreach (Card card in cards.Values)
    {
        int wins = card.Wins;
        if (depth == 1)
        {
            CARD = card.CardNumber;
        }
    
        Console.Write($" {CARD:D3} - {card.CardNumber:D3} - {wins:D2} - ");
        for (int x = 1; x <= depth; x++)
        {
            Console.Write("#");
        }
        Console.Write('\n');
        if (depth == 1) Console.WriteLine($"Checking Card:{card.CardNumber}");
        
        //Console.WriteLine($"Card {card.CardNumber} has {wins} wins.");
        addedCards.Add(card.CardNumber, card);
        if (wins > 0)
        {
            
            SortedList<int, Card> cardsWon = new SortedList<int, Card>(new DuplicateKeyComparer<int>());
            int nextCard = card.CardNumber;
            for (int i = nextCard; i < nextCard + wins; i++)
            {
                Card c = deck.Values[i];
                cardsWon.Add(c.CardNumber, c);
                //Console.WriteLine($"Adding Card:{c.CardNumber}");
            }
            
            SortedList<int, Card> temp = ProcessCardList(cardsWon, depth);

            foreach (Card added in temp.Values)
            {
                addedCards.Add(added.CardNumber, added);
            }
        }
    }
    Console.WriteLine($"Total Cards:{cards.Count:D5}=========================================================================================");   
    return addedCards;
}
