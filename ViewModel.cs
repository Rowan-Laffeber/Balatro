using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balatro2.Cards;

namespace Balatro2
{
    class ViewModel
    {
        //Directe reference naar de model, dit is de live model. Kan ook met api als het 2 losse programma's zijn.
        private Model Model;
        //Kopie van de data die nodig is voor visualisatie
        private int DeckCardsTotal = 0, DeckCardsRemaining = 0;
        private IEnumerable<Card> CardsInHand = new List<Card>();
        private IEnumerable<int> SelectedCards = new List<int>();
        private int highlightedIndex = 0;
        private bool keepViewRunning = true;
        private int lastAwardedPoints = 0;
        private string lastAwardedCombo = null;
        private int totalPoints = 0;

        public ViewModel(Model model)
        {
            this.Model = model;
        }

        //Method die de kopie update met nieuwe data uit de model
        public void UpdateFromModel()
        {
            this.DeckCardsTotal = this.Model.Deck.TotalCardCount;
            this.DeckCardsRemaining = this.Model.Deck.CardsRemainingCount;
            this.CardsInHand = this.Model.PlayerHand.CardsInHand;
            this.SelectedCards = this.Model.PlayerHand.SelectedCards;
            // clamp highlightedIndex
            if (this.CardsInHand.Count() == 0) this.highlightedIndex = 0;
            else if (this.highlightedIndex >= this.CardsInHand.Count()) this.highlightedIndex = this.CardsInHand.Count() - 1;
        }

        //Print nieuwe console view met gekopieerde data
        public void RenderUI()
        {
            Console.Clear();

            Console.WriteLine("Deck: "
                + this.DeckCardsRemaining.ToString()
                + "/"
                + this.DeckCardsTotal.ToString());

            for (int i = 0; i < this.CardsInHand.Count(); i++)
            {
                Card card = this.CardsInHand.ElementAt(i);

                // highlight pointer
                if (i == this.highlightedIndex)
                    Console.Write("> ");
                else
                    Console.Write("  ");

                // selection box
                if (this.SelectedCards.Contains(i))
                    Console.Write("[x] ");
                else
                    Console.Write("[ ] ");

                Console.WriteLine($"{i + 1}. {card.MakeAsString()}");
            }

            if (this.CardsInHand.Count() == 0)
            {
                Console.WriteLine("(hand is empty)");
            }

            // Display permanent score info
            Console.WriteLine();
            Console.WriteLine($"Last awarded: {lastAwardedCombo ?? "-"}  Points: {lastAwardedPoints}");
            Console.WriteLine($"Total points: {totalPoints}");
        }

        //user input
        public void HandleUserInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    if (this.highlightedIndex > 0) this.highlightedIndex--;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    if (this.highlightedIndex < this.CardsInHand.Count() - 1) this.highlightedIndex++;
                    break;
                // A-D and Space/Enter to select
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    // toggle selection for highlighted index
                    this.SelectCard(this.highlightedIndex);
                    break;
                case ConsoleKey.Delete:
                case ConsoleKey.X:
                    // delete selected and top off the hand from the deck
                    var res = this.Model.PlayerHand.RemoveSelected();
                    if (res.combo.HasValue)
                    {
                        int pts = res.points;
                        lastAwardedPoints = pts;
                        lastAwardedCombo = res.combo.Value.ToString();
                        totalPoints = this.Model.PlayerHand.Score;
                        // refill up to default hand size
                        this.Model.PlayerHand.FillFromDeck(this.Model.Deck);
                    }
                    else
                    {
                        lastAwardedPoints = 0;
                        lastAwardedCombo = null;
                    }

                    this.UpdateFromModel();
                    break;
                case ConsoleKey.Escape:
                case ConsoleKey.Q:
                    // exit view loop
                    this.keepViewRunning = false;
                    break;
            }
        }

        //Voer UI loop uit
        public void Run()
        {
            this.keepViewRunning = true;
            while (this.keepViewRunning)
            {
                this.UpdateFromModel();
                this.RenderUI();
                this.HandleUserInput();
            }
        }
        public void SelectCard(int index)
        {
            this.Model.PlayerHand.SelectCard(index);
            this.UpdateFromModel();
        }
    }
}
