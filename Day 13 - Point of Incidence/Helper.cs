using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day13
{

    public static class Helper
    {
        public const char ASH = '.';
        public const char ROCK = '#';

        public static List<List<string>> GetPatterns(List<string> input)
        {
            List<List<string>> patterns = [];
            List<string> lines = [];
            foreach (string line in input)
            {
                if (line == string.Empty)
                {
                    //End of Pattern
                    patterns.Add(lines);
                    lines = [];
                }
                else
                {
                    lines.Add(line);
                }
            }
            patterns.Add(lines);
            return patterns;
        }

        public static int FindReflections(List<string> pattern)
        {
            var result = 0;
            int hScore  = FindHorizontalMirror(pattern);
            int vScore = FindVerticalMirror(pattern);
            return hScore + vScore;
        }

        private static int FindVerticalMirror(List<string> pattern)
        {
            var result = 0;
            var cols = pattern[0].Length;

            for (int col = 0; col < cols - 1; col++)
            {
                string first = new string(pattern.Select(p => p[col]).ToList().ToArray());
                string next = new string(pattern.Select(p => p[col + 1]).ToList().ToArray());

                if (first.Equals(next))
                {
                    var isMirror = WorkColumnMatchBack(col, pattern);
                    if (isMirror)
                    {
                        result = col + 1;
                        break;
                    }
                }
            }

            return result;
        }
        private static int FindHorizontalMirror(List<string> pattern)
        {
            var result = 0;
            var rows = pattern.Count();

            for (int row = 0; row < rows -1; row++)
            {
                string first = pattern[row];
                string second = pattern[row + 1];
                if (first.Equals(second))
                {
                    //Found a matching pair. Work back to the origin, or until a match fails
                    bool isMirror = WorkRowMatchBack(row, pattern);
                    if (isMirror)
                    {
                        result = 100 * (row + 1);
                        break;
                    }
                }
            }
            return result;
        }

        private static bool WorkColumnMatchBack(int column, List<string> pattern)
        {
            bool isMirror = true;
            bool done = false;
            int previous = column - 1;
            int next = column + 2;

            do
            {
                //If rowLocation is 2, that means matching pair is 2 and 3.
                if ((previous >= 0) && (next < pattern[0].Length))
                {
                    string line1 = new string(pattern.Select(p => p[previous]).ToList().ToArray());
                    string line2 = new string(pattern.Select(p => p[next]).ToList().ToArray());
                    previous--;
                    next++;
                    if (!line1.Equals(line2))
                    {
                        done = true;
                        isMirror = false;
                    }
                }
                else
                {
                    done = true;
                }
            }
            while (!done);

            return isMirror;
        }

        private static bool WorkRowMatchBack(int rowLocation, List<string>pattern)
        {
            bool isMirror = true;
            bool done = false;
            int previous = rowLocation - 1;
            int next = rowLocation + 2;
            do
            {
                //If rowLocation is 2, that means matching pair is 2 and 3.
                if ((previous >= 0) && (next < pattern.Count))
                {
                    var line1 = pattern[previous--];
                    var line2 = pattern[next++];
                    if (!line1.Equals(line2))
                    {
                        done = true;
                        isMirror = false;
                    }
                }
                else
                {
                    done = true;
                }
            }
            while (!done);
            return isMirror;
        }
    }
}
