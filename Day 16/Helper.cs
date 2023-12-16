using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2023.Day16
{
    public enum TileType
    {
        Empty, Backslash, Forwardslash, VerticalSplit, HorizontalSplit
    }

    public enum Direction
    {
        Right, Left, Up, Down
    }
    public class Tile
    {
        public TileType Type {  get; private set; }
        public bool Energized { get; set; }
        public bool Up {  get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }

        public Tile (char value)
        {
            switch (value)
            {
                case '|':
                    this.Type = TileType.VerticalSplit; break;
                case '-':
                    this.Type = TileType.HorizontalSplit; break;
                case '\\':
                    this.Type = TileType.Backslash; break;
                case '/':
                    this.Type = TileType.Forwardslash; break;
                default:
                    this.Type = TileType.Empty; break;

            }
            this.Energized = false;
            this.Up = false;
            this.Down = false;
            this.Left = false;
            this.Right = false;
        }
    }
    public static class Helper
    {
        public static Tile[][] BuildMap(char[][] input)
        {
            //Assume rectangular map
            var rows = input.Count();
            var columns = input[0].Length;
            Tile[][] map = new Tile[rows][];

            for (int row = 0; row < input.Count(); row++)
            {
                var tileLine = new Tile[columns];
                for (int col = 0; col < input[row].Count(); col++)
                {
                    tileLine[col] = new Tile(input[row][col]);
                }
                map[row] = tileLine;
            }

            return map;
        }

        public static void TraverseMap(Tile[][] map, int startRow, int startCol, Direction direction)
        {
            //Energize tile
            Tile tile = map[startRow][startCol];
            tile.Energized = true;
            SetTileDirection(tile, direction);
            
            Direction next1;
            Direction next2;
            switch (tile.Type)
            {
                case TileType.Empty:
                    next1 = direction;
                    MoveNext(map, startRow, startCol, next1);
                    break;

                case TileType.Forwardslash:
                case TileType.Backslash:
                    next1 = GetNextBounceDirection(tile, direction);
                    MoveNext(map, startRow, startCol, next1);
                    break;

                case TileType.HorizontalSplit:
                case TileType.VerticalSplit:
                    (next1, next2) = GetNextSplitDirections(tile);
                    MoveNext(map, startRow, startCol, next1);
                    MoveNext(map, startRow, startCol, next2);
                    break;

            }
            
        }

        public static int CountEnergized(Tile[][] map)
        {
            var result = 0;
            foreach (var line in map)
            {
                foreach (var tile in line)
                {
                    if (tile.Energized)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private static void MoveNext(Tile[][] map, int currentRow, int currentCol, Direction direction)
        {
            int nextRow = currentRow;
            int nextCol = currentCol;
            int maxRow = map.Count();
            int maxCol = map[0].Count();
            Tile previous = map[currentRow][currentCol];

            switch (direction)
            {
                case Direction.Left:
                    currentCol--; break;
                case Direction.Right:
                    currentCol++; break;
                case Direction.Up:
                    currentRow--; break;
                case Direction.Down:
                    currentRow++; break;
            }

            if ((currentRow < 0) || (currentRow == maxRow) || (currentCol < 0) || (currentCol == maxCol))
            {
                //Hit a boundary here, done tracing path
                return;
            }

            Tile next = map[currentRow][currentCol];

            //Now check if we are on a previously energized tile, going in the same direction
            if (next.Energized)
            {
                switch (direction)
                {
                    case Direction.Left:
                        if (next.Left)
                        {
                            //Hit an energized tile going in the same direction, done tracing path
                            return;
                        }
                        break;
                    case Direction.Right:
                        if (next.Right)
                        {
                            //Hit an energized tile going in the same direction, done tracing path
                            return;
                        }
                        break;
                    case Direction.Up:
                        if (next.Up)
                        {
                            //Hit an energized tile going in the same direction, done tracing path
                            return;
                        }
                        break;
                    case Direction.Down:
                        if (next.Down)
                        {
                            //Hit an energized tile going in the same direction, done tracing path
                            return;
                        }
                        break;
                }
            }
            
            //Energize the tile, and set the direction we are travelling
            next.Energized = true;
            SetTileDirection(next, direction);

            Direction next1;
            Direction next2;
            switch (next.Type)
            {
                case TileType.Empty:
                    next1 = direction;
                    MoveNext(map, currentRow, currentCol, next1);
                    break;

                case TileType.Forwardslash:
                case TileType.Backslash:
                    next1 = GetNextBounceDirection(next, direction);
                    MoveNext(map, currentRow, currentCol, next1);
                    break;

                case TileType.HorizontalSplit:
                case TileType.VerticalSplit:
                    (next1, next2) = GetNextSplitDirections(next);
                    MoveNext(map, currentRow, currentCol, next1);
                    MoveNext(map, currentRow, currentCol, next2);
                    break;

            }
        }

        private static Direction GetNextBounceDirection(Tile tile, Direction direction)
        {
            Direction next = direction;
            switch (tile.Type)
            {
                case TileType.Forwardslash:
                    if (direction == Direction.Right)
                    {
                        next = Direction.Up;
                    }
                    else if (direction == Direction.Left)
                    {
                        next = Direction.Down;
                    }
                    else if (direction == Direction.Up)
                    {
                        next = Direction.Right;
                    }
                    else
                    {
                        next = Direction.Left;
                    }
                    break;

                case TileType.Backslash:
                    if (direction == Direction.Right)
                    {
                        next = Direction.Down;
                    }
                    else if (direction == Direction.Left)
                    {
                        next = Direction.Up;
                    }
                    else if (direction == Direction.Up)
                    {
                        next = Direction.Left;
                    }
                    else
                    {
                        next = Direction.Right;
                    }

                    break;
            }
            return next;
        }

        private static Tuple<Direction, Direction> GetNextSplitDirections(Tile tile)
        {
            Direction next1 = Direction.Down;
            Direction next2 = Direction.Down;

            switch (tile.Type)
            {
                case TileType.HorizontalSplit:
                    next1 = Direction.Left;
                    next2 = Direction.Right;
                    break;
                case TileType.VerticalSplit:
                    next1 = Direction.Up;
                    next2 = Direction.Down;
                    break;
            }

            return new Tuple<Direction, Direction>(next1, next2);
        }

        private static void SetTileDirection(Tile tile, Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    tile.Left = true; break;
                case Direction.Right:
                    tile.Right = true; break;
                case Direction.Up:
                    tile.Up = true; break;
                case Direction.Down:
                    tile.Down = true; break;
            }

        }

        public static void PrintMap(Tile[][] map)
        {
            foreach (var line in map)
            {
                foreach (var tile in line)
                {
                    if (tile.Energized)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        switch (tile.Type)
                        {
                            case TileType.Empty:
                                Console.Write('.');
                                break;
                            case TileType.HorizontalSplit:
                                Console.Write('-');
                                break;
                            case TileType.VerticalSplit:
                                Console.Write('|');
                                break;
                            case TileType.Backslash:
                                Console.Write('\\');
                                break;
                            case TileType.Forwardslash:
                                Console.Write('/');
                                break;
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
