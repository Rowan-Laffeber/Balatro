using System;
using System.Collections.Generic;
using System.Linq;
using Balatro2.Cards;

namespace Balatro2.Combinations
{
    internal static class HighCard
    {
        // HighCard always matches (fallback). We keep it explicit for evaluator ordering.
        public static bool IsMatch(IEnumerable<Card> cards)
        {
            return true;
        }
    }
}
