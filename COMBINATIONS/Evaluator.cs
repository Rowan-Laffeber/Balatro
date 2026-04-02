using System;
using System.Collections.Generic;
using System.Linq;
using Balatro2.Cards;

namespace Balatro2.Combinations
{
    internal static class Evaluator
    {
        // Evaluate a set of cards against known combinations (highest to lowest).
        // Returns a tuple of (matchedCombination?, points)
        public static (Combination? combo, int points) Evaluate(IEnumerable<Card> cards)
        {
            if (cards == null) return (null, 0);

            // Check in priority order (highest to lowest).
            if (TwoPair.IsMatch(cards))
            {
                return (Combination.TwoPair, Points.GetPoints(Combination.TwoPair));
            }

            // OnePair next (detect any value group with count >= 2)
            var groups = cards.GroupBy(c => c.Value).Select(g => g.Count()).ToList();
            if (groups.Any(cnt => cnt >= 2))
            {
                return (Combination.OnePair, Points.GetPoints(Combination.OnePair));
            }

            // HighCard fallback
            if (HighCard.IsMatch(cards))
            {
                return (Combination.HighCard, Points.GetPoints(Combination.HighCard));
            }

            return (null, 0);
        }
    }
}
