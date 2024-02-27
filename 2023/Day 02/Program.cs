using System.Globalization;
using System.Text.RegularExpressions;

var f = File.ReadLines("input.txt");

var total1 = 0;
var total2 = 0;

foreach (var line in f)
{
    var red = 0;
    var green = 0;
    var blue = 0;

    var parsed = Regex.Match(line, @"^Game (?<a>\d*): (?<pulls>.*)", RegexOptions.Compiled | RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(1));
    var game = int.Parse(parsed.Groups["a"].Value, CultureInfo.CurrentCulture);

    var pulls = parsed.Groups["pulls"].Value.Split("; ");

    foreach (var pull in pulls)
    {
        var colours = pull.Split(", ");
        foreach (var colour in colours)
        {
            var parts = colour.Split(" ");
            var count = int.Parse(parts[0], CultureInfo.CurrentCulture);

            switch (parts[1])
            {
                case "red":
                {
                    red = Math.Max(red, count);
                    break;
                }
                case "green":
                {
                    green = Math.Max(green, count);
                    break;
                }
                case "blue":
                {
                    blue = Math.Max(blue, count);
                    break;
                }
                default:
                    throw new InvalidOperationException(parts[1]);
            }
        }
    }

    if (red <= 12 && green <= 13 && blue <= 14)
    {
        total1 += game;
    }

    var power = red * green * blue;
    total2 += power;
}

Console.WriteLine(total1);
Console.WriteLine(total2);
