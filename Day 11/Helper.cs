using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day11
{
    public class Spacetime : List<List<Movement>>
    {
        
        public Spacetime() 
        {
            
        }
    }

    public struct Movement
    {
        public int Vertical;
        public int Horizontal;

        public Movement(int vertical, int horizontal)
        {
            this.Vertical = vertical;
            this.Horizontal = horizontal;
        }
    }
    
    public struct Coordinate
    {
        public int Row;
        public int Column;

        public Coordinate() : this(0,0)
        {

        }

        public Coordinate(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }
    public static class Helper
    {
        public static Spacetime GetDistanceMap(List<string> input, int darkEnergy)
        {
            Spacetime map = [];
            int rows = input.Count;
            int cols = input[0].Length;

            int rownum = 0;
            foreach (var row in input)
            {
                map.Add(new List<Movement>());
                foreach (var column in row)
                {
                    map[rownum].Add(new Movement(1, 1));
                }
                rownum++;
            }

            for (int i = 0; i < rows; i++)
            {
                if (!input[i].Contains("#"))
                {
                    for (int j = 0; j < input[1].Length; j++)
                    {
                        Movement m = map[i][j];
                        m.Vertical = darkEnergy;
                        map[i][j] = m;
                    }
                }
            }


            for (int i = 0; i < cols; i++)
            {
                List<char> tiles = input.Select(s => s[i]).ToList();
                if (!tiles.Contains('#'))
                {
                    Debug.WriteLine($"Col:{i} is empty");
                    List<Movement> movements = map[i];
                    for (int j = 0; j < movements.Count; j++)
                    {
                        Movement movement = map[i][j];
                        movement.Horizontal = darkEnergy;
                        map[j][i] = movement;
                    }
                }
            }

            return map;
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

        public static long MapPaths(List<Coordinate> coordinates, List<string> universe, Spacetime map)
        {
            long total = 0;
            for (int galaxy = 0; galaxy < coordinates.Count; galaxy++)
            {
                
                Coordinate local = coordinates[galaxy];
                Debug.WriteLine($"Calculating Paths from Galaxy {galaxy} {local.Row + 1}:{local.Column + 1}");
                for (int nextGalaxy = galaxy + 1; nextGalaxy < coordinates.Count; nextGalaxy++)
                {
                    Coordinate next = coordinates[nextGalaxy];
                    //Select all the vertical steps from the map.
                    //starting at local.row, local.column ending on next.row, local.column
                    var vertical = 0;
                    var horizontal = 0;
                    var start = 0;
                    var end = 0;
                    if (local.Row < next.Row)
                    {
                        //Work Down
                        start = local.Row;
                        end = next.Row;
                    }
                    else
                    {
                        //Work Up
                        start = next.Row;
                        end = local.Row;
                    }

                    for (int i = start; i < end; i++)
                    {
                        Movement move = map[i][local.Column];
                        vertical += move.Vertical;
                    }
                    Debug.Write($"\tVertical:{vertical} ");

                    start = 0;
                    end = 0;
                    if (local.Column < next.Column)
                    {
                        //work Right
                        start = local.Column;
                        end = next.Column;
                    }
                    else
                    {
                        //Work left
                        start = next.Column;
                        end = local.Column;
                    }

                    for (int i = start; i < end; i++)
                    {
                        Movement move = map[local.Row][i];
                        horizontal += move.Horizontal;
                    }
                    Debug.Write($"\tHorizontal:{horizontal} ");

                    total += vertical + horizontal;
                    Debug.WriteLine($"Total:{total}");
                }
            }

            return total;
        }

        public static void PrintSpacetime(Spacetime map)
        {
            foreach (var line in map)
            {
                foreach (var location in line)
                {
                    Console.Write($"{location.Vertical},{location.Horizontal} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();   
        }
    }
}
