using System;
using System.Collections.Generic;
using System.Linq;
using Balatro2.Cards;

namespace Balatro2.Combinations
{
    internal static class FullHouse
    {
        // Returns true if the provided cards contain a full house (3 of one value and 2 of another)
        public static bool IsMatch(IEnumerable<Card> cards)
        {
            if (cards == null) return false;
            var counts = cards.GroupBy(c => c.Value).Select(g => g.Count()).OrderByDescending(c => c).ToList();
            return counts.Count >= 2 && counts[0] >= 3 && counts[1] >= 2;
        }
    }
}
