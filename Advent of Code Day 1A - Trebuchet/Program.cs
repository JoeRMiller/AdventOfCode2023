// See https://aka.ms/new-console-template for more information


Console.WriteLine("Advent of Code, Day 1B, Trebuchet");
var value = 0;
var input = File.OpenText("..\\..\\..\\input.txt");

var numsDict = new Dictionary<string, int>()
{
    {"zero", 0},
    {"one", 1},
    {"two", 2},
    {"three", 3},
    {"four", 4},
    {"five", 5},
    {"six", 6},
    {"seven", 7},
    {"eight", 8},
    {"nine", 9},
};
    
                        
string? line;
while ((line = input.ReadLine()) != null)
{
    
    var first = GetFirstNumber(line);
    var last = GetLastNumber(line);
    value = value + (10 * first + last);
    
}
Console.WriteLine($"Calibration Value: {value}");

int GetFirstNumber(string line)
{
    int num = 0;
    int lowPosition = int.MaxValue;
    int position = -1;
    string found = "";
    //search for first number word in string
    foreach (string word in numsDict.Keys)
    {
        position = line.IndexOf(word);
        if ((position > -1) && (position < lowPosition)) {
            lowPosition = position;
            found = word;
        }
    }

    //Now we have first substring position, search for first number character
    var currentPos = 0;
    foreach (char chr in line)
    {
        if (Char.IsNumber(chr))
        {
            num = (int)Char.GetNumericValue(chr);
            break;
        }
        currentPos++;
    }
    if (lowPosition < currentPos)
    {
        num = numsDict[found];
    }
    return num;
}

int GetLastNumber(string line)
{
    int highPosition = -1;
    int position = -1;
    string found = "";
    //search for first number word in string
    foreach (string word in numsDict.Keys)
    {
        position = line.LastIndexOf(word);
        if ((position > -1) && (position > highPosition))
        {
            highPosition = position;
            found = word;
        }
    }

    int num = 0;
    var length = line.Length;
    var currentPos = length - 1;
    for (int i = length - 1; i >= 0; i--)
    {
        var chr = line[i];
        if (Char.IsNumber(chr))
        {
            num = (int)Char.GetNumericValue(chr);
            currentPos = i;
            break;
        }
    }
    if (highPosition > currentPos)
    {
        num = numsDict[found];
    }
    return num;
}




