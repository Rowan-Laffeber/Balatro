using Balatro2.Cards;
using System;
using System.Collections.Generic;

namespace Balatro2.Combinations
{
    internal class FourOfKind
    {
        // Returns true if the provided cards contain at least one 4OfKind.
        public static bool IsMatch(IEnumerable<Card> cards)
        {
            if (cards == null) return false;
            var groups = cards.GroupBy(c => c.Value).Select(g => new { Value = g.Key, Count = g.Count() });
            return groups.Any(g => g.Count >= 4);
        }
    }
}