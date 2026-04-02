using System;
using System.Collections.Generic;
using System.Linq;
using Balatro2.Cards;

namespace Balatro2.Combinations
{
    internal static class TwoPair
    {
        // Simple check: returns true if the provided cards contain exactly two distinct pairs.
        // Expects at least the selected cards (usually 4 cards for two-pair), but will work on any set.
        public static bool IsMatch(IEnumerable<Card> cards)
        {
            if (cards == null) return false;
            var groups = cards.GroupBy(c => c.Value).Select(g => new { Value = g.Key, Count = g.Count() }).ToList();
            // count how many distinct values appear at least twice
            int pairsOrBetter = groups.Count(g => g.Count >= 2);
            // true when there are at least two distinct values that each occur at least twice
            return pairsOrBetter >= 2;
        }
    }
}
