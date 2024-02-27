using System.Globalization;
using System.Text;

var f = File.ReadAllLines("input.txt");

var total1 = 0;
var total2 = 0;
var grid = new char?[f.Length, f[0].Length];

var lineNumber = -1;

foreach (var line in f)
{
    lineNumber++;
    var charNumber = -1;

    foreach (var c in line)
    {
        charNumber++;

        if (c == '.')
            grid[lineNumber, charNumber] = null;
        else
            grid[lineNumber, charNumber] = c;
    }
}

for (var x = 0; x < f.Length; x++)
{
    for (var y = 0; y < f[0].Length; y++)
    {
        if (grid[x, y] == null || char.IsNumber(grid[x, y].Value))
            continue;

        var checkTop = true;
        var checkedY = new List<int>();
        var found = new List<int>();

        // Top-Left
        if (IsInGrid(x - 1, y - 1) && grid[x - 1, y - 1] != null)
        {
            checkTop = false;

            var part = FindWholeNumber(x - 1, y - 1);
            total1 += part.value;
            checkedY.AddRange(part.checkedY);
            found.Add(part.value);
        }

        // Top-Right
        if (IsInGrid(x - 1, y + 1) && grid[x - 1, y + 1] != null && !checkedY.Contains(y + 1))
        {
            checkTop = false;

            var part = FindWholeNumber(x - 1, y + 1);
            total1 += part.value;
            found.Add(part.value);
        }

        // Top
        if (checkTop && IsInGrid(x - 1, y) && grid[x - 1, y] != null)
        {
            var part = FindWholeNumber(x - 1, y);
            total1 += part.value;
            found.Add(part.value);
        }

        var checkBottom = true;
        checkedY = new List<int>();

        // Bottom-Left
        if (IsInGrid(x + 1, y - 1) && grid[x + 1, y - 1] != null)
        {
            checkBottom = false;

            var part = FindWholeNumber(x + 1, y - 1);
            total1 += part.value;
            found.Add(part.value);
            checkedY.AddRange(part.checkedY);
        }

        // Bottom-Right
        if (IsInGrid(x + 1, y + 1) && grid[x + 1, y + 1] != null && !checkedY.Contains(y + 1))
        {
            checkBottom = false;

            var part = FindWholeNumber(x + 1, y + 1);
            total1 += part.value;
            found.Add(part.value);
        }

        // Bottom
        if (checkBottom && IsInGrid(x + 1, y) && grid[x + 1, y] != null)
        {
            var part = FindWholeNumber(x + 1, y);
            total1 += part.value;
            found.Add(part.value);
        }

        // Left
        if (IsInGrid(x, y - 1) && grid[x, y - 1] != null)
        {
            var part = FindWholeNumber(x, y - 1);
            total1 += part.value;
            found.Add(part.value);
        }

        // Bottom-Right
        if (IsInGrid(x, y + 1) && grid[x, y + 1] != null)
        {
            var part = FindWholeNumber(x, y + 1);
            total1 += part.value;
            found.Add(part.value);
        }

        // Part 2
        if (grid[x, y] == '*' && found.Count == 2)
        {
            total2 += found[0] * found[1];
        }
    }
}

Console.WriteLine(total1);
Console.WriteLine(total2);

(int value, List<int> checkedY) FindWholeNumber(int x, int y)
{
    ArgumentNullException.ThrowIfNull(grid);

    while (IsInGrid(x, y - 1) && grid[x, y - 1] != null && char.IsNumber(grid[x, y - 1].Value))
        y--;

    var number = new StringBuilder(grid[x, y].Value.ToString());
    var checkedY = new List<int> {y};

    while (IsInGrid(x, y + 1) && grid[x, y + 1] != null && char.IsNumber(grid[x, y + 1].Value))
    {
        y++;
        number.Append(grid[x, y].Value.ToString());
        checkedY.Add(y);
    }

    return (int.Parse(number.ToString(), CultureInfo.CurrentCulture), checkedY);
}

bool IsInGrid(int x, int y) => x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1);
