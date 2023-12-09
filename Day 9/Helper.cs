using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day9
{
    public class Helper
    {
        public static int ExtrapolateValue(List<int> readings) 
        {
            var filledTree = BuildTree(readings);
            for (int i = filledTree.Count - 2; i > 0; i--)
            {
                var adder = filledTree[i].Last();
                var extrapolated = filledTree[i - 1].Last() + adder;
                filledTree[i - 1].Add(extrapolated);
            }
            return filledTree[0].Last();
        }

        private static List<List<int>> BuildTree(List<int> readings)
        {
            List<List<int>> newTree = [];
            newTree.Add(readings);
            //List<int> diffs = [];

            bool done = false;
            var current = 0;

            while (!done)
            {
                var workingList = newTree[current];
                var nextLevel = GetDiffList(workingList);
                newTree.Add(nextLevel);

                var zeroCount = nextLevel.Where(x => x == 0).Count();
                if (nextLevel.Count == zeroCount)
                {
                    done = true;
                }
                current++;
            }

            return newTree;
        }

        private static List<int> GetDiffList(List<int> readings)
        {
            List<int> diffs = new List<int>();
            for (int i = 0; i < readings.Count - 1; i++)
            {
                var diff = readings[i + 1] - readings[i];
                diffs.Add(diff);
            }
            return diffs;
        }
    }
}
