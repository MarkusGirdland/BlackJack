using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class Soft17
    {
        public bool DoHit(model.Player a_dealer)
        {
            int hitLimit = 17;

            foreach (Card c in a_dealer.GetHand())
            {
                if (c.GetValue() == Card.Value.Ace)
                {
                    hitLimit = 18;
                }
            }

            return a_dealer.CalcScore() < hitLimit;
        }
    }
}
