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
}
