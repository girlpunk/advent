namespace Day_07;

public class HandComparer2 : Comparer<Hand>
{
    public override int Compare(Hand? one, Hand? two)
    {
        if (one == null)
            return 1;

        if (two == null)
            return -1;

        if (one.HandType2 < two.HandType2)
            return -1;

        if (two.HandType2 < one.HandType2)
            return 1;

        if (one.Cards2[0] > two.Cards2[0])
            return -1;
        if (two.Cards2[0] > one.Cards2[0])
            return 1;

        if (one.Cards2[1] > two.Cards2[1])
            return -1;
        if (two.Cards2[1] > one.Cards2[1])
            return 1;

        if (one.Cards2[2] > two.Cards2[2])
            return -1;
        if (two.Cards2[2] > one.Cards2[2])
            return 1;

        if (one.Cards2[3] > two.Cards2[3])
            return -1;
        if (two.Cards2[3] > one.Cards2[3])
            return 1;

        if (one.Cards2[4] > two.Cards2[4])
            return -1;
        if (two.Cards2[4] > one.Cards2[4])
            return 1;

        return 0;
    }
}