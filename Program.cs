using System;
using Balatro2.Cards;

namespace Balatro2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create model containing deck and player hand
            Model model = new Model();
            model.Deck.Shuffle();
            Console.WriteLine("Deck generated and shuffled.");

            // Fill initial hand
            model.PlayerHand.FillFromDeck(model.Deck);

            // Create view model and show visualization. The loop will re-render the view on each iteration.
            ViewModel vm = new ViewModel(model);

            bool keepRunning = true;
            while (keepRunning)
            {
                // Update and render visualisation (clears console)
                vm.UpdateFromModel();
                vm.RenderUI();

                // Print menu below the visualization
                Console.WriteLine();
                Console.WriteLine("Options:");
                Console.WriteLine("D. Draw next card");
                Console.WriteLine("R. Reshuffle remaining cards");
                Console.WriteLine("H. Show player hand (visual, interactable)");
                Console.WriteLine("X. Discard hand and refill");
                Console.WriteLine("P. Print remaining deck order (dumps)");
                Console.WriteLine("Q. Quit");
                Console.Write("Enter D, R, H, X, P, or Q: ");
                string choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "d":
                        Card? drawn = model.Deck.TakeCard();
                        if (drawn == null)
                        {
                            Console.WriteLine("Deck is empty.");
                        }
                        else
                        {
                            Console.WriteLine("You drew:");
                            drawn.PrintMe();
                        model.PlayerHand.Add(drawn);
                        // update view model on change - the loop will re-render
                        }
                        break;
                    case "r":
                        model.Deck.Shuffle();
                        Console.WriteLine("Remaining cards reshuffled.");
                        break;
                    case "h":
                        // open a simple view loop where keys control selection/deletion
                        vm.Run();
                        break;
                    case "x":
                        model.PlayerHand.Clear();
                        Console.WriteLine("Hand discarded. Refilling...");
                        model.PlayerHand.FillFromDeck(model.Deck);
                        // update view model on change - loop will re-render
                        break;
                    case "p":
                        model.Deck.PrintDeck();
                        break;
                    case "q":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}