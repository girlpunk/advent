using System.Text.RegularExpressions;

var f = File.ReadLines("input.txt");

var steps = f.Take(1).Single();
var nodes = f
    .Skip(2)
    .Select(static l =>
    {
        var matches = Regex.Match(l, @"(?'this'\w+) = \((?'left'\w+), (?'right'\w+)\)", RegexOptions.Compiled | RegexOptions.ExplicitCapture,
            TimeSpan.FromSeconds(1));

        return (thisNode: matches.Groups["this"].Value, left: matches.Groups["left"].Value, right: matches.Groups["right"].Value);
    })
    .ToDictionary(
        static g => g.thisNode,
        static g => (g.left, g.right)
    );


// Part 1
var total1 = CalculateSteps("AAA", static n => n == "ZZZ");

// Part 2
var total2 = nodes
    .Where(static n => n.Key.EndsWith('A'))
    .Select(static n => n.Key)
    .Select(startNode => CalculateSteps(startNode, static endNode => endNode.EndsWith('Z')))
    .Select(Convert.ToInt64)
    .Aggregate(LCM);

Console.WriteLine(total1);
Console.WriteLine(total2);

int CalculateSteps(string thisNode, Func<string, bool> completionCondition)
{
    var stepIndex = 0;
    var stepCount = 0;

    while (!completionCondition(thisNode))
    {
        thisNode = steps[stepIndex] == 'L'
            ? nodes[thisNode].left
            : nodes[thisNode].right;

        stepIndex++;
        stepCount++;

        if (stepIndex == steps.Length)
            stepIndex = 0;
    }

    return stepCount;
}

static long GCF(long a, long b)
{
    while (b != 0)
    {
        var temp = b;
        b = a % b;
        a = temp;
    }

    return a;
}

static long LCM(long a, long b) => (a / GCF(a, b)) * b;
