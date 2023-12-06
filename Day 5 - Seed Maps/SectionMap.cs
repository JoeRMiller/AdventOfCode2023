using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5___Seed_Maps
{
    public class SectionMap
    {
        public List<long> Sources { get; }
        public List<long> Destinations { get; }
        public List<long> Ranges { get; }

        public SectionMap() 
        {
            this.Sources = [];
            this.Destinations = [];
            this.Ranges = [];
        }

        public void AddMapping(long source, long dest, long range)
        {
            this.Sources.Add(source);
            this.Destinations.Add(dest);
            this.Ranges.Add(range);
        }

        public long GetDestination(long source)
        {
            var result = source;
            int ranges = this.Sources.Count;
            for (int i = 0; i < ranges; i++) 
            { 
                long start = this.Sources[i];
                long end = start + this.Ranges[i] - 1;
                
                if ((source >= start) && (source <= end))
                {
                    long gap = source - start;
                    result = this.Destinations[i] + gap;
                    break;
                }
            }
            return result; ;
        }
    }
}
