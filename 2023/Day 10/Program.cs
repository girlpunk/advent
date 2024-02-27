using Day_10;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

var f = File.ReadAllLines("input.txt");

var total1 = 0;
int total2;
var grid = new Piece[f.Length, f[0].Length];

var lineNumber = -1;

var start = new Location(-1, -1);

foreach (var line in f)
{
    lineNumber++;
    var charNumber = -1;

    foreach (var c in line)
    {
        charNumber++;

        var location = new Location(lineNumber, charNumber);
        grid[lineNumber, charNumber] = new Piece(GetEnumValueFromDescription<Pipe>(c.ToString()), location);

        if (grid[lineNumber, charNumber].Pipe == Pipe.Start)
            start = location;
    }
}

Console.WriteLine($"starting at {start}");

var currentLocation = FindNext(start, Direction.None, grid);

Console.WriteLine($"Step {total1}: coming from {currentLocation.direction} to {currentLocation.location}");

while (true)
{
    grid[currentLocation.location.X, currentLocation.location.Y].Status = Status.Loop;

    if (grid[currentLocation.location.X, currentLocation.location.Y].Pipe == Pipe.Start)
        break;

    total1++;
    currentLocation = FindNext(currentLocation.location, currentLocation.direction, grid);
    // Console.WriteLine($"Step {total1}: coming from {currentLocation.direction} to {currentLocation.location}");
}

total1++;
total1 /= 2;

var grid2 = new Piece[grid.GetLength(0) * 2 + 1, grid.GetLength(1) * 2 + 1];

for (var x = 0; x < grid2.GetLength(0); x++)
{
    for (var y = 0; y < grid2.GetLength(1); y++)
    {
        var location = new Location(x, y);
        grid2[x, y] = new Piece(Pipe.None, location) {Status = Status.Unknown};
    }
}

for (var x = 1; x < grid2.GetLength(0) - 1; x += 2)
{
    for (var y = 1; y < grid2.GetLength(1) - 1; y += 2)
    {
        var old = grid[(x - 1) / 2, (y - 1) / 2];
        if (old.Status == Status.Loop)
        {
            grid2[x, y].Pipe = old.Pipe;
            grid2[x, y].Status = Status.Loop;
        }
    }
}

for (var x = 2; x < grid2.GetLength(0) - 1; x += 2)
{
    for (var y = 1; y < grid2.GetLength(1) - 1; y += 2)
    {
        if (new[] {Pipe.Vertical, Pipe.SouthEast, Pipe.SouthWest}.Contains(grid2[x - 1, y].Pipe))
        {
            grid2[x, y].Pipe = Pipe.Vertical;
            grid2[x, y].Status = Status.Loop;
        }
        else if (grid2[x - 1, y].Pipe == Pipe.Start &&
                 new[] {Pipe.Vertical, Pipe.NorthEast, Pipe.NorthWest}.Contains(grid2[x + 1, y].Pipe))
        {
            grid2[x, y].Pipe = Pipe.Vertical;
            grid2[x, y].Status = Status.Loop;
        }
    }
}

for (var x = 1; x < grid2.GetLength(0) - 1; x += 2)
{
    for (var y = 2; y < grid2.GetLength(1) - 1; y += 2)
    {
        if (new[] {Pipe.Horizontal, Pipe.SouthEast, Pipe.NorthEast}.Contains(grid2[x, y - 1].Pipe))
        {
            grid2[x, y].Pipe = Pipe.Horizontal;
            grid2[x, y].Status = Status.Loop;
        }
        else if (grid2[x, y - 1].Pipe == Pipe.Start &&
                 new[] {Pipe.Horizontal, Pipe.SouthWest, Pipe.NorthWest}.Contains(grid2[x, y + 1].Pipe))
        {
            grid2[x, y].Pipe = Pipe.Horizontal;
            grid2[x, y].Status = Status.Loop;
        }
    }
}

while (true)
{
    var next = grid2
        .Cast<Piece>()
        .FirstOrDefault(static p => p.Status == Status.Unknown);

    if (next == null)
        break;

    FillRange(next.Location, grid2);
}

var grid3 = new Piece[grid.GetLength(0), grid.GetLength(1)];

for (var x = 1; x < grid2.GetLength(0) - 1; x += 2)
{
    for (var y = 1; y < grid2.GetLength(1) - 1; y += 2)
    {
        grid3[(x - 1) / 2, (y - 1) / 2] = grid2[x, y];
    }
}

total2 = grid3.Cast<Piece>().Count(static item => item.Status == Status.Inside);

// 590 - too high

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

    return (T) field.Field.GetRawConstantValue();
}

static bool IsInGrid(Location location, Piece[,] grid) => location is (>= 0, >= 0) && location.X < grid.GetLength(0) && location.Y < grid.GetLength(1);

