using System.ComponentModel;

namespace Day_10;

public enum Pipe
{
    [Description("|")]
    Vertical,

    [Description("-")]
    Horizontal,

    [Description("L")]
    NorthEast,

    [Description("J")]
    NorthWest,

    [Description("7")]
    SouthWest,

    [Description("F")]
    SouthEast,

    [Description(".")]
    None,

    [Description("S")]
    Start,
}
