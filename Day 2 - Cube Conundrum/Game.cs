using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2___Cube_Conundrum
{
    internal class Game
    {
        public int ID { get; set; }
        
        public List<Sample> Samples { get; }

        public Game(string initLine)
        {
            this.Samples = new List<Sample>();
            this.ParseGameLine(initLine);
        }

        public bool IsValid()
        {
            var result = true;
            foreach (var sample in this.Samples)
            {
                if (sample.IsValid() == false)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private void ParseGameLine(string line)
        {

        }



    }
}
