using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day15
{
    public enum OperationEnum
    {
        Remove,
        Add
    }
    public class Lens
    {
        public string Word { get; set; }
        public int WordHash { get; private set; }
        public int BoxHash { get; private set; }
        public OperationEnum Operation { get; private set; }
        public int FocalLength { get; private set; }
        public string LensName { get; private set; }

        public Lens(string word)
        {
            this.Word = word;
            CalculateWordValue();
            CalculateBoxValue();
        }

        private void CalculateWordValue()
        {
            int result = 0;
            byte[] bytes = Encoding.ASCII.GetBytes(this.Word);

            foreach (var chr in bytes)
            {
                int value = result + (int)chr;
                value = value * 17;
                value = value % 256;
                result = value;
            }
            this.WordHash = result;
        }

        private void CalculateBoxValue()
        {
            int result = 0;
            byte[] bytes;
            if (this.Word.Contains('-'))
            {
                var splits = this.Word.Split('-');
                this.Operation = OperationEnum.Remove;
                bytes = Encoding.ASCII.GetBytes(splits[0]);
                this.LensName = splits[0];
                
            }
            else
            {
                var splits = this.Word.Split('=');
                this.Operation = OperationEnum.Add;
                this.FocalLength = int.Parse(splits[1]);
                bytes = Encoding.ASCII.GetBytes(splits[0]);
                this.LensName = splits[0];

            }
            
            foreach (var chr in bytes) 
            { 
                int value = result + (int)chr;
                value = value * 17;
                value = value % 256;
                result = value;
            }
            this.BoxHash = result;
        }
    }
    public static class Helper
    {
        public static List<Lens> GetHashList(string input) 
        {
            List<Lens> list = [];

            var splits = input.Split(',');
            foreach (var split in splits) 
            {
                Lens hash = new Lens(split);
                list.Add(hash);
            }

            return list;
        }

        public static Dictionary<int, List<Lens>> GetBoxes() 
        {
            Dictionary<int, List<Lens>> boxes = [];
            for (int i = 0; i < 256; i++)
            {
                List<Lens> hashList = [];
                boxes.Add(i, hashList);
            }
            return boxes;
        }

        public static int CalculateTotalLensPower(Dictionary<int, List<Lens>> boxes)
        {
            int power = 0;

            foreach (var boxNumber in boxes.Keys) 
            {
                var lenses = boxes[boxNumber];
                if (lenses.Count > 0)
                {
                    power += CalculateLensPower(lenses, boxNumber);
                }
                
            }

            return power;
        }

        private static int CalculateLensPower(List<Lens> lenses, int boxNumber)
        {
            int power = 0;

            for (int slot = 1; slot <= lenses.Count; slot++)
            {
                power += (boxNumber + 1) * slot * lenses[slot - 1].FocalLength;
            }

            return power;
        }
    }
}
