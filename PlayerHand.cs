using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balatro2.Cards;
using Balatro2.Combinations;

namespace Balatro2
{
    public class PlayerHand
    {
        private List<Card> cards = new List<Card>();
        private HashSet<int> selected = new HashSet<int>();
        public int Score { get; private set; } = 0;

        public void Add(Card card)
        {
            if (card == null) return;
            cards.Add(card);
        }

        public void Clear()
        {
            cards.Clear();
        }

        public int Count => cards.Count;

        public IEnumerable<Card> Cards => cards;

        public void PrintHand()
        {
            if (cards.Count == 0)
            {
                Console.WriteLine("Player hand is empty.");
                return;
            }

            Console.WriteLine("Player hand:");
            for (int i = 0; i < cards.Count; i++)
            {
                Console.Write(($"{i + 1}: "));
                cards[i].PrintMe();
            }
        }

        // Fill hand from deck until target count (default 8) or deck is empty
        public void FillFromDeck(Deck deck, int target = 8)
        {
            while (this.Count < target)
            {
                Card? c = deck.TakeCard();
                if (c == null) break; // no more cards
                this.Add(c);
            }
        }

        // For the ViewModel: expose cards and selected indices
        public IEnumerable<Card> CardsInHand => cards;
        public IEnumerable<int> SelectedCards => selected;
        public void SelectCard(int index)
        {
            if (index < 0 || index >= cards.Count)
            {
                return;
            }
            if (selected.Contains(index))
            {
                selected.Remove(index);
            }
            else
            {
                selected.Add(index);
            }
        }

        // Remove all selected cards from the hand. Returns the matched combination and points if removed.
        internal (Combination? combo, int points) RemoveSelected()
        {
            var indices = selected.Where(i => i >= 0 && i < cards.Count).OrderBy(i => i).ToList();
            var selectedCards = indices.Select(i => cards[i]).ToList();

            var eval = Evaluator.Evaluate(selectedCards);
            if (!eval.combo.HasValue)
                return (null, 0);

            // remove selected indices from hand (highest-first)
            foreach (var idx in indices.OrderByDescending(i => i))
                cards.RemoveAt(idx);

            selected.Clear();
            // award points to this hand/player
            this.Score += eval.points;
            return (eval.combo, eval.points);
        }
    }
}
