namespace Day_10;

public record Location
{
    public Location(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public Location(Location start, int x, int y)
    {
        X = start.X + x;
        Y = start.Y + y;
    }

    public int X { get; }
    public int Y { get; }

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    public override string ToString() => $"[{X}, {Y}]";
}
