using AdventofCode2023.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day12
{
    public class SpringLine
    {
        public string StatusLine { get; private set; } = string.Empty;
        public List<int> Groups { get; } = new List<int>();
        public int BrokenStrings { get; private set; }
        public int Arrangements { get; private set; }
        public List<bool> IsUnknown { get; private set; }

        private string _regex;
        private Dictionary<string, bool> Matches;





        public SpringLine(string input)
        {
            this.Groups = [];
            this.IsUnknown = [];
            this.Matches = [];
            this.Arrangements = 0;
            this.ParseInput(input);
            this.ParseStatusLine();
            this.BuildRegexPattern();
            this.CalculateArrangements();
        }

        private void ParseInput(string input)
        {
            var splits = input.Split(' '); ;
            this.StatusLine = splits[0];

            var groups = splits[1].Split(',');
            foreach (var group in groups)
            {
                this.Groups.Add(int.Parse(group));
            }
        }

        private void ParseStatusLine()
        {
            foreach (var chr in this.StatusLine)
            {
                if (chr == '?')
                {
                    this.IsUnknown.Add(true);
                }
                else
                {
                    this.IsUnknown.Add(false);
                }
            }
        }

        private void BuildRegexPattern()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("^\\.*");
            for (int x = 0; x < this.Groups.Count; x++)
            {
                sb.Append('(');
                for (int i = 0; i < this.Groups[x]; i++)
                {
                    sb.Append("\\#");
                }
                sb.Append(')');
                if (x < this.Groups.Count - 1)
                {
                    sb.Append("\\.+");
                }
            }
            sb.Append("(?!.*\\#)");
            _regex = sb.ToString();
        }

        private void CalculateArrangements()
        {
            this.BrokenStrings = this.Groups.Sum();

            //Get list of all unknown indices
            var unknowns = this.StatusLine.Select((character, index) => new { character, index })
                                          .Where(x => x.character == '?')
                                          .Select(x => x.index)
                                          .ToList();

            string updated = this.StatusLine.ReplaceCharAt('#', unknowns[0]);
            updated = updated.Replace('?', '.');
            CheckMatch(updated, 0, unknowns);

            updated = this.StatusLine.ReplaceCharAt('.', unknowns[0]);
            updated = updated.Replace('?', '.');
            CheckMatch(updated, 0, unknowns);
        }

        //Index is where we are starting the recursive checking
        private void CheckMatch(string line, int index, List<int> indices)
        {
            if (!this.Matches.ContainsKey(line))
            {
                Regex r = new Regex(_regex);
                var matches = r.Matches(line);
                bool match = true;
                if (matches.Count == 1)
                {
                    //Have a regex match
                    if (matches[0].Groups.Count - 1 == this.Groups.Count)
                    {
                        this.Arrangements++;
                    }
                }
                this.Matches.Add(line, true);
            }

            //Set up the next check
            int nextIndex = index + 1;
            if (nextIndex != indices.Count)
            {
                //more to test
                string updated = line.ReplaceCharAt('#', indices[nextIndex]);
                updated = updated.Replace('?', '.');
                CheckMatch(updated, nextIndex, indices);

                updated = line.ReplaceCharAt('.', indices[nextIndex]);
                updated = updated.Replace('?', '.');
                CheckMatch(updated, nextIndex, indices);
            }
        }
    }

    public static class Helper
    {
        public static List<SpringLine> GetSpringLines(List<string> input)
        {
            return input.AsParallel()
                        .Select(str => new SpringLine(str))
                        .ToList();
        }

        public static string PrintStringline(SpringLine springLine)
        {
            //Console.WriteLine("In Action");
            StringBuilder sb = new StringBuilder();
            sb.Append($"Status Line: {springLine.StatusLine}\n");
            sb.Append($"Broken Group Count: {springLine.Groups.Count()}\n");
            sb.Append($"Broken Springs: {springLine.BrokenStrings}\n");
            sb.Append($"\n");
            return sb.ToString();
        }
    }
}
