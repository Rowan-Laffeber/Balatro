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
            var detectors = new (Func<IEnumerable<Card>, bool> Detector, Combination Combo)[]
            {
                //(RoyalFlush.IsMatch, Combination.RoyalFlush),
                //(StraightFlush.IsMatch, Combination.StraightFlush),
                (FourOfKind.IsMatch, Combination.FourOfKind),
                (FullHouse.IsMatch, Combination.FullHouse),
                //(Flush.IsMatch, Combination.Flush),
                //(Straight.IsMatch, Combination.Straight),
                (ThreeOfKind.IsMatch, Combination.ThreeOfKind),
                (TwoPair.IsMatch, Combination.TwoPair),
                (OnePair.IsMatch, Combination.OnePair),
            };

            foreach (var (detector, combo) in detectors)
            {
                if (detector(cards))
                    return (combo, Points.GetPoints(combo));
            }

            // fallback to high card
            return (Combination.HighCard, Points.GetPoints(Combination.HighCard));
        }
    }
}
