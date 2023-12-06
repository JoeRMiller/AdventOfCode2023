using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5___Seed_Maps
{
    public class SeedMap
    {
        public List<long> Start {  get; }
        public List<long> End { get; }
        public int Ranges { get; private set; }

        public SeedMap()
        {
            this.Start = [];
            this.End = [];
            this.Ranges = 0;
        }

        public void AddRange(long start, long range)
        {
            this.Start.Add(start);
            this.End.Add(start + range - 1);
            this.Ranges++;
        }
    }
}
