using System;

namespace Balatro2.Cards
{
    public class Card
    {
        public CardValue Value;
        public CardSymbol Symbol;

        public Card(CardValue startValue, CardSymbol startSymbol)
        {
            this.Value = startValue;
            this.Symbol = startSymbol;
        }

        public void PrintMe()
        {
            string display;
            // Use explicit comparisons because some enum members share the same numeric value
            if (Value == CardValue.J)
                display = "J";
            else if (Value == CardValue.Q)
                display = "Q";
            else if (Value == CardValue.K)
                display = "K";
            else if (Value == CardValue.A)
                display = "A";
            else
                display = ((int)Value).ToString();

            Console.WriteLine($"{display} of {Symbol}");
        }

        // Return a compact string for ViewModel display
        public string MakeAsString()
        {
            string display;
            if (Value == CardValue.J) display = "J";
            else if (Value == CardValue.Q) display = "Q";
            else if (Value == CardValue.K) display = "K";
            else if (Value == CardValue.A) display = "A";
            else display = ((int)Value).ToString();

            string suit;
            switch (Symbol)
            {
                case CardSymbol.harten: suit = "♥"; break;
                case CardSymbol.ruit: suit = "♦"; break;
                case CardSymbol.schoppen: suit = "♠"; break;
                case CardSymbol.klaver: suit = "♣"; break;
                default: suit = Symbol.ToString(); break;
            }

            return $"{display} {suit}";
        }
    }
}
