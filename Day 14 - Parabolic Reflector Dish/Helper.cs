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

        public static int CalculatePositionFromRepeats(List<int> weightSequence, Dictionary<int, int> weights, int iterations)
        {
            List<int> sequence = [];

            //Figure out which weights are part of the repeated sequence, and which are not.
            //Idea is that given enough iterations, the count of repeated weights will be much larger than the non repeated sequence weights.
            //To divide the groups, we take the mean of the times each weight appears in the list.
            //  Then calculate the standard deviation, and discard the weights that show up less than that.
            //This alomst certainly isnt the right algorith for this, but I don't know how else to separate the groups.
            int weightCountSum = weights.Values.Sum();
            int weightsCount = weights.Keys.Count();
            double meanWeightCount = weightCountSum / weightsCount;
            double stdev = Math.Sqrt(weights.Values.Sum(x => Math.Pow(x - meanWeightCount, 2)) / weights.Values.Count());
            int lowWeight = Convert.ToInt32(meanWeightCount - stdev);

            //Select only the highly repeated weights from the full list of weights.
            //This size of this list will be the initial window size for searching the list space for repeated sequences.
            var repeatMembers = weights.Where(x => x.Value > lowWeight).Select(y => y.Key).ToList();
            var repeatMembersCount = repeatMembers.Count;

            //Find last instance of a weight not in sequence member list
            //All weights prior to this one will be trimmed from the full list.
            //The rest of the weight sequence will be the search space
            int last = 0;
            for (int i = 0; i < repeatMembersCount; i++)
            {
                if (!repeatMembers.Contains(weightSequence[i]))
                {
                    last = i;
                    continue;
                }
            }
            var fullSearchSequence = weightSequence.SubList(last + 1);
            //Console.WriteLine($"Last non member weight is {weightSequence[last]} at {last + 1}");

            //The maximum size of a repeated sequence is half of the search space.
            //Start building search blocks using the size of the repeated members list.
            List<int> repeatingSequence = [];
            bool isMatch = false;
            int matchIndex = -1;
            var maxSearchSequenceLength = fullSearchSequence.Count / 2;
            
            //For smallest window size to max window size
            for (int windowSize = repeatMembersCount; windowSize <= maxSearchSequenceLength; windowSize++)
            {
                //Console.WriteLine($"Window size:{windowSize}");
                //for 0 to sequence end - window size
                for (int searchWindowIndex = 0; searchWindowIndex < fullSearchSequence.Count - windowSize; searchWindowIndex++)
                {
                    //Build the search window of length searchSequenceLength
                    List<int> searchWindow = [];
                    //searchWindow = fullSearchSequence.SubList<int>(searchWindowIndex, searchWindowIndex + searchSequenceIndex - 1);
                    searchWindow = fullSearchSequence.SubList<int>(searchWindowIndex, searchWindowIndex + windowSize - 1);

                    //Now we have a search window, which we will slide along until we would repeat the same window in the full sequence
                    int windowSlides = searchWindow.Count - 1;
                    for (int slideIteration = 0; slideIteration < windowSlides; slideIteration++)
                    {
                        int matchCount = 0;
                        int timesToSearch = (fullSearchSequence.Count - searchWindow.Count - slideIteration) / searchWindow.Count;
                        isMatch = false;
                        matchIndex = -1;
                        for (int i = 0; i < timesToSearch; i++)
                        {
                            var startIndex = (i + 1) * searchWindow.Count + slideIteration;
                            var endIndex = startIndex + searchWindow.Count;
                            var testSequence = fullSearchSequence.SubList<int>(startIndex, endIndex - 1);
                            //if (testSequence.Equals(searchWindow))
                            if (IsEqual(testSequence, searchWindow))
                            {
                                //Found a match. Every subsequent comparison must also match for this to be a repeating sequence window
                                isMatch = true;
                                //Set the index for where the repeating section started
                                if (matchCount == 0)
                                {
                                    matchIndex = (slideIteration + ((i + 1) * searchWindow.Count)) - searchWindow.Count;
                                }
                                
                                matchCount++;
                                if (slideIteration == windowSlides - 1)
                                {
                                    isMatch = false;
                                }
                            }
                            else if (isMatch)
                            {
                                //Here we had found a matching sequence window, but a subsequent window failed. This isn't a repeating pattern
                                //Quit searching from this sequence start location
                                isMatch = false;
                                matchCount = 0;
                                break;
                            }
                        }
                        //Here we have completed a slide iteration. If isMatch is true, we found our repeating sequence
                        //The start of the repeating sequence is held in matchIndex.
                        //This is relative to the trimmed full search sequence, not the complete weights sequence
                        if (isMatch && matchCount > 1)
                        {
                            //Should be all done
                            break;
                        }
                    }
                    //If we have a mnatch here, quit the loop
                    if (isMatch)
                    {
                        //Should be all done
                        repeatingSequence = searchWindow;
                        break;
                    }
                }
                if (isMatch)
                {
                    //Should be all done
                    
                    break;
                }

            }
            int leading = last + 1;
            
            int rem = iterations - (matchIndex + leading);
            int weight = rem % repeatingSequence.Count;
           
            return repeatingSequence[weight - 1];
        }

        private static bool IsEqual(List<int> a, List<int> b)
        {
            for (int i = 0; i<a.Count; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
