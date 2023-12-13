using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day13
{

    public static class Helper
    {
        public static List<List<string>> GetPatterns(List<string> input)
        {
            List<List<string>> patterns = [];
            List<string> lines = [];
            foreach (string line in input)
            {
                if (line == string.Empty)
                {
                    //End of Pattern
                    patterns.Add(lines);
                    lines = [];
                }
                else
                {
                    lines.Add(line);
                }
            }
            patterns.Add(lines);
            return patterns;
        }
        public static int FindSmudgedReflections(ref List<string> pattern)
        {
            //Check for smudges
            bool smudge = false;
            var score = 0;
            var count = 0;
            var total = 0;

            bool method1 = false;
            
            if (method1)
            {
                score = FindHorizontalMirrorWithSmudge(ref pattern, out smudge);
                if (smudge)
                {
                    score = FindHorizontalMirror(pattern);
                    total = score;
                }

                if (!smudge)
                {
                    score = FindVerticalMirrorWithSmudge(ref pattern, out smudge);
                    if (smudge)
                    {
                        score = FindVerticalMirror(pattern);
                        total = score;
                    }
                }
                if (!smudge)
                {
                    throw new Exception("ASDFADSF");
                }
            }
            else
            {
                score = FindHorizontalMirrorWithSmudge(ref pattern, out smudge);
                if (smudge)
                {
                    return score;
                }
                score = FindVerticalMirrorWithSmudge(ref pattern,out smudge);
                if (smudge)
                {
                    return score;
                }
                if (!smudge)
                {
                    throw new Exception("ASDFASDFASD");
                }
            }
            
            
            
            
            
         
            
            return total;



            //score = FindHorizontalMirror(pattern);
            //score += FindVerticalMirror(pattern);


            //score = FindHorizontalMirrorWithSmudge(ref pattern, out smudge);
            //if (smudge)
            //{
            //    smudge = false;
            //    var skip = FindVerticalMirrorWithSmudge(ref pattern, out smudge);
            //    if (smudge)
            //    {
            //        throw new Exception("ASDFADSF");
            //    }
            //    //already fixed the smudge, add vertical score
            //    score += FindVerticalMirror(pattern);
            //}
            //else
            //{
            //    score = FindVerticalMirrorWithSmudge(ref pattern, out smudge);
            //    if (!smudge)
            //    {
            //        throw new Exception("sadkjfhads kljfhdas j");
            //    }
            //    smudge = false;
            //    score += FindHorizontalMirror(pattern);
            //    if (smudge)
            //    {
            //        throw new Exception("sadfjkadshg fasdjhkf asdlkjfh asdlkjhf ");
            //    }
            //}

            return score;
        }
        private static int FindHorizontalMirrorWithSmudge(ref List<string> pattern, out bool smudge)
        {
            smudge = false;
            var result = 0;
            var rows = pattern.Count();
            var length = pattern[0].Count();

            for (int row = 0; row < rows - 1; row++)
            {
                //Check for exact matching pair first
                string first = pattern[row];
                string second = pattern[row + 1];
                if (first.Equals(second))
                {
                    //Found a matching pair. Work back to the origin, or until a match fails
                    bool isMirror = WorkRowMatchBackWithSmudge(row, ref pattern, out smudge);
                    if (isMirror)
                    {
                        result = 100 * (row + 1);
                        //Console.WriteLine($"\tHorizontal Split After Row: {row+1}");
                        if (smudge)
                        {
                            break;
                        }
                        
                    }
                }

                //Now check for initial one off match
                var mismatches = 0;
                var chr = 0;
                var mismatch = 0;
                do
                {
                    //var firstChar = pattern[row][chr];
                    //var secondChar = pattern[row + 1][chr];
                    var firstChar = first[chr];
                    var secondChar = second[chr];

                    if (firstChar != secondChar)
                    {
                        mismatches++;
                        mismatch = chr;
                    }
                    chr++;
                }
                while ((mismatches < 2) && (chr < length));
                if (mismatches == 1)
                {
                    //Console.WriteLine($"Found Horzontal Smudge at Row:{row + 1} or Row{row + 2}:  Col{mismatch + 1}");
                    //Have a one off match here at chr
                    //There can be no other smudges, check the rest of the lines normally
                    bool isMirror = WorkRowMatchBack(row, pattern);
                    if (isMirror)
                    {
                        //Console.Write($"Found Initial Horizontal Smudge at Rows: {row + 1},{row + 2} - Col:{mismatch + 1}");
                        //Console.WriteLine($"\tHorizontal Split After Row: {row + 1}");
                        result = 100 * (row + 1);
                        smudge = true;

                        for (int i = 0; i < pattern.Count; i++)
                        {
                            //Console.WriteLine($"\t{pattern[i]}");
                        }
                        //Console.WriteLine();
                        //Replace first smudged char with second
                        string line = pattern[row];
                        StringBuilder cleaned = new StringBuilder(pattern[row]);
                        cleaned[mismatch] = pattern[row+1][mismatch];
                        pattern[row] = cleaned.ToString();

                        for (int i = 0; i < pattern.Count; i++)
                        {
                            //Console.WriteLine($"\t{pattern[i]}");
                        }
                        //Console.WriteLine();

                        break;
                    }
                }
            }
            return result;
        }
        private static bool WorkRowMatchBackWithSmudge(int rowLocation, ref List<string> pattern, out bool smudge)
        {
            bool isMirror = true;
            bool done = false;
            int previous = rowLocation - 1;
            int next = rowLocation + 2;
            var length = pattern[0].Count();
            var foundSmudge = false;
            var mismatchCol = 0;
            var mismatchRow1 = 0;
            var mismatchRow2 = 0;

            do
            {
                //If rowLocation is 2, that means matching pair is 2 and 3.
                if ((previous >= 0) && (next < pattern.Count))
                {
                    var line1 = pattern[previous];
                    var line2 = pattern[next];
                    
                    if ((!line1.Equals(line2)) && !foundSmudge)
                    {
                        if (!foundSmudge)
                        {
                            //Lines dont match, check if a smudge can fix it
                            var mismatches = 0;
                            var chr = 0;
                            //var mismatch = 0;

                            do
                            {
                                var firstChar = pattern[previous][chr];
                                var secondChar = pattern[next][chr];

                                if (firstChar != secondChar)
                                {
                                    mismatches++;
                                    mismatchCol = chr;
                                    mismatchRow1 = previous;
                                    mismatchRow2 = next;
                                }
                                chr++;
                            }
                            while ((mismatches < 2) && (chr < line1.Length));
                            if (mismatches == 1)
                            {
                                //Found the smudge, this line is a match
                                foundSmudge = true;
                                //Console.WriteLine($"Found Horizontal Smudge at Row:{previous + 1} or Row{next + 1} Col:{mismatch + 1}");
                            }
                            else
                            {
                                //Smudge doesnt help
                                done = true;
                                isMirror = false;
                            }
                        }
                        else
                        {
                            isMirror = false;
                            done = true;
                        }

                    }

                    previous--;
                    next++;
                }
                else
                {
                    done = true;
                }
            }
            while (!done);

            if (isMirror && foundSmudge)
            {
                for (int i = 0; i < pattern.Count; i++)
                {
                    //Console.WriteLine($"\t{pattern[i]}");
                }
                //Console.WriteLine();
                //Replace first smudged char with second
                string line = pattern[mismatchRow1];
                StringBuilder cleaned = new StringBuilder(pattern[mismatchRow1]);
                cleaned[mismatchCol] = pattern[mismatchRow2][mismatchCol];
                pattern[mismatchRow1] = cleaned.ToString();
                
                for (int i = 0; i < pattern.Count; i++)
                {
                    //Console.WriteLine($"\t{pattern[i]}");
                }
                //Console.WriteLine();
                //Console.WriteLine($"Found Deep Horizontal Smudge at Rows: {mismatchRow1+1},{mismatchRow2+1} - Col:{mismatchCol+1}");
            }
            smudge = foundSmudge;
            return isMirror;
        }
        public static int FindReflections(List<string> pattern)
        {
            int hScore  = FindHorizontalMirror(pattern);
            int vScore = FindVerticalMirror(pattern);
            return hScore + vScore;
        }
        private static int FindVerticalMirror(List<string> pattern)
        {
            var result = 0;
            var cols = pattern[0].Length;
            //var cols = pattern[0].Count;

            for (int col = 0; col < cols - 1; col++)
            {
                try
                {
                    string first = new string(pattern.Select(p => p[col]).ToList().ToArray());
                    string next = new string(pattern.Select(p => p[col + 1]).ToList().ToArray());

                    if (first.Equals(next))
                    {
                        var isMirror = WorkColumnMatchBack(col, pattern);
                        if (isMirror)
                        {
                            result = col + 1;
                            break;
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }

            return result;
        }
        private static int FindVerticalMirrorWithSmudge(ref List<string> pattern, out bool smudge)
        {
            smudge = false;
            var result = 0;
            var cols = pattern[0].Length;
            

            for (int col = 0; col < cols - 1; col++)
            {
                //Check for exact matching pair first
                string first = new string(pattern.Select(p => p[col]).ToList().ToArray());
                string next = new string(pattern.Select(p => p[col + 1]).ToList().ToArray());

                if (first.Equals(next))
                {
                    //Found a matching pair. Work back to the origin, or until a match fails
                    //WorkRowMatchBackWithSmudge
                    var isMirror = WorkColumnMatchBackWithSmudge(col, ref pattern, out smudge);
                    if (isMirror)
                    {
                        result = col + 1;
                        //Console.WriteLine($"\tVertical Split After Col: {col + 1}");
                        if (smudge)
                        {
                            break;
                        }
                    }
                }

                //Now check for initial one off match
                var mismatches = 0;
                var chr = 0;
                var mismatch = 0;
                do
                {
                    try
                    {
                        var firstChar = first[chr];
                        var secondChar = next[chr];

                        if (firstChar != secondChar)
                        {
                            mismatches++;
                            mismatch = chr;
                        }
                        chr++;
                    }
                    catch (Exception ex) { }
                }
                while ((mismatches < 2) && (chr < first.Length));
                if (mismatches == 1)
                {
                    //Have a one off match here at chr
                    //There can be no other smudges, check the rest of the lines normally
                    //Console.WriteLine($"Found Vertical Smudge at Row:{mismatch + 1} Col{col + 1}");
                    bool isMirror = WorkColumnMatchBack(col, pattern);
                    if (isMirror)
                    {
                        //Console.WriteLine();
                        //Console.WriteLine("===============Fixing Vertical smudge line");
                        for (int i = 0; i < pattern.Count; i++)
                        {
                           //Console.WriteLine($"\t{pattern[i]}");
                        }
                        //Console.WriteLine();
                        //Replace first smudged char with second
                        string line = pattern[mismatch];
                        StringBuilder cleaned = new StringBuilder(pattern[mismatch]);
                        cleaned[col] = pattern[mismatch][col+1];
                        pattern[mismatch] = cleaned.ToString();

                        for (int i = 0; i < pattern.Count; i++)
                        {
                            //Console.WriteLine($"\t{pattern[i]}");
                        }
                        //Console.Write($"Found Initial Vertical Smudge at Row: {mismatch + 1} - Cols:{col + 1},{col + 2}");
                        //Console.WriteLine($" -- Vertical Split After Column: {col + 1}");
                        result = col + 1;
                        smudge = true;
                        break;
                    }
                }
            }

            return result;
        }
        private static int FindHorizontalMirror(List<string> pattern)
        {
            var result = 0;
            var rows = pattern.Count();

            for (int row = 0; row < rows -1; row++)
            {
                string first = pattern[row];
                string second = pattern[row + 1];
                if (first.Equals(second))
                {
                    //Found a matching pair. Work back to the origin, or until a match fails
                    bool isMirror = WorkRowMatchBack(row, pattern);
                    if (isMirror)
                    {
                        result = 100 * (row + 1);
                        break;
                    }
                }
            }
            return result;
        }
        private static bool WorkColumnMatchBackWithSmudge(int column, ref List<string> pattern, out bool smudge)
        {
            bool isMirror = true;
            bool done = false;
            int previous = column - 1;
            int next = column + 2;
            var length = pattern.Count();
            var foundSmudge = false;
            var mismatchRow = 0;
            var mismatchCol1 = 0;
            var mismatchCol2 = 0;
            smudge = false;

            do
            {
                //If rowLocation is 2, that means matching pair is 2 and 3.
                if ((previous >= 0) && (next < pattern[0].Length))
                {
                    string line1 = new string(pattern.Select(p => p[previous]).ToList().ToArray());
                    string line2 = new string(pattern.Select(p => p[next]).ToList().ToArray());
                    
                    if (!line1.Equals(line2))
                    {
                        if (!foundSmudge)
                        {
                            //Lines dont match, check if a smudge can fix it
                            var mismatches = 0;
                            var chr = 0;
                            

                            do
                            {
                                var firstChar = line1[chr];
                                var secondChar = line2[chr];

                                if (firstChar != secondChar)
                                {
                                    mismatches++;
                                    mismatchRow = chr;
                                    mismatchCol1 = previous;
                                    mismatchCol2 = next;
                                }
                                chr++;
                            }
                            while ((mismatches < 2) && (chr < length));
                            if (mismatches == 1)
                            {
                                //Found the smudge, this line is a match
                                foundSmudge = true;
                                //Console.WriteLine($"Found Vertical Smudge at Row:{mismatch + 1} Col{previous + 1} or Col{next + 1}");
                            }
                            else
                            {
                                //Smudge doesnt help
                                done = true;
                                isMirror = false;
                            }
                        }
                        else
                        {
                            isMirror = false;
                            done = true;
                        }
                        
                    }
                    previous--;
                    next++;
                }
                else
                {
                    done = true;
                }
            }
            while (!done);
            
            if (isMirror && foundSmudge)
            {
                //Console.WriteLine();
                //Console.WriteLine("===============Fixing Vertical smudge line");
                for (int i = 0; i < pattern.Count; i++)
                {
                    //Console.WriteLine($"\t{pattern[i]}");
                }
                //Console.WriteLine();
                //Replace first smudged char with second
                string line = pattern[mismatchRow];
                StringBuilder cleaned = new StringBuilder(pattern[mismatchRow]);
                cleaned[mismatchCol1] = pattern[mismatchRow][mismatchCol2];
                pattern[mismatchRow] = cleaned.ToString();

                for (int i = 0; i < pattern.Count; i++)
                {
                    //Console.WriteLine($"\t{pattern[i]}");
                }
                //Console.WriteLine($"Found Deep Vertical Smudge at Row: {mismatchRow + 1} - Cols:{mismatchCol1 + 1},{mismatchCol2 + 1}");
            }

            smudge = foundSmudge;
            return isMirror;
        }
        private static bool WorkColumnMatchBack(int column, List<string> pattern)
        {
            bool isMirror = true;
            bool done = false;
            int previous = column - 1;
            int next = column + 2;

            do
            {
                //If rowLocation is 2, that means matching pair is 2 and 3.
                if ((previous >= 0) && (next < pattern[0].Length))
                {
                    string line1 = new string(pattern.Select(p => p[previous]).ToList().ToArray());
                    string line2 = new string(pattern.Select(p => p[next]).ToList().ToArray());
                    previous--;
                    next++;
                    if (!line1.Equals(line2))
                    {
                        done = true;
                        isMirror = false;
                    }
                }
                else
                {
                    done = true;
                }
            }
            while (!done);

            return isMirror;
        }

        private static bool WorkRowMatchBack(int rowLocation, List<string>pattern)
        {
            bool isMirror = true;
            bool done = false;
            int previous = rowLocation - 1;
            int next = rowLocation + 2;
            do
            {
                //If rowLocation is 2, that means matching pair is 2 and 3.
                if ((previous >= 0) && (next < pattern.Count))
                {
                    var line1 = pattern[previous--];
                    var line2 = pattern[next++];
                    if (!line1.Equals(line2))
                    {
                        done = true;
                        isMirror = false;
                    }
                }
                else
                {
                    done = true;
                }
            }
            while (!done);
            return isMirror;
        }
    }
}
