// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Advent of Code Day 3 - Gear Ratios");

var input = File.OpenText("..\\..\\..\\input.txt");

int ARRAYLENGTH = 140;


int lineNumber = 0;
Char[][] schematic = new Char[ARRAYLENGTH][];
bool[][] partMap = new bool[ARRAYLENGTH][];
bool[][] gearMap = new bool[ARRAYLENGTH][];
int ratioTotal = 0;

//Read input, create schematic and symbol map arrays
string? line;
while ((line = input.ReadLine()) != null)
{
    char[] mapLine = line.ToCharArray();
    schematic[lineNumber] = mapLine;
    partMap[lineNumber] = new bool[ARRAYLENGTH];
    gearMap[lineNumber] = new bool[ARRAYLENGTH];
    
    for (int i = 0;  i < mapLine.Length; i++)
    {
        //if ((!Char.IsDigit(mapLine[i]) && (mapLine[i] != '.')))
        if (MatchPart(mapLine[i]))
        {
            partMap[lineNumber][i] = true;
        }
        else
        {
            partMap[lineNumber][i] = false;
        }

        //if (mapLine[i] == '*')
        if (MatchGear(mapLine[i]))
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

SortedSet<PartLocation> partLocations = ProcessMapForParts(partMap, MatchType.Parts);
SortedSet<PartLocation> gearLocations = ProcessMapForParts(gearMap, MatchType.Gears);

int partsSum = 0;
foreach (PartLocation part in partLocations)
{
    //Console.WriteLine($"Found Part: {part}");
    partsSum += part.PartNumber;
}

Console.WriteLine($"Parts Sum: {partsSum}");
Console.WriteLine($"Gears Sum: {ratioTotal}");

bool MatchPart(char chr)
{
    bool result = false;

    if ((!Char.IsDigit(chr) && (chr != '.')))
    {
        result = true;
    }

    return result;
}

bool MatchGear(char chr)
{
    bool result = false;

    if (chr == '*')
    {
        result = true;
    }

    return result;
}

SortedSet<PartLocation> ProcessMapForParts(bool[][] map, MatchType matchType)
{
    SortedSet<PartLocation> locations = new();
    //read symbol map, and identify surrounding digits
    for (int row = 0; row < ARRAYLENGTH; row++)
    {
        for (int col = 0; col < ARRAYLENGTH; col++)
        {
            SortedSet<PartLocation> tempLocations = new();
            if (map[row][col] == true)
            {
                //found a symbol, check surrounding 9 locations
                //above row locations
                if (row > 0)
                {
                    AddLocation(FindParts(row - 1, col), ref tempLocations);
                    AddLocation(FindParts(row - 1, col + 1), ref tempLocations);
                    AddLocation(FindParts(row - 1, col + 2), ref tempLocations);
                }

                //same row locations
                AddLocation(FindParts(row, col), ref tempLocations);
                AddLocation(FindParts(row, col + 2), ref tempLocations);

                //lower row locations
                if (row < ARRAYLENGTH)
                {
                    AddLocation(FindParts(row + 1, col), ref tempLocations);
                    AddLocation(FindParts(row + 1, col + 1), ref tempLocations);
                    AddLocation(FindParts(row + 1, col + 2), ref tempLocations);
                }
                switch (matchType)
                {
                    case MatchType.Parts:
                        //add all locations found
                        foreach (PartLocation location in tempLocations)
                        {
                            locations.Add(location);
                        }
                        break;

                    case MatchType.Gears:
                        //only add if exactly two parts found
                        if (tempLocations.Count == 2)
                        {
                            int ratio = 1;
                            foreach (PartLocation location in tempLocations)
                            {
                                locations.Add(location);
                                ratio = ratio * location.PartNumber;
                            }
                            ratioTotal += ratio;
                        }
                        break;
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
            while (foundDigit && (currentCol < ARRAYLENGTH))
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
            return location;
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
    public int Ratio { get; }

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
        return (1000000 * Row) + (1000 * StartCol) + EndCol;
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

enum MatchType
{
    Parts,
    Gears
}