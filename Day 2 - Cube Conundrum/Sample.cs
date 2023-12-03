using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2___Cube_Conundrum
{
    class Sample
    {
        private readonly int maxRed = 12;
        private readonly int maxGreen = 13;
        private readonly int maxBlue = 14;

        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public Sample()
        {
            Red = 0;
            Blue = 0;
            Green = 0;
        }

        public static Sample ParseLine(string line)
        {
            Sample sample = new Sample();

            return sample;
        }
        public bool IsValid()
        {
            if ((Red <= maxRed) && (Green <= maxGreen) && (Blue <= maxBlue))
            {
                return true;
            }
            return false;
        }
    }
}

