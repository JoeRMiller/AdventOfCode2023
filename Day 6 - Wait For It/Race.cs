using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2023
{
    public struct Race
    {
        public long Time { get; }
        public long Distance { get; }

        public Race(long time, long distance)
        {
            this.Time = time;
            this.Distance = distance;
        }
    }

    public static class RaceFactory
    {
        public static List<Race> CreateRaces(List<string> input)
        {
            List<Race> races = [];
            List<string> line1 = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> line2 = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            var count = line1.Count();
            for (int i = 1; i < count; i++)
            {
                races.Add(new Race(long.Parse(line1[i]), long.Parse(line2[i])));
            }

            return races;
        }

        public static Race CreateRace(List<string> input) 
        {
            string[] items = input[0].Split(':', StringSplitOptions.TrimEntries);
            string time = Regex.Replace(items[1], @"\s+", "");
           
            items = input[1].Split(':', StringSplitOptions.TrimEntries);
            string distance = Regex.Replace(items[1], @"\s+", "");

            return new Race(long.Parse(time), long.Parse(distance));
        }
    }
}
