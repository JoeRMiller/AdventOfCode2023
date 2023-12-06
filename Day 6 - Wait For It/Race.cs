using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode2023
{
    public struct Race
    {
        public int Time { get; }
        public int Distance { get; }

        public Race(int time, int distance)
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
                races.Add(new Race(int.Parse(line1[i]), int.Parse(line2[i])));
            }

            return races;
        }
    }
}
