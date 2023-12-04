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
            this.ParseGameInfo(initLine);
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

        private void ParseGameInfo(string line)
        {
            string[] splits = line.Split(": ");
            string[] gameInfo = splits[0].Split(' ');
            this.ID = int.Parse(gameInfo[1]);

            ParseGamesLine(splits[1]);
        }

        private void ParseGamesLine(string line)
        {
            string[] games = line.Split("; ");
            foreach (string sampleInfo in games)
            {
                ParseSampleInfo(sampleInfo);
            }
        }

        private void ParseSampleInfo(string info)
        {
            Sample sample = Sample.ParseLine(info);
            this.Samples.Add(sample);
        }
    }
}
