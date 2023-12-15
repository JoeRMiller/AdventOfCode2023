using AdventofCode2023.Core;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
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

        public static void TiltDishSouth(char[][] dish)
        {
            int length = dish.Length;
            int width = dish[0].Length;
            bool moved = false;
            do
            {
                moved = false;
                for (int row = length - 2; row >= 0; row--)
                {
                    for (int col = 0; col < width; col++)
                    {
                        if (dish[row][col] == 'O')
                        {
                            //Have a rock
                            //Check for open space above.
                            if (dish[row + 1][col] == '.')
                            {
                                //space to move here
                                dish[row + 1][col] = 'O';
                                dish[row][col] = '.';
                                moved = true;
                            }
                        }
                    }
                }
            }
            while (moved);
        }

        public static void TiltDishWest(char[][] dish)
        {
            int length = dish.Length;
            int width = dish[0].Length;
            bool moved = false;
            do
            {
                moved = false;
                for (int col = 1; col < width; col++)
                {
                    for (int row = 0; row < length; row++)
                    {
                        if (dish[row][col] == 'O')
                        {
                            //Have a rock
                            //Check for open space above.
                            if (dish[row][col - 1] == '.')
                            {
                                //space to move here
                                dish[row][col - 1] = 'O';
                                dish[row][col] = '.';
                                moved = true;
                            }
                        }
                    }
                }
            }
            while (moved);
        }

        public static void TiltDishEast(char[][] dish)
        {
            int length = dish.Length;
            int width = dish[0].Length;
            bool moved = false;
            do
            {
                moved = false;
                for (int col = width - 2; col >= 0; col--)
                {
                    for (int row = 0; row < length; row++)
                    {
                        if (dish[row][col] == 'O')
                        {
                            //Have a rock
                            //Check for open space above.
                            if (dish[row][col + 1] == '.')
                            {
                                //space to move here
                                dish[row][col + 1] = 'O';
                                dish[row][col] = '.';
                                moved = true;
                            }
                        }
                    }
                }
            }
            while (moved);
        }

        public static void TiltDishNorth2(char[][] dish)
        {
            int length = dish.Length;
            int width = dish[0].Length;

            for (int col = 0; col < width; col++)
            {
                //Get the entire column here

                //var column = dish.Select(d => d[col]).ToArray();
                var column = new char[length];
                for (int i = 0; i < length; i++)
                {
                    column[i] = dish[i][col];
                }

                //SortColumn(column);
                SortColumn2(column);

                for (int i = 0; i < column.Length; i++)
                {
                    dish[i][col] = column[i];
                }
                
            }   
        }

        public static void SortColumn2(char[] column)
        {
            //New approach, find runs between #'s, count the O's and just put that many at the front

            //var stopIndexes = column.Select((value, index) => new { value, index })
            //.Where(x => x.value == '#')
            //.Select(x => x.index)
            //.ToList();

            List<int> stopIndexes = [];
            for (int i = 0; i < column.Length; i++)
            {
                if (column[i] == '#')
                {
                    stopIndexes.Add(i);
                }
            }
            
            stopIndexes.Add(column.Length);

            int start = 0;
            foreach (var index in stopIndexes)
            {
                //Get rock count in run
                int rocks = 0;
                for (int i = start; i < index; i++)
                {
                    if (column[i] == 'O')
                    {
                        rocks++;
                    }
                }

                //rocks = column.Skip(start)
                //                  .Take(index - 1)
                //                  .Count(x => x == 'O');
                
                //Put the rocks at the beginning
                for (int i = 0; i < rocks ; i++)
                {
                    column[start + i] = 'O';
                }
                //Fill in .
                for (int i = start + rocks; i < index; i++)
                {
                    column[i] = '.';
                }
                start = index + 1;
                
            }
        }

        public static void SortColumn(char[] columnm)
        {
            for (int i = 1; i < columnm.Length; i++)
            {
                //move rock as far up as possible
                
                bool found = false;
                if (columnm[i] == 'O')
                {
                    var previous = i - 1;
                    do
                    {
                        if (columnm[previous] != '.')
                        {
                            //found stop
                            found = true;
                            break;
                        }
                        previous--;
                        if (previous == -1)
                        {
                            found = true;
                        }
                        

                    }
                    while ((!found) && (previous >= 0));

                    if ((found) && ((previous + 1) != i))
                    {
                        //Move Rock
                        columnm[previous + 1] = 'O';
                        columnm[i] = '.';
                    }
                }
            }
        }

        public static int CalculateWeight(char[][] dish)
        {
            int weight = 0;
            int rows = dish.Length;
            
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

        public static int CountRocks(char[][] dish)
        {
            int count = 0;
            foreach (var row in dish)
            {
                count += row.Count(r => r == 'O');
            }

            return count;
        }

        public static List<int> CalculatePositionFromRepeats(List<int> weightSequence, Dictionary<int, int> weights, int iterations)
        {
            List<int> sequence = [];

            int weightCountSum = weights.Values.Sum();
            int weightsCount = weights.Keys.Count();
            double meanWeightCount = weightCountSum / weightsCount;
            double stdev = Math.Sqrt(weights.Values.Sum(x => Math.Pow(x - meanWeightCount, 2)) / weights.Values.Count());
            int lowWeight = Convert.ToInt32(meanWeightCount - stdev);


            var repeatMembers = weights.Where(x => x.Value > lowWeight).Select(y => y.Key).ToList();
//            var repeatMembers = weights.Where(x => x.Value > ).Select(y => y.Key).ToList();

            //Brute force the damn thing
            //Find last instance of weight not in sequence member list
            int last = 0;
            for (int i = 0; i < repeatMembers.Count; i++)
            {
                if (!repeatMembers.Contains(weightSequence[i]))
                {
                    last = i;
                    continue;
                }
            }
            //int searchStartPosition = last + 1;
            Console.WriteLine($"Last non member weight is {weightSequence[last]} at {last + 1}");

            //start checking lists of length member count and up
            //build 
            //start at first member position
            //check all sample lists
            //increase sample list size, and check again




            //For each first member position on
            //build sample list
            //check all ranges
            //if repeat found,
            //exit loop, noting sample list and match position
            //else
            //increase sample length
            //repeat

            //build sample list
            //check all ranges
            //if match found
            //exit loop, noting sample list and match position
            //else
            //move the start forward 1 spot

            //increase sample length
            //repeat
            

            var searchList = weightSequence.SubList(last + 1); 

           //This will increase with each full search iteration
            int windowLength = repeatMembers.Count;
            int lastRangeSearchPosition = searchList.Count - repeatMembers.Count;
            bool foundMatch = false;
            int matchPosition = 0;
            List<int> sample = [];

            //For each first member position on
            for (int x = 0; x < lastRangeSearchPosition; x++)
            {
                //build sample list
                sample = [];
                for (int j = x; j < x + windowLength; j++)
                {
                    sample.Add(searchList[j]);
                }


                //Here we have the trimmed weight list.
                var rangeStart = 0 /*searchStartPosition*/;


                for (int y = rangeStart; y <= lastRangeSearchPosition - x/* - rangeStart - windowLength*/; y++)
                {
                    int endPosition = lastRangeSearchPosition - y - windowLength;
                    for (int i = rangeStart + windowLength + y; i <= endPosition; i = i + windowLength)
                    {
                        //Starting at last member found position, build sequences starting with member list size.
                        var testSequence = searchList.SubList(i, i + windowLength - 1);
                        if (testSequence.SequenceEqual(sample))
                        //if (weightSequence.Where((item, index) => index <= weightSequence.Count - sample.Count)
                        //                      .Any(index => weightSequence.Skip(index).Take(sample.Count).SequenceEqual(sample)))
                        {
                            Console.WriteLine("Found Match");
                            foundMatch = true;
                            matchPosition = y /*+ searchStartPosition*/;
                            //break;
                        }
                        else if (foundMatch)
                        {
                            //Here we have found an initial match, but subsequent matches failed
                            Console.WriteLine("False Match");
                            foundMatch = false;
                        }
                    }
                    if (foundMatch)
                    {
                        break;
                    }
                }                
                if (foundMatch)
                {
                    break;
                }
                windowLength++;
            }
            Console.WriteLine($"Sequence starts at {matchPosition}");
            foreach (var s in sample)
            {
                Console.Write($"{s} ");
            }
            Console.WriteLine();

            int result = (iterations - matchPosition - 1) % sample.Count;
            Console.WriteLine(result);



            //Start at the weight value after the last, and start looking for repeats with growing list lengths. Start with the length of the members list
            

            


            return sequence;
        }
    }
}
