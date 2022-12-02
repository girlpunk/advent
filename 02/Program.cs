using System.IO;

var groups = new List<int>();

var input = (await File.OpenText("input2.txt").ReadToEndAsync()).Split('\n').ToList();

var one = input.Select(game =>
{
    var parts = game.Split(' ');
    return parts[1] switch
    {
        // Rock
        "X" => 1 + parts[0] switch
        {
            // Rock
            "A" => 3,
            // Paper
            "B" => 0,
            // Sciscors
            "C" => 6,
            _ => throw new InvalidOperationException(),
        },
        // Paper
        "Y" => 2 + parts[0] switch
        {
            // Rock
            "A" => 6,
            // Paper
            "B" => 3,
            // Sciscors
            "C" => 0,
            _ => throw new InvalidOperationException(),
        },
        "Z" => 3 + parts[0] switch
        {
            // Rock
            "A" => 0,
            // Paper
            "B" => 6,
            // Sciscors
            "C" => 3,
            _ => throw new InvalidOperationException(),
        },
        _ => throw new InvalidOperationException(),
    };
})
    .Sum();

Console.WriteLine(one);

var two = input.Select(game =>
{
    var parts = game.Split(' ');
    return parts[0] switch
    {
        // Rock
        "A" => parts[1] switch
        {
            // Win
            "Z" => 6 + 2,
            // Draw
            "Y" => 3 + 1,
            // Loose
            "X" => 0 + 3,
            _ => throw new InvalidOperationException(),
        },
        // Paper
        "B" =>  parts[1] switch
        {
            // Win
            "Z" => 6 + 3,
            // Draw
            "Y" => 3 + 2,
            // Loose
            "X" => 0 + 1,
            _ => throw new InvalidOperationException(),
        },
        // Sciscors
        "C" => parts[1] switch
        {
            // Win
            "Z" => 6 + 1,
            // Draw
            "Y" => 3 + 3,
            // Loose
            "X" => 0 + 2,
            _ => throw new InvalidOperationException(),
        },
        _ => throw new InvalidOperationException(),
    };
})
    .Sum();

Console.WriteLine(two);
