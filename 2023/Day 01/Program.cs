using System.Globalization;
using System.Text.RegularExpressions;

var f = File.ReadLines("input.txt");

var total1 = 0;
var total2 = 0;

foreach (var line in f)
{
    var part1Match = Regex.Match(line, @"^(.*?)(?<a>\d)(.*)(?<b>\d)(.*?)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(1));
    if (part1Match.Success)
    {
        var part1String = part1Match.Groups["a"].Value + part1Match.Groups["b"].Value;
        total1 += int.Parse(part1String, CultureInfo.CurrentCulture);
    }
    else
    {
        part1Match = Regex.Match(line, @"^(.*?)(?<a>\d)(.*)", RegexOptions.Compiled | RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(1));
        var part1String = part1Match.Groups["a"].Value + part1Match.Groups["a"].Value;
        total1 += int.Parse(part1String, CultureInfo.CurrentCulture);
    }

    var part2Match = Regex.Match(line,
        @"^(.*?)(?<a>\d|one|two|three|four|five|six|seven|eight|nine)(.*)(?<b>\d|one|two|three|four|five|six|seven|eight|nine)(.*?)$",
        RegexOptions.Compiled | RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(1)
    );

    if (part2Match.Success)
    {
        var part2String = (part2Match.Groups["a"].Value + part2Match.Groups["b"].Value)
            .Replace("one", "1")
            .Replace("two", "2")
            .Replace("three", "3")
            .Replace("four", "4")
            .Replace("five", "5")
            .Replace("six", "6")
            .Replace("seven", "7")
            .Replace("eight", "8")
            .Replace("nine", "9");
        total2 += int.Parse(part2String, CultureInfo.CurrentCulture);
    }
    else
    {
        part2Match = Regex.Match(line,
            @"^(.*?)(?<a>\d|one|two|three|four|five|six|seven|eight|nine)(.*)$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(1)
        );
        var part2String = (part2Match.Groups["a"].Value + part2Match.Groups["a"].Value)
            .Replace("one", "1")
            .Replace("two", "2")
            .Replace("three", "3")
            .Replace("four", "4")
            .Replace("five", "5")
            .Replace("six", "6")
            .Replace("seven", "7")
            .Replace("eight", "8")
            .Replace("nine", "9");
        total2 += int.Parse(part2String, CultureInfo.CurrentCulture);
    }
}

Console.WriteLine(total1);
Console.WriteLine(total2);
