using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode2023.Day10
{
    public enum Direction
    {
        Up, Down, Left, Right, None
    }

    public struct Marker
    {
        public char c;
        public int line;
        public int chr;
        public Direction direction;
        public Direction previous;
    }
    public struct Location
    {
        public int line;
        public int chr;
        public int nextLine;
        public int nextChr;
        public char marker;
        public char nextMarker;
        public Direction direction;
        public Direction previous;
        
        public Location ()
        {
            line = -1;
            chr = -1;
            nextLine = -1;
            nextChr = -1;
            nextMarker = '.';
            direction = Direction.None;
            previous = Direction.None;
        }


    }
    public static class Helper
    {
        public static Marker GetNextMarker(Marker marker, List<string> input)
        {
            Marker next = marker;
            switch (marker.direction)
            {
                case Direction.Left:
                    next.chr--;
                    break;

                case Direction.Right:
                    next.chr++;
                    break;

                case Direction.Up:
                    next.line--;
                    break;

                case Direction.Down:
                    next.line++;
                    break;
            }
            next.c = input[next.line][next.chr];
            next.direction = GetNextMarkerDirection(next);
            next.previous = marker.direction;
            return next;
        }

        public static Direction GetNextMarkerDirection (Marker marker)
        {
            Direction d = Direction.None;
            switch (marker.c)
            {
                case '|':
                    if (marker.direction == Direction.Down)
                    {
                        d = Direction.Down;
                    }
                    else
                    {
                        d = Direction.Up;
                    }
                    break;

                case '-':
                    if (marker.direction == Direction.Left)
                    {
                        d = Direction.Left;
                    }
                    else
                    {
                        d = Direction.Right;
                    }
                    break;
                
                case 'L':
                    if (marker.direction == Direction.Down)
                    {
                        d = Direction.Right;
                    }
                    else
                    {
                        d = Direction.Up;
                    }
                    break;

                case 'J':
                    if (marker.direction == Direction.Down)
                    {
                        d = Direction.Left;
                    }
                    else
                    {
                        d = Direction.Up;
                    }
                    break;

                case '7':
                    if (marker.direction == Direction.Up)
                    {
                        d = Direction.Left;
                    }
                    else
                    {
                        d = Direction.Down;
                    }
                    break;

                case 'F':
                    if (marker.direction == Direction.Up)
                    {
                        d = Direction.Right;
                    }
                    else
                    {
                        d = Direction.Down;
                    }
                    break;

                case 'S':
                    
                    break;

            }
            return d;
        }
        public static Tuple<int, int> FindStart(List<string> input)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].Contains('S'))
                {
                    y = i;
                    x = input[i].IndexOf('S');
                    break;
                }
                
            }
            return Tuple.Create(y, x);
        }
        
        public static Direction SetInitialDirection(int line, int chr, List<string> input)
        {
            if (line > 0)
            {
                char c = input[line - 1][chr];
                if ((c == '|') || (c == '7') || (c == 'F'))
                {
                    //Next move is up
                    return Direction.Up;
                }
            }


            if ((line + 1) < input.Count)
            {
                char c = input[line + 1][chr];
                if ((c == '|') || (c == 'L') || (c == 'J'))
                {
                    //Next move is down
                    return Direction.Down;
                }
            }

            if (chr > 0)
            {
                char c = input[line][chr - 1];
                if ((c == '-') || (c == 'L') || (c == 'F'))
                {
                    //Next move is left
                    return Direction.Left;
                }
            }


            if ((chr + 1) < input[line].Length)
            {
                char c = input[line][chr + 1];
                if ((c == '-') || (c == '7') || (c == 'J'))
                {
                    //Next move is right
                    return Direction.Right;
                }
            }

            throw new Exception($"Miscalculated route: {line},{chr} loc:{input[line][chr]}");
        }

        public static int CountCrossedWalls(int line, int chr, List<bool> mapLine, List<Marker> chain, string inputLine)
        {
            //Check for an S to the right, if found, we're going to count walls left for simplicity
            var result = 0;

            bool foundS = false;
            for (int i = chr; i < inputLine.Length; i++)
            {
                if (inputLine[i] == 'S')
                {
                    foundS = true;
                    break;
                }
            }

            if (foundS)
            {
                //Scan left
                result = CountToLeft(line, chr, mapLine, chain, inputLine);
            }
            else
            {
                //Scan right
                result = CountToRight(line, chr, mapLine, chain, inputLine);
            }


            return result;
        }

        public static int CountToRight(int line, int chr, List<bool> mapLine, List<Marker> chain, string inputLine)
        {
            int crossed = 0;
            int currentChr = chr + 1;
            bool inRun = false;
            Direction enteredFrom = Direction.None;
            Direction exitedTo = Direction.None;
            do
            {
                if (mapLine[currentChr])
                {
                    //Found a pipe here
                    //Get the marker
                    Marker marker = chain.Find(c => ((c.line == line) && (c.chr == currentChr)));
                    char charVal = marker.c;

                    if (charVal == '|')
                    {
                        crossed++;
                    }

                    //Look for pipe char to start run
                    if (((charVal == 'L') || (charVal == 'F')) && (inRun == false))
                    {
                        //In a run here
                        inRun = true;
                        if (charVal == 'F')
                        {
                            exitedTo = Direction.Down;
                        }
                        else
                        {
                            exitedTo = Direction.Up;
                        }

                    }

                    else if (((charVal == 'J') || (charVal == '7')) && (inRun == true))
                    {
                        //ending a run here
                        if (charVal == 'J')
                        {
                            enteredFrom = Direction.Up;
                        }
                        else
                        {
                            enteredFrom = Direction.Down;
                        }
                        
                        //var exitedTo = marker.direction;
                        if (exitedTo != enteredFrom)
                        {
                            crossed++;
                            enteredFrom = Direction.None;
                            exitedTo = Direction.None;
                        }
                        inRun = false;
                    }
                }
                else { inRun = false; }
                currentChr++;
            }
            while (currentChr < inputLine.Length);

            return crossed;
        }

        public static int CountToLeft(int line, int chr, List<bool> mapLine, List<Marker> chain, string inputLine)
        {
            int crossed = 0;
            int currentChr = chr - 1;
            bool inRun = false;
            Direction enteredFrom = Direction.None;
            Direction exitedTo = Direction.None;
            do
            {
                if (mapLine[currentChr])
                {
                    //Found a pipe here
                    //Get the marker
                    Marker marker = chain.Find(c => ((c.line == line) && (c.chr == currentChr)));
                    char charVal = marker.c;

                    if (charVal == '|')
                    {
                        crossed++;
                    }

                    //Look for pipe char to start run
                    if (((charVal == '7') || (charVal == 'J')) && (inRun == false))
                    {
                        //In a run here
                        inRun = true;
                        if (charVal == '7')
                        {
                            exitedTo = Direction.Down;
                        }
                        else
                        {
                            exitedTo = Direction.Up;
                        }

                    }

                    else if (((charVal == 'F') || (charVal == 'L')) && (inRun == true))
                    {
                        //ending a run here
                        if (charVal == 'L')
                        {
                            enteredFrom = Direction.Up;
                        }
                        else
                        {
                            enteredFrom = Direction.Down;
                        }

                        //var exitedTo = marker.direction;
                        if (exitedTo != enteredFrom)
                        {
                            crossed++;
                            enteredFrom = Direction.None;
                            exitedTo = Direction.None;
                        }
                        inRun = false;
                    }
                }
                else { inRun = false; }
                currentChr--;
            }
            while (currentChr >= 0);
            return crossed;

            /*

            int crossed = 0;
            int currentChr = chr - 1;
            bool inRun = false;
            Direction entered = Direction.None;
            do
            {
                if (mapLine[currentChr])
                {
                    //Found a pipe here
                    //Get the marker
                    Marker marker = chain.Find(c => ((c.line == line) && (c.chr == currentChr)));
                    char charVal = marker.c;

                    if (charVal == '|')
                    {
                        crossed++;
                    }

                    //Look for pipe char to start run
                    if (((charVal == 'L')
                        || (charVal == 'J')
                        || (charVal == '7')
                        || (charVal == 'F'))
                        && (inRun == false))
                    {
                        //In a run here
                        inRun = true;
                        //crossed++;
                        //entered = marker.direction;
                        entered = marker.previous;
                    }

                    else if (((charVal == 'L')
                        || (charVal == 'J')
                        || (charVal == '7')
                        || (charVal == 'F'))
                        && (inRun == true))
                    {
                        //ending a run here
                        if (marker.direction == entered)
                        {
                            crossed++;
                            entered = Direction.None;
                        }
                        inRun = false;
                    }
                }
                else { inRun = false; }
                currentChr--;
            }
            while (currentChr >= 0);

            return crossed;
            */
        }

        public static void PrintMap(List<List<bool>> map)
        {
            foreach (var mapLine in map)
            {
                foreach (var mapItem in mapLine)
                {
                    if (mapItem)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

