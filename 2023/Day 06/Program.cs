using System.Globalization;

var f = File.ReadLines("input.txt");

var total1 = 0;
var total2 = 0;

var times = f.Take(1).Single().Replace("Time:", "").Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
var distances = f.Skip(1).Take(1).Single().Replace("Distance: ", "").Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

for (var raceNumber = 0; raceNumber < times.Count; raceNumber++)
{
    Console.WriteLine($"Race {raceNumber}. Time: {times[raceNumber]}ms. Distance: {distances[raceNumber]}mm.");
    var ways = 0;

    for (var holdTime = 0; holdTime <= times[raceNumber]; holdTime++)
    {
        var distance = holdTime * (times[raceNumber] - holdTime);
        if (distance > distances[raceNumber])
            ways++;
    }

    if (total1 == 0)
        total1 = ways;
    else
        total1 *= ways;
}

var times2 = long.Parse(f.First().Replace("Time:", "").Replace(" ", ""), CultureInfo.CurrentCulture);
var distances2 = long.Parse(f.Skip(1).First().Replace("Distance:", "").Replace(" ", ""), CultureInfo.CurrentCulture);

for (long holdTime = 0; holdTime <= times2; holdTime++)
{
    var distance = holdTime * (times2 - holdTime);
    if (distance > distances2)
        total2++;
}

Console.WriteLine(total1);
Console.WriteLine(total2);
