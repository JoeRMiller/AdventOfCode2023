using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        
        public Location ()
        {
            line = -1;
            chr = -1;
            nextLine = -1;
            nextChr = -1;
            nextMarker = '.';
            direction = Direction.None;

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
    }
}
