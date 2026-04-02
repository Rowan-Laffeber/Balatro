using System.Collections.Generic;

namespace Balatro2.Combinations
{
    // Simple static points per combination. Tweak values as needed.
    internal static class Points
    {
        private static Dictionary<Combination, int> basePoints = new Dictionary<Combination, int>
        {
            { Combination.HighCard, 10 },
            { Combination.OnePair, 100 },
            { Combination.TwoPair, 200 },
            { Combination.ThreeOfKind, 300 },
            { Combination.Straight, 400 },
            { Combination.Flush, 500 },
            { Combination.FullHouse, 600 },
            { Combination.FourOfKind, 700 },
            { Combination.StraightFlush, 800 },
            { Combination.RoyalFlush, 900 }
        };

        public static int GetPoints(Combination combo)
        {
            return basePoints.TryGetValue(combo, out var v) ? v : 0;
        }
    }
}
