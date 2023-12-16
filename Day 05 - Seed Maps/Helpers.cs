using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Day_5___Seed_Maps
{
    public class Helpers
    {
        public static List<long> StringToLongList(string str)
        {
            var list = new List<long>();
            string[] strArr = str.Split(' ');
            foreach (string str2 in strArr)
            {
                list.Add(long.Parse(str2.Trim()));
            }

            return list;
        }

        public static SeedMap GetSeedMap(string str)
        {
            SeedMap map = new();
            string[] strArr = str.Split(' ');
            int pairs = strArr.Length / 2;
            for (int i = 0; i < pairs; i++)
            {
                long start = long.Parse(strArr[2 * i]);
                long range = long.Parse(strArr[2 * i + 1]);
                map.AddRange(start, range);
            }
            
            return map;
        }
                
        public static int ReadSection(List<string> lines, int startPosition, string header, List<string> sections)
        {
            if (lines[startPosition] == header)
            {
                startPosition++;
                string sectionLine = lines[startPosition++];
                while (sectionLine != String.Empty)
                {
                    sections.Add(sectionLine);
                    if (startPosition < lines.Count)
                    {
                        sectionLine = lines[startPosition++];
                    }
                    else
                    {
                        sectionLine = String.Empty;
                    }
                }
            }
            else
            {
                throw new ArgumentException($"{lines[startPosition]} doesnt match header: {header}");
            }
            return startPosition;
        }

        public static SectionMap MapSection(List<string> section)
        {
            SectionMap map = new SectionMap();
            foreach (string line in section)
            {
                List<long> list = StringToLongList(line);
                long destStart = list[0];
                long sourceStart = list[1];
                long range = list[2];
                map.AddMapping(sourceStart, destStart, range);
            }
            return map;
        }
        
        public static long GetMapLocation(long source, SectionMap map)
        {
            return map.GetDestination(source);
        }
    }
}
