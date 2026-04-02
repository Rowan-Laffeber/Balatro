using System;
using System.Collections.Generic;
using System.Linq;
using Balatro2.Cards;

namespace Balatro2.Combinations
{
    internal static class OnePair
    {
        // Returns true if the provided cards contain at least one pair.
        public static bool IsMatch(IEnumerable<Card> cards)
        {
            if (cards == null) return false;
            var groups = cards.GroupBy(c => c.Value).Select(g => new { Value = g.Key, Count = g.Count() });
            return groups.Any(g => g.Count >= 2);
        }
    }
}
