using System.ComponentModel;

namespace Day_07;

public enum Card2
{
    [Description("A")]
    Ace = 14,

    [Description("K")]
    King = 13,

    [Description("Q")]
    Queen = 12,

    [Description("T")]
    Ten = 10,

    [Description("9")]
    Nine = 9,

    [Description("8")]
    Eight = 8,

    [Description("7")]
    Seven = 7,

    [Description("6")]
    Six = 6,

    [Description("5")]
    Five = 5,

    [Description("4")]
    Four = 4,

    [Description("3")]
    Three = 3,

    [Description("2")]
    Two = 2,

    [Description("J")]
    Joker = 1,
}