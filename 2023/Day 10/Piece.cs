namespace Day_10;

public class Piece(Pipe pipe, Location location)
{
    public Pipe Pipe { get; set; } = pipe;
    public Status Status { get; set; } = Status.Unknown;
    public Location Location { get; } = location;
}
