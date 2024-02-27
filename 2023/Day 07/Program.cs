using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using Day_07;

var f = File.ReadLines("input.txt");

var total1 = 0;
var total2 = 0;

var hands = new List<Hand>();

foreach (var line in f)
{
    var hand = new Hand();

    var parts = line.Split(' ');
    hand.Bid = int.Parse(parts[1], CultureInfo.CurrentCulture);

    foreach (var card in parts[0])
    {
        hand.Cards1.Add(GetEnumValueFromDescription<Card1>(card.ToString()));
        hand.Cards2.Add(GetEnumValueFromDescription<Card2>(card.ToString()));
    }

    hands.Add(hand);
}

var rank = 1;

foreach (var hand in hands.OrderDescending(new HandComparer1()))
{
    // Console.WriteLine(string.Join(" ", hand.Cards1.Select(static c => c.ToString())) + $" Rank: {rank}. Score: {hand.Bid * rank}");

    total1 += hand.Bid * rank;
    rank++;
}

rank = 1;

foreach (var hand in hands.OrderDescending(new HandComparer2()))
{
    Console.WriteLine(string.Join(" ", hand.Cards2.Select(static c => c.ToString())) + $" Hand: {hand.HandType2}. Rank: {rank}. Score: {hand.Bid * rank}");

    total2 += hand.Bid * rank;
    rank++;
}

Console.WriteLine(total1);
Console.WriteLine(total2);

static T GetEnumValueFromDescription<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] T>(string description) where T : Enum
{
    var type = typeof(T);
    if (!type.IsEnum)
        throw new ArgumentException();

    var fields = type.GetFields();
    var field = fields
        .SelectMany(static f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
            static (f, a) => new {Field = f, Att = a})
        .Single(a => ((DescriptionAttribute) a.Att)
            .Description == description);

    var raw = (int) field.Field.GetRawConstantValue();

    return Unsafe.As<int, T>(ref raw);
}