static (Location location, Direction direction) FindNext(Location location, Direction fromDirection, Piece[,] grid)
{
    var toCheck = new Location(location, -1, 0);
    if (fromDirection != Direction.North &&
        IsInGrid(toCheck, grid) &&
        (new[] {Pipe.Vertical, Pipe.NorthEast, Pipe.NorthWest}.Contains(grid[location.X, location.Y].Pipe) || fromDirection == Direction.None) &&
        new[] {Pipe.Vertical, Pipe.SouthWest, Pipe.SouthEast, Pipe.Start}.Contains(grid[toCheck.X, toCheck.Y].Pipe))
        return (toCheck, Direction.South);

    toCheck = new Location(location, 1, 0);
    if (fromDirection != Direction.South &&
        IsInGrid(toCheck, grid) &&
        (new[] {Pipe.Vertical, Pipe.SouthWest, Pipe.SouthEast}.Contains(grid[location.X, location.Y].Pipe) || fromDirection == Direction.None) &&
        new[] {Pipe.Vertical, Pipe.NorthWest, Pipe.NorthEast, Pipe.Start}.Contains(grid[toCheck.X, toCheck.Y].Pipe))
        return (toCheck, Direction.North);

    toCheck = new Location(location, 0, -1);
    if (fromDirection != Direction.West &&
        IsInGrid(toCheck, grid) &&
        (new[] {Pipe.Horizontal, Pipe.NorthWest, Pipe.SouthWest}.Contains(grid[location.X, location.Y].Pipe) || fromDirection == Direction.None) &&
        new[] {Pipe.Horizontal, Pipe.NorthEast, Pipe.SouthEast, Pipe.Start}.Contains(grid[toCheck.X, toCheck.Y].Pipe))
        return (toCheck, Direction.East);

    toCheck = new Location(location, 0, 1);
    if (
        fromDirection != Direction.East &&
        IsInGrid(toCheck, grid) &&
        (new[] {Pipe.Horizontal, Pipe.NorthEast, Pipe.SouthEast}.Contains(grid[location.X, location.Y].Pipe) || fromDirection == Direction.None) &&
        new[] {Pipe.Horizontal, Pipe.NorthWest, Pipe.SouthWest, Pipe.Start}.Contains(grid[toCheck.X, toCheck.Y].Pipe))
        return (toCheck, Direction.West);

    throw new InvalidOperationException(
        $"Could not find next pipe from [{location.X}, {location.Y}] without going {fromDirection}");
}

static string GetEnumDescription<T>(T en) where T : Enum
{
    var type = en.GetType();
    var name = Enum.GetName(type, en);
    ArgumentNullException.ThrowIfNull(name);

    var field = type.GetField(name);
    ArgumentNullException.ThrowIfNull(field);

    var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
    if (attributes is {Length: > 0})
        return ((DescriptionAttribute) attributes[0]).Description;

    return en.ToString();
}

static bool CheckFill(Location location, Piece[,] grid)
{
    if (!IsInGrid(location, grid))
        return true;

    if (grid[location.X, location.Y].Status == Status.Loop ||
        grid[location.X, location.Y].Status == Status.Filling)
        return false;

    return true;
}

static bool FillRange(Location startLocation, Piece[,] grid)
{
    if (grid[startLocation.X, startLocation.Y].Status != Status.Unknown)
        return false;

    var queue = new Queue<Location>();
    var list = new List<Location>();

    queue.Enqueue(startLocation);
    list.Add(startLocation);

    var isOutside = false;

    while (queue.TryDequeue(out var current))
    {
        if (!IsInGrid(current, grid) ||
            grid[current.X, current.Y].Status == Status.Outside)
        {
            isOutside = true;
            continue;
        }

        grid[current.X, current.Y].Status = Status.Filling;
        list.Add(current);

        if (list.Count % 1000 == 0)
        {
            Console.WriteLine($"Found {list.Count:N0} in fill, {queue.Count:N0} in queue");
        }

        var next = new Location(current, -1, -1);
        if (!list.Contains(next) && !queue.Contains(next) && CheckFill(next, grid))
            queue.Enqueue(next);

        next = new Location(current, -1, 0);
        if (!list.Contains(next) && !queue.Contains(next) && CheckFill(next, grid))
            queue.Enqueue(next);

        next = new Location(current, -1, 1);
        if (!list.Contains(next) && !queue.Contains(next) && CheckFill(next, grid))
            queue.Enqueue(next);

        next = new Location(current, 0, -1);
        if (!list.Contains(next) && !queue.Contains(next) && CheckFill(next, grid))
            queue.Enqueue(next);

        next = new Location(current, 0, 1);
        if (!list.Contains(next) && !queue.Contains(next) && CheckFill(next, grid))
            queue.Enqueue(next);

        next = new Location(current, 1, -1);
        if (!list.Contains(next) && !queue.Contains(next) && CheckFill(next, grid))
            queue.Enqueue(next);

        next = new Location(current, 1, 0);
        if (!list.Contains(next) && !queue.Contains(next) && CheckFill(next, grid))
            queue.Enqueue(next);

        next = new Location(current, 1, 1);
        if (!list.Contains(next) && !queue.Contains(next) && CheckFill(next, grid))
            queue.Enqueue(next);
    }

    var status = isOutside
        ? Status.Outside
        : Status.Inside;

    foreach (var location in list)
    {
        grid[location.X, location.Y].Status = status;
    }

    return true;
}

static void LogGrid(Piece[,] grid)
{
    for (var x = 0; x < grid.GetLength(0); x++)
    {
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            var piece = grid[x, y];

            Console.Write(piece.Status switch
            {
                Status.Loop => GetEnumDescription(piece.Pipe),
                Status.Filling => 'F',
                Status.Inside => 'I',
                Status.Outside => 'O',
                Status.Unknown => '.',
                _ => '.',
            });
        }

        Console.WriteLine();
    }

    Console.WriteLine();
    Console.WriteLine();
}
