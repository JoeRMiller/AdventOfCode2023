using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4___Scratchcards
{
    internal class Card
    {
        public List<int> Winners {  get; }
        public List<int> Numbers { get; }

        public int CardNumber { get; }

        private static char[] _separator = { ':', '|' };

        public static Card Parse(string line)
        {
            
            string[] strings1 = line.Split(_separator, StringSplitOptions.TrimEntries);
            string[] cardData = strings1[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] winnersData = strings1[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] numbersData = strings1[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            List<int> winners = new List<int>();
            foreach (string winner in winnersData)
            {
                winners.Add(int.Parse(winner));
            }

            List<int> numbers = new List<int>();
            foreach (string number in numbersData)
            {
                numbers.Add(int.Parse(number));
            }

            int cardNumber = int.Parse(cardData[1]);

            return new Card(cardNumber, winners, numbers);
        }

        public Card(int cardNumber, List<int> winners, List<int> numbers) 
        {
            this.CardNumber = cardNumber;
            this.Winners = winners;
            this.Numbers = numbers;
        }

        public int GetValue()
        {
            var value = 0;
            foreach (int number in this.Numbers)
            {
                if (this.Winners.Contains(number))
                {
                    if (value == 0)
                    {
                        value = 1;
                    }
                    else
                    {
                        value *= 2;
                    }
                }
            }
            return value;
        }
    }
}
