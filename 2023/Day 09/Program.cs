var f = File.ReadLines("input.txt");

var steps = f.Select(static metric => metric.Split(" ").Select(int.Parse).ToList()).ToList();

var total2 = 0;

// Part 1
var total1 = steps.Sum(CalculateNext);

// Part 2
total2 = steps.Sum(static m =>
{
    m.Reverse();
    return CalculateNext(m);
});

Console.WriteLine(total1);
Console.WriteLine(total2);

static int CalculateNext(List<int> values)
{
    var differences = new List<List<int>> {values};

    do
    {
        differences.Add(CalculateNextDifference(differences.Last()));
    } while (differences.Last().Any(static value => value != 0));

    differences.Reverse();

    for (var i = 1; i < differences.Count; i++)
    {
        var change = differences[i].Last() + differences[i - 1].Last();

        differences[i].Add(change);
    }

    return differences.Last().Last();
}

static List<int> CalculateNextDifference(IReadOnlyList<int> values)
{
    var newValues = new List<int>();

    for (var i = 0; i < values.Count - 1; i++)
    {
        newValues.Add(values[i + 1] - values[i]);
    }

    return newValues;
}
