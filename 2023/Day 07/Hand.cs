namespace Day_07;

public class Hand
{
    public IList<Card1> Cards1 { get; } = new List<Card1>();
    public IList<Card2> Cards2 { get; } = new List<Card2>();

    public HandType HandType1
    {
        get
        {
            var cardGroups = Cards1
                .GroupBy(static c => c)
                .OrderByDescending(static g => g.Count())
                .ToList();

            if (cardGroups[0].Count() == 5)
                return HandType.FiveOfAKind;

            if (cardGroups[0].Count() == 4)
                return HandType.FourOfAKind;

            if (cardGroups[0].Count() == 3 && cardGroups[1].Count() == 2)
                return HandType.FullHouse;

            if (cardGroups[0].Count() == 3)
                return HandType.ThreeOfAKind;

            if (cardGroups[0].Count() == 2 && cardGroups[1].Count() == 2)
                return HandType.TwoPair;

            if (cardGroups[0].Count() == 2)
                return HandType.OnePair;

            return HandType.HighCard;
        }
    }

    public HandType HandType2
    {
        get
        {
            var cardGroups = Cards2
                .GroupBy(static c => c)
                .OrderByDescending(static g => g.Count())
                .ToList();

            if (cardGroups.Others(0) == 5 ||
                (cardGroups.Others(0) == 4 && cardGroups.Jokers() >= 1) ||
                (cardGroups.Others(0) == 3 && cardGroups.Jokers() >= 2) ||
                (cardGroups.Others(0) == 2 && cardGroups.Jokers() >= 3) ||
                (cardGroups.Others(0) == 1 && cardGroups.Jokers() >= 4) ||
                (cardGroups.Jokers() >= 5))
                return HandType.FiveOfAKind;

            if (cardGroups.Others(0) == 4 ||
                (cardGroups.Others(0) == 3 && cardGroups.Jokers() >= 1) ||
                (cardGroups.Others(0) == 2 && cardGroups.Jokers() >= 2) ||
                (cardGroups.Others(0) == 1 && cardGroups.Jokers() >= 3) ||
                (cardGroups.Jokers() >= 4))
                return HandType.FourOfAKind;

            if ((cardGroups.Others(0) == 3 && cardGroups.Others(1) == 2) ||
                (cardGroups.Others(0) == 2 && cardGroups.Others(1) == 2 && cardGroups.Jokers() >= 1) ||
                (cardGroups.Others(0) == 2 && cardGroups.Others(1) == 1 && cardGroups.Jokers() >= 2) ||
                (cardGroups.Others(0) == 2 && cardGroups.Jokers() >= 3) ||
                (cardGroups.Others(0) == 1 && cardGroups.Others(1) == 1 && cardGroups.Jokers() >= 3) ||
                (cardGroups.Others(0) == 1 && cardGroups.Jokers() >= 4))
                return HandType.FullHouse;

            if (cardGroups.Others(0) == 3 ||
                (cardGroups.Others(0) == 2 && cardGroups.Jokers() >= 1) ||
                (cardGroups.Others(0) == 1 && cardGroups.Jokers() >= 2) ||
                cardGroups.Jokers() >= 3)
                return HandType.ThreeOfAKind;

            if ((cardGroups.Others(0) == 2 && cardGroups.Others(1) == 2) ||
                (cardGroups.Others(0) == 2 && cardGroups.Others(1) == 1 && cardGroups.Jokers() >= 1) ||
                (cardGroups.Others(0) == 1 && cardGroups.Others(1) == 1 && cardGroups.Jokers() >= 2) ||
                (cardGroups.Others(0) == 1 && cardGroups.Jokers() >= 3)
               )
                return HandType.TwoPair;

            if (cardGroups.Others(0) == 2 ||
                (cardGroups.Others(0) == 1 && cardGroups.Jokers() >= 1))
                return HandType.OnePair;

            return HandType.HighCard;
        }
    }

    public int Bid { get; set; }
}

public static class HandHelpers
{
    public static int Others(this IEnumerable<IGrouping<Card2, Card2>> groups, int count)
    {
        return groups
            .Where(static g => g.Key != Card2.Joker)
            .OrderByDescending(static g => g.Count())
            .Skip(count)
            .FirstOrDefault()?
            .Count() ?? 0;
    }

    public static int Jokers(this IEnumerable<IGrouping<Card2, Card2>> groups)
    {
        return groups
            .Where(static g => g.Key == Card2.Joker)
            .SingleOrDefault()?
            .Count() ?? 0;
    }
}
