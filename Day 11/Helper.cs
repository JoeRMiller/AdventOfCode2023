using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day11
{
    public struct Coordinate
    {
        public int Row;
        public int Column;

        public Coordinate(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }
    public static class Helper
    {
        
        public static List<string> GetUniverse(List<string> input)
        {
            int rows = input.Count;
            int cols = input[0].Length;

            List<int> emptyRows = [];
            for (int i = 0; i < rows; i++)
            {
                if (!input[i].Contains("#"))
                {
                    emptyRows.Add(i);
                }
            }

            int iterations = 0;
            foreach (var row in emptyRows)
            {
                StringBuilder sb = new();
                for (int i = 0; i < cols; i++)
                {
                    sb.Append(".");
                }
                string newRow = sb.ToString();
                input.Insert(row + iterations++, newRow);
            }

            List<int> emptyColumns = [];
            for (int i = 0; i < cols; i++)
            {
                List<string> column = input.Select(s => s[i].ToString()).ToList();
                if (!column.Contains("#"))
                {
                    emptyColumns.Add(i);
                }
            }

            rows = input.Count;
            iterations = 0;
            foreach (var column in emptyColumns)
            {
                for (int i = 0; i < rows; i++)
                {
                    input[i] = input[i].Insert(column + iterations, ".");
                }
                iterations++;
            }

            return input;
        }

        public static List<Coordinate> FindGalaxies (List<string> universe)
        {
            List<Coordinate> coordinates = [];

            for (int row = 0; row < universe.Count; row++)
            {
                for (int col = 0; col < universe[row].Length; col++)
                {
                    if (universe[row][col] == '#')
                    {
                        Coordinate coordinate = new Coordinate(row, col);
                        coordinates.Add(coordinate);
                    }
                }
            }

            return coordinates;
        }

        public static int MapPaths(List<Coordinate> coordinates, List<string> universe)
        {
            int total = 0;
            for (int galaxy = 0; galaxy < coordinates.Count; galaxy++)
            {
                
                Coordinate local = coordinates[galaxy];
                Console.WriteLine($"Calculating Paths from Galaxy {galaxy} {local.Row + 1}:{local.Column + 1}");
                for (int nextGalaxy = galaxy + 1; nextGalaxy < coordinates.Count; nextGalaxy++)
                {
                    Coordinate next = coordinates[nextGalaxy];
                    var vert = Math.Abs(local.Row - next.Row);
                    var hor = Math.Abs(local.Column - next.Column);
                    var path = vert + hor;
                    total += path;
                }
            }

            return total;
        }

        public static void PrintUniverse(List<string> universe)
        {
            foreach (var line in universe)
            {
                Console.WriteLine(line);
            }
        }
    }
}
