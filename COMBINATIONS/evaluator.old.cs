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
            // Check combinations in priority order (highest -> lowest).
            if (RoyalFlush.IsMatch(cards))
                return (Combination.RoyalFlush, Points.GetPoints(Combination.RoyalFlush));

            if (StraightFlush.IsMatch(cards))
                return (Combination.StraightFlush, Points.GetPoints(Combination.StraightFlush));

            if (FourOfKind.IsMatch(cards))
                return (Combination.FourOfKind, Points.GetPoints(Combination.FourOfKind));

            if (FullHouse.IsMatch(cards))
                return (Combination.FullHouse, Points.GetPoints(Combination.FullHouse));

            if (Flush.IsMatch(cards))
                return (Combination.Flush, Points.GetPoints(Combination.Flush));

            if (Straight.IsMatch(cards))
                return (Combination.Straight, Points.GetPoints(Combination.Straight));

            if (ThreeOfKind.IsMatch(cards))
                return (Combination.ThreeOfKind, Points.GetPoints(Combination.ThreeOfKind));

            if (TwoPair.IsMatch(cards))
                return (Combination.TwoPair, Points.GetPoints(Combination.TwoPair));

            if (OnePair.IsMatch(cards))
                return (Combination.OnePair, Points.GetPoints(Combination.OnePair));

            // fallback to high card
            return (Combination.HighCard, Points.GetPoints(Combination.HighCard));
        }
    }
}
