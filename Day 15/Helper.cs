using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day15
{
    public class Hash
    {
        public string Word { get; set; }
        public int Value { get; private set; }

        public Hash(string word)
        {
            this.Word = word;
            CalculateValue();
        }

        private void CalculateValue()
        {
            int result = 0;
            byte[] bytes= Encoding.ASCII.GetBytes(this.Word);
            foreach (var chr in bytes) 
            { 
                int value = result + (int)chr;
                value = value * 17;
                value = value % 256;
                result = value;
            }
            this.Value = result;
        }
    }
    public static class Helper
    {
        public static List<Hash> GetHashList(string input) 
        {
            List<Hash> list = [];

            var splits = input.Split(',');
            foreach (var split in splits) 
            {
                Hash hash = new Hash(split);
                list.Add(hash);
            }

            return list;
        }
    }
}
