using Balatro2.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balatro2
{
    public class Model
    {
        public Deck Deck { get; }
        public PlayerHand PlayerHand { get; }

        public Model()
        {
            Deck = new Deck();
            PlayerHand = new PlayerHand();
        }
    }
}
