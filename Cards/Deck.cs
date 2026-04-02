using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balatro2.Cards
{
    public class Deck
    {
        private List<Card> Cards;

        private List<Card> CardsTaken;
        private readonly int initialCount;
        public int TotalCardCount => initialCount;
        public int CardsRemainingCount => this.Cards?.Count ?? 0;
        public Deck()
        {
            this.Cards = new List<Card>();
            this.CardsTaken = new List<Card>();
            foreach (CardSymbol cardSymbol in Enum.GetValues(typeof(CardSymbol)))
            {
                foreach (CardValue cardValue in Enum.GetValues(typeof(CardValue)))
                {
                    Card card = new Card(cardValue, cardSymbol);
                    this.Cards.Add(card);
                    Console.WriteLine(card.Symbol.ToString() + " " + card.Value.ToString());
                }
            }
            this.initialCount = this.Cards.Count;
        }
        public void Shuffle()
        {
            // Use extension method to shuffle and reset taken pile
            if (this.Cards == null) return;
            this.Cards = this.Cards.Shuffle().ToList();
            this.CardsTaken = new List<Card>();
        }
        public Card? TakeCard()
        {
            if (this.Cards == null || this.Cards.Count == 0)
            {
                return null;
            }
            Card taken = this.Cards.First();
            this.Cards.RemoveAt(0);
            if (this.CardsTaken == null) this.CardsTaken = new List<Card>();
            this.CardsTaken.Add(taken);
            return taken;
        }
        public void Reset()
        {
            if (this.CardsTaken == null || this.CardsTaken.Count == 0) return;
            this.Cards.AddRange(this.CardsTaken);
            this.CardsTaken.Clear();
        }

        // Print the remaining deck order (top card is first)
        public void PrintDeck()
        {
            if (this.Cards == null || this.Cards.Count == 0)
            {
                Console.WriteLine("Deck is empty.");
                return;
            }

            Console.WriteLine("Deck (top -> bottom):");
            for (int i = 0; i < this.Cards.Count; i++)
            {
                Console.Write(($"{i + 1}: "));
                this.Cards[i].PrintMe();
            }
        }
    }
}
