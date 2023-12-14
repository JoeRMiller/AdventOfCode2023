using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day14
{
    public static class Helper
    {
        public static void TiltDishNorth(char[][] dish)
        {
            int length = dish.Length;
            int width = dish[0].Length;
            bool moved = false;

            do
            {
                moved = false;
                for (int row = 1; row < length; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        if (dish[row][col] == 'O')
                        {
                            //Have a rock
                            //Check for open space above.
                            if (dish[row - 1][col] == '.')
                            {
                                //space to move here
                                dish[row - 1][col] = 'O';
                                dish[row][col] = '.';
                                moved = true;
                            }
                        }
                    }
                }
            }
            while (moved);
        }

        public static int CalculateWeight(char[][] dish)
        {
            int weight = 0;
            int rows = dish.Length;
            int total = 0;
            for (int row = 0; row < rows; row++)
            {
                int rocks = dish[row].Where(x => x == 'O').Count();
                weight += (rocks * (rows - row));
            }

            return weight;
        }

        public static void PrintDish(char[][] dish)
        {
            foreach (var row in dish)
            {
                foreach (var point in row)
                {

                    Console.Write(point);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
