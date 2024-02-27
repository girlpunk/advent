namespace Day_07;

public class HandComparer1 : Comparer<Hand>
{
    public override int Compare(Hand? one, Hand? two)
    {
        if (one == null)
            return 1;

        if (two == null)
            return -1;

        if (one.HandType1 < two.HandType1)
            return -1;

        if (two.HandType1 < one.HandType1)
            return 1;

        if (one.Cards1[0] > two.Cards1[0])
            return -1;
        if (two.Cards1[0] > one.Cards1[0])
            return 1;

        if (one.Cards1[1] > two.Cards1[1])
            return -1;
        if (two.Cards1[1] > one.Cards1[1])
            return 1;

        if (one.Cards1[2] > two.Cards1[2])
            return -1;
        if (two.Cards1[2] > one.Cards1[2])
            return 1;

        if (one.Cards1[3] > two.Cards1[3])
            return -1;
        if (two.Cards1[3] > one.Cards1[3])
            return 1;

        if (one.Cards1[4] > two.Cards1[4])
            return -1;
        if (two.Cards1[4] > one.Cards1[4])
            return 1;

        return 0;
    }
}