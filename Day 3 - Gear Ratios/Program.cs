// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Text;

Console.WriteLine("Advent of Code Day 3 - Gear Ratios");

//HashSet<PartLocation> partLocations = new();
SortedSet<PartLocation> partLocations = new();
int globalCount = 0;

var input = File.OpenText("..\\..\\..\\input.txt");

int arrayLength = 140;

int partsSum = 0;
int lineNumber = 0;
Char[][] schematic = new Char[arrayLength][];
bool[][] partMap = new bool[arrayLength][];
bool[][] gearMap = new bool[arrayLength][];

//Read input, create schematic and symbol map arrays
string? line;
while ((line = input.ReadLine()) != null)
{
    char[] mapLine = line.ToCharArray();
    schematic[lineNumber] = mapLine;
    partMap[lineNumber] = new bool[arrayLength];
    gearMap[lineNumber] = new bool[arrayLength];
    
    for (int i = 0;  i < mapLine.Length; i++)
    {
        //if ((!Char.IsDigit(mapLine[i]) && (mapLine[i] != '.')))
        if (matchPart(mapLine[i]))
        {
            partMap[lineNumber][i] = true;
        }
        else
        {
            partMap[lineNumber][i] = false;
        }

        //if (mapLine[i] == '*')
        if (matchGear(mapLine[i]))
        {
            gearMap[lineNumber][i] = true;
        }
        else
        {
            partMap[lineNumber][i] = false;
        }
    }
    lineNumber++;
}

partLocations = ProcessMapForParts();

foreach (PartLocation part in partLocations)
{
    Console.WriteLine($"Found Part: {part}");
    partsSum += part.PartNumber;
}


Console.WriteLine($"Global Count: {globalCount}");
Console.WriteLine($"HashSet Count: {partLocations.Count}");
Console.WriteLine($"Parts Sum: {partsSum}"); //357407

bool matchPart(char chr)
{
    bool result = false;

    if ((!Char.IsDigit(chr) && (chr != '.')))
    {
        result = true;
    }

    return result;
}

bool matchGear(char chr)
{
    bool result = false;

    if (chr == '*')
    {
        result = true;
    }

    return result;
}

void ProcessMapForGears()
{
    void ProcessMapForParts()
    {
        //read symbol map, and identify surrounding digits
        for (int row = 0; row < arrayLength; row++)
        {
            for (int col = 0; col < arrayLength; col++)
            {
                if (partMap[row][col] == true)
                {
                    //found a symbol, check surrounding 9 locations

                    //above row locations
                    if (row > 0)
                    {
                        FindParts(row - 1, col);
                        FindParts(row - 1, col + 1);
                        FindParts(row - 1, col + 2);
                    }

                    //same row locations
                    FindParts(row, col);
                    FindParts(row, col + 2);

                    //lower row locations
                    if (row < arrayLength)
                    {
                        FindParts(row + 1, col);
                        FindParts(row + 1, col + 1);
                        FindParts(row + 1, col + 2);
                    }
                }
            }
        }
    }
}
SortedSet<PartLocation>  ProcessMapForParts(bool[][] map)
{
    SortedSet<PartLocation> locations = new();
    //read symbol map, and identify surrounding digits
    for (int row = 0; row < arrayLength; row++)
    {
        for (int col = 0; col < arrayLength; col++)
        {
            if (partMap[row][col] == true)
            {
                //found a symbol, check surrounding 9 locations
                //PartLocation? location;
                //above row locations
                if (row > 0)
                {
                    //location = FindParts(row - 1, col);
                    //location = FindParts(row - 1, col + 1);
                    //location = FindParts(row - 1, col + 2);
                    AddLocation(FindParts(row - 1, col), ref locations);
                    AddLocation(FindParts(row - 1, col + 1), ref locations);
                    AddLocation(FindParts(row - 1, col + 2), ref locations);
                }

                //same row locations
                //location = FindParts(row, col);
                //location = FindParts(row, col + 2);
                AddLocation(FindParts(row, col), ref locations);
                AddLocation(FindParts(row, col + 2), ref locations);

                //lower row locations
                if (row < arrayLength)
                {
                    //location = FindParts(row + 1, col);
                    //location = FindParts(row + 1, col + 1);
                    //location = FindParts(row + 1, col + 2);
                    AddLocation(FindParts(row + 1, col), ref locations);
                    AddLocation(FindParts(row + 1, col + 1), ref locations);
                    AddLocation(FindParts(row + 1, col + 2), ref locations);
                }
            }
        }
    }
    return locations;
}

void AddLocation(PartLocation? location, ref SortedSet<PartLocation> locations)
{
    if (location != null)
    {
        locations.Add((PartLocation)location);
    }
}

PartLocation? FindParts(int row, int col)
{
    //Look to the left
    if (col > 0)
    {
        int currentCol = col - 1;
        int firstCol, lastCol;
        char chr = schematic[row][currentCol];
        if (Char.IsDigit(chr))
        {
            //Found one. Look backwards for start of part number
            bool foundDigit = true;
            lastCol = currentCol;
            firstCol = currentCol;
            while (foundDigit && (currentCol > 0))
            {

                currentCol--;
                chr = schematic[row][currentCol];
                if (Char.IsDigit(chr))
                {
                    firstCol = currentCol;
                }
                else
                {
                    foundDigit = false;
                }
            }

            //Now look forward for the end
            currentCol = lastCol;
            foundDigit = true;
            while (foundDigit && (currentCol < arrayLength))
            {
                chr = schematic[row][currentCol];
                if (Char.IsDigit(chr))
                {
                    lastCol = currentCol;
                }
                else
                {
                    foundDigit = false;
                }
                currentCol++;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = firstCol; i <= lastCol; i++)
            {
                sb.Append(schematic[row][i]);
            }
            int partNumber = int.Parse(sb.ToString());

            PartLocation location = new PartLocation(firstCol, lastCol, row, partNumber);
            //partLocations.Add(location);
            return location;
            //globalCount++;
        }
    }
    return null;
}

struct PartLocation : IComparable
{
    public int StartCol { get; }
    public int EndCol {  get; }
    public int Row { get; }
    public int PartNumber {  get; }
    public int Order { get; }

    public PartLocation(int startCol, int endCol, int row, int partNumber)
    {
        this.StartCol = startCol;
        this.EndCol = endCol;
        this.Row = row;
        this.PartNumber = partNumber;
        this.Order = this.GetHashCode();
    }

    public override string ToString()
    {
        return $"Part Number:{PartNumber}, Row:{Row + 1}, Start:{StartCol + 1}, End:{EndCol + 1}, Hash:{this.GetHashCode()}";
    }

    public override int GetHashCode()
    {
        return (1000000 * Row) + (1000 * StartCol) + EndCol;//xxx-xxx-xxx
    }

    public int CompareTo(object? obj)
    {
        if (obj != null)
        {
            PartLocation loc = (PartLocation)obj;
            if (this.Order < loc.Order) return -1;
            if (this.Order > loc.Order) return 1;
        }
        return 0;
    }
}