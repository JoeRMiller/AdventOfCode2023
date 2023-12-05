// See https://aka.ms/new-console-template for more information

using Day_5___Seed_Maps;

Console.WriteLine("Advent of Code Day 5 - Seed Maps");

using var input = File.OpenText("..\\..\\..\\input.txt");
//using var input = File.OpenText("..\\..\\..\\sample.txt");

List<long> seeds;
List<string> soilLines = [];
List<string> fertilizerLines = [];
List<string> waterLines = [];
List<string> lightLines = [];
List<string> temperatureLines = [];
List<string> humidityLines = [];
List<string> locationLines = [];

List<string> file = [];
string fileLine = "";
while ((fileLine = input.ReadLine()) != null)
{
    file.Add(fileLine);
}

int lineCount = file.Count;
string[] seedsLine = file[0].Split(':', StringSplitOptions.TrimEntries);

seeds = Helpers.StringToLongList(seedsLine[1]);
int i = 2;
while (i < lineCount)
{
    i = Helpers.ReadSection(file, i, "seed-to-soil map:", soilLines);
    i = Helpers.ReadSection(file, i, "soil-to-fertilizer map:", fertilizerLines);
    i = Helpers.ReadSection(file, i, "fertilizer-to-water map:", waterLines);
    i = Helpers.ReadSection(file, i, "water-to-light map:", lightLines);
    i = Helpers.ReadSection(file, i, "light-to-temperature map:", temperatureLines);
    i = Helpers.ReadSection(file, i, "temperature-to-humidity map:", humidityLines);
    i = Helpers.ReadSection(file, i, "humidity-to-location map:", locationLines);
}

SectionMap soilMap = Helpers.MapSection(soilLines);
SectionMap fertilizerMap = Helpers.MapSection(fertilizerLines);
SectionMap waterMap = Helpers.MapSection(waterLines);
SectionMap lightMap = Helpers.MapSection(lightLines);
SectionMap temperatureMap = Helpers.MapSection(temperatureLines);
SectionMap humidityMap = Helpers.MapSection(humidityLines);
SectionMap locationMap = Helpers.MapSection(locationLines);

long lowest = long.MaxValue;

foreach (long seed in seeds)
{
    long soilLocation = Helpers.GetMapLocation(seed, soilMap);
    long fertilizerLocation = Helpers.GetMapLocation(soilLocation, fertilizerMap);
    long waterLocation = Helpers.GetMapLocation(fertilizerLocation, waterMap);
    long lightLocation = Helpers.GetMapLocation(waterLocation, lightMap);
    long temperatureLocation = Helpers.GetMapLocation(lightLocation, temperatureMap);
    long humidityLocation = Helpers.GetMapLocation(temperatureLocation, humidityMap);
    long location = Helpers.GetMapLocation(humidityLocation, locationMap);
    
    if (location < lowest)
    {
        lowest = location;
    }
}

Console.WriteLine($"Lowest Seed location: {lowest}");
