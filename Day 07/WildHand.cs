using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day7
{
    public class WildHand : IComparable<WildHand>
    {
        public enum HandValue
        {
            HighCard = 0,
            Pair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            FullHouse = 4,
            FourOfAKind = 5,
            FiveOfAKind = 6,
        }
        public List<int> Cards { get; }
        public int Bid { get; }
        public string ShowHand { get; }

        public HandValue Value { get; }

        public WildHand(string input)
        {
            this.Cards = [];
            string[] data = input.Split(' ');
            this.ShowHand = data[0];
            foreach (char c in data[0])
            {
                switch (c)
                {
                    case 'A':
                        this.Cards.Add(14);
                        break;
                    case '2':
                        this.Cards.Add(2);
                        break;
                    case '3':
                        this.Cards.Add(3);
                        break;
                    case '4':
                        this.Cards.Add(4);
                        break;
                    case '5':
                        this.Cards.Add(5);
                        break;
                    case '6':
                        this.Cards.Add(6);
                        break;
                    case '7':
                        this.Cards.Add(7);
                        break;
                    case '8':
                        this.Cards.Add(8);
                        break;
                    case '9':
                        this.Cards.Add(9);
                        break;
                    case 'T':
                        this.Cards.Add(10);
                        break;
                    case 'J':
                        this.Cards.Add(1);
                        break;
                    case 'Q':
                        this.Cards.Add(12);
                        break;
                    case 'K':
                        this.Cards.Add(13);
                        break;
                }
            }
            this.Bid = int.Parse(data[1]);
            this.Value = this.GetHandValue();
        }

        private HandValue GetHandValue()
        {
            HandValue value;
            List<HandValue> values = [];
            int jokers = this.Cards.Where(c => c == 1).Count();

            for (int i = 1; i <= 14; i++) 
            {
                var count = this.Cards.Where(c => c == i).Count();

                switch (count)
                {
                    case 5:
                        values.Add(HandValue.FiveOfAKind);
                        break;
                    case 4:
                        values.Add(HandValue.FourOfAKind);
                        break;
                    case 3:
                        values.Add(HandValue.ThreeOfAKind);
                        break;
                    case 2:
                        values.Add(HandValue.Pair);
                        break;
                }
            }

            if (values.Count == 0)
            {
                value = HandValue.HighCard;
            }
            else if (values.Count == 1)
            {
                value = values[0];
            }
            else
            {
                if (values[0] == values[1])
                {
                    value = HandValue.TwoPair;
                }
                else
                {
                    value = HandValue.FullHouse;
                }
            }

            if (jokers > 0)
            {
                value = AdjustHandValueForJokers(this.Cards, value, jokers);
            }
           
            return value;
        }

        private HandValue AdjustHandValueForJokers(List<int> cards, HandValue value, int jokers)
        {
            if (jokers == 4) 
            {
                value = HandValue.FiveOfAKind;
            }
            else if (jokers == 3) 
            {
                switch (value)
                {
                    case HandValue.ThreeOfAKind:
                        value = HandValue.FourOfAKind;
                        break;
                    case HandValue.FullHouse:
                        value = HandValue.FiveOfAKind;
                        break;
                }
            }
            else if (jokers == 2) 
            {
                switch (value)
                {
                    case HandValue.Pair:
                        value = HandValue.ThreeOfAKind;
                        break;
                    case HandValue.ThreeOfAKind:
                        value = HandValue.FiveOfAKind;
                        break;
                    case HandValue.TwoPair:
                        value = HandValue.FourOfAKind;
                        break;
                    case HandValue.FullHouse:
                        value = HandValue.FiveOfAKind;
                        break;
                }
            }
            else
            {
                switch (value)
                {
                    case HandValue.HighCard:
                        value = HandValue.Pair;
                        break;
                    case HandValue.Pair:
                        value = HandValue.ThreeOfAKind;
                        break;
                    case HandValue.ThreeOfAKind:
                        value = HandValue.FourOfAKind;
                        break;
                    case HandValue.FourOfAKind:
                        value = HandValue.FiveOfAKind;
                        break;
                    case HandValue.TwoPair:
                        value = HandValue.FullHouse;
                        break;
                    case HandValue.FullHouse:
                        value = HandValue.FiveOfAKind;
                        break;
                }
            }
            
            return value;
        }

        public static List<WildHand> GetAllHands(List<string> input)
        {
            List<WildHand> hands = [];
            foreach (var item in input)
            {
                hands.Add(new WildHand(item));
            }
            return hands;
        }

        public int CompareTo(WildHand other)
        {
            if (other.Value > this.Value) { return -1; }
            if (other.Value < this.Value) { return 1; }

            for (int i = 0; i < 5; i++)
            {
                if (other.Cards[i] > this.Cards[i]) { return -1; }
                if (other.Cards[i] < this.Cards[i]) { return 1; }
            }
            return 0;
        }
    }
}
