// See https://aka.ms/new-console-template for more information


Console.WriteLine("Advent of Code, Day 1A, Trebuchet");
var value = 0;
var input = File.OpenText("..\\..\\..\\input.txt");
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
    int num  =0;
    foreach (char chr in line)
    {
        if (Char.IsNumber(chr))
        {
            num = (int)Char.GetNumericValue(chr);
            break;
        }
    }
    return num;
}

int GetLastNumber(string line)
{
    int num = 0;
    var length = line.Length;
    for (int i = length - 1; i >= 0; i--)
    {
        var chr = line[i];
        if (Char.IsNumber(chr))
        {
            num = (int)Char.GetNumericValue(chr);
            break;
        }
    }
    return num;
}




