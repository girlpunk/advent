using System.Globalization;
using Day_05;

var f = File.ReadLines("example.txt");

var total1 = new List<long>();

var seedsString = f
    .Take(2)
    .First()
    .Replace("seeds: ", "", StringComparison.OrdinalIgnoreCase);
var seeds = seedsString
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(long.Parse)
    .ToList();

var mapStrings = string
    .Join('\n', f.Skip(2))
    .Split("\n\n", StringSplitOptions.RemoveEmptyEntries);

var maps = mapStrings
    .Select(static mapString => mapString.Split('\n'))
    .Select(static values => values
        .Skip(1)
        .Select(static value => value.Split(' '))
        .Select(static parts => new MapValue(long.Parse(parts[1], CultureInfo.CurrentCulture), long.Parse(parts[0], CultureInfo.CurrentCulture), long.Parse(parts[2], CultureInfo.CurrentCulture)))
        .ToList())
    .ToList();

foreach (var seed in seeds)
{
    var s = seed;

    foreach (var map in maps)
    {
        foreach (var mapValue in map)
        {
            if (s >= mapValue.Source && s - mapValue.Source < mapValue.Length)
            {
                var offset = s - mapValue.Source;
                s = mapValue.Destination + offset;

                break;
            }
        }
    }

    total1.Add(s);
}

var part2Seeds = seeds
    .Select(static (x, i) => new {Index = i, Value = x})
    .GroupBy(static x => x.Index / 2)
    .Select(static x => new SeedPair(x.First().Value, x.Skip(1).First().Value))
    .ToList();

foreach (var map in maps)
{
    var nextSeeds = new List<SeedPair>();
    foreach (var seed in part2Seeds)
    {
        foreach (var mapValue in map.Where(m => seed.Start >= m.Source || seed.Start + seed.Length <= m.Source + m.Length))
        {
            if (seed.Start >= mapValue.Source && seed.Start + seed.Length <= mapValue.Source + mapValue.Length)
            {
                // Entirely within range
                var offset1 = seed.Start - mapValue.Source;
                seed.Start = mapValue.Destination + offset1;

                break;
            }

            if (seed.Start >= mapValue.Source)
            {
                // Need to split seed
                var offset1 = seed.Start - mapValue.Length;
                nextSeeds.Add(new SeedPair(seed.Start + offset1 + 1, seed.Length - (offset1 + 1)));
                seed.Length = offset1;

                var offset2 = seed.Start - mapValue.Source;
                seed.Start = mapValue.Destination + offset2;

                break;
            }

            // Split end
            var offset = seed.Start - mapValue.Length;
            var seed2 = new SeedPair(seed.Start + offset + 1, seed.Length - (offset + 1));
            seed.Length = offset;

            var offset3 = seed2.Start - mapValue.Source;
            seed2.Start = mapValue.Destination + offset3;

            nextSeeds.Add(seed2);

            break;
        }

        nextSeeds.Add(seed);
    }

    part2Seeds = nextSeeds;
}


Console.WriteLine(total1.Order().First());
Console.WriteLine(part2Seeds.OrderBy(static s => s.Start).First().Start);
