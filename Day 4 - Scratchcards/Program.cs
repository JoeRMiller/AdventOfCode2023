// See https://aka.ms/new-console-template for more information
using Day_4___Scratchcards;

Console.WriteLine("Advent of Code Day 4 - Scratchcards");

using var input = File.OpenText("..\\..\\..\\input.txt");
//using var input = File.OpenText("..\\..\\..\\testdata.txt");

List<Card> deck = new List<Card>();


int CARD = 0;
int ITERATION = 0;

int totalValue = 0;
string line = "";
while ((line = input.ReadLine()) != null)
{
    Card card = Card.Parse(line); ;
    var value = card.GetValue();
    totalValue += value;
    deck.Add(card);
}

List<Card> allCards = ProcessCardListLevel(deck);

//Console.WriteLine($"Total Value: {totalValue}");
Console.WriteLine($"Total Cards: {allCards.Count + deck.Count}");

List<Card> ProcessCardListLevel(List<Card> previousCards)
{
    List<Card> addedCards = new List<Card>();

    foreach (Card card in previousCards)
    {
        int wins = card.Wins;

        if (wins > 0)
        {
            int nextCard = card.CardNumber;
            for (int i = nextCard; i < nextCard + wins; i++)
            {
                Card c = deck[i];
                addedCards.Add( c);
            }
        }
    }

    List<Card> temp = new List<Card>();
    if (addedCards.Count > 0)
    {
        temp = ProcessCardListLevel(addedCards);
    }

    //Here the addedCards list has all the winners from the current cardStack;
    if (temp.Count > 0)
    {
        foreach (Card card in temp)
        {
            addedCards.Add(card);
        }
    }

    return addedCards;
}
