using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day12
{
    public class SpringLine
    {
        public string StatusLine { get; private set; } = string.Empty;
        public List<int> GroupSize { get; } = new List<int>();
        public int BrokenStrings { get; private set; }
        public int Arrangements { get; private set; }

        

        public SpringLine(string input) 
        {
            this.GroupSize = [];
            this.ParseInput(input);
            this.CalculateArrangements();
        }

        private void ParseInput(string input)
        {
            var splits = input.Split(' ');;
            this.StatusLine = splits[0];

            var groups = splits[1].Split(',');
            foreach (var group in groups)
            {
                this.GroupSize.Add(int.Parse(group));
            }
        }

        private void CalculateArrangements()
        {
            this.BrokenStrings = this.GroupSize.Sum();
            ///find first possible broken group match
            
            foreach (var group in this.GroupSize)
            {
                (int start, int end) = this.FindFirstNotOK(this.StatusLine, 0);
                //Does current group fit?
                
            }
        }

        private Tuple<int, int> FindFirstNotOK(string springLine, int startPos)
        {
            bool inGroup = false;
            bool foundGroup = false;
            int start = 0;
            int end = 0;
            int position = startPos;
            do
            {
                if ((springLine[position] != '.') && (!inGroup))
                {
                    //Found first possibly broken spring
                    start = position;
                    inGroup = true;
                }
                else if ((springLine[position] == '.') && (inGroup))
                {
                    //Found first functional spring since starting a potential bad spring grouping
                    end = position - 1;
                    inGroup = false;
                    foundGroup = true;
                }
                position++;
            }
            while (!foundGroup);
            return Tuple.Create(start, end);
        }
    }

    public static class Helper
    {
        public static List<SpringLine> GetSpringLines(List<string> input)
        {
            List<SpringLine> list = [];
            foreach (var line in input)
            {
                list.Add(new SpringLine(line));
            }
            return list;
        }
    }
}
