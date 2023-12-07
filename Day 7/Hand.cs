using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day7
{

 
    public class Hand
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
            Unknown = 7
        }
        public List<int> Cards { get; }
        public int Bid { get; }
        public string ShowHand { get; }

        public HandValue Value { get; }

        public Hand(string input)
        {
            this.Cards = [];
            string[] data = input.Split(' ');
            this.ShowHand = data[0];
            foreach (char c in data[0])
            {
                switch (c)
                {
                    case 'A':
                        this.Cards.Add(1);
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
                        this.Cards.Add(11);
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
 
            for (int i = 1; i <= 13; i++) 
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

            return value;
        }

        public static List<Hand> GetAllHands(List<string> input)
        {
            List<Hand> hands = [];
            foreach (var item in input)
            {
                hands.Add(new Hand(item));
            }
            return hands;
        }
    }
}
