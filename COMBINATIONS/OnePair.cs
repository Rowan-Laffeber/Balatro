using System;
using System.Collections.Generic;
using Balatro2.Cards;

namespace Balatro2.Combinations
{
    /// <summary>
    /// OnePair detector.
    /// Returns true when the supplied set of cards contains at least one value that appears two or more times.
    /// This check ignores suits (only card values matter).
    /// </summary>
    internal static class OnePair
    {
        public static bool IsMatch(IEnumerable<Card> cards)
        {
            if (cards == null) return false;

            // Simple frequency map: value -> count
            var counts = new Dictionary<CardValue, int>();
            foreach (var card in cards)
            {
                if (counts.ContainsKey(card.Value))
                    counts[card.Value]++;
                else
                    counts[card.Value] = 1;
            }

            // If any value appears at least twice, it's a pair
            foreach (var kv in counts)
            {
                if (kv.Value >= 2)
                    return true;
            }

            return false;
        }
    }
}
