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

            // Build simple frequency map of card values
            var counts = new Dictionary<CardValue, int>();
            foreach (var card in cards)
            {
                if (counts.ContainsKey(card.Value)) counts[card.Value]++;
                else counts[card.Value] = 1;
            }

            // Count how many distinct values appear exactly twice
            int pairCount = 0;
            foreach (var kv in counts)
            {
                if (kv.Value == 2) pairCount++;
            }

            return pairCount == 2;
        }
    }
}
