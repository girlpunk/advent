using System.Globalization;
using System.Text.RegularExpressions;

var f = File.ReadLines("input.txt");

var total1 = 0;

var cards = new Dictionary<int, int>();

foreach (var line in f)
{
    var parsed = Regex.Match(line, @"^Card *(?<a>\d*): (?<numbers>.*)", RegexOptions.Compiled | RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(1));
    var card = int.Parse(parsed.Groups["a"].Value, CultureInfo.CurrentCulture);
    if (cards.ContainsKey(card))
        cards[card]++;
    else
        cards[card] = 1;

    var numbers = parsed.Groups["numbers"].Value.Split('|');
    var winning = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
    var random = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

    var points = 0;

    foreach (var unused in random.Where(pull => winning.Contains(pull)))
    {
        if (points == 0)
            points = 1;
        else
            points *= 2;
    }

    var wins = random.Count(pull => winning.Contains(pull));
    for (var i = 1; i <= wins; i++)
    {
        if (cards.ContainsKey(card + i))
            cards[card + i] += cards[card];
        else
            cards[card + i] = cards[card];
    }

    total1 += points;
}

var total2 = cards.Values.Sum();

Console.WriteLine(total1);
Console.WriteLine(total2);
