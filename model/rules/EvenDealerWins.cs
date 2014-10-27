using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class EvenDealerWins : INewWinStrategy
    {
        public bool Verdict()
        {
            return true;
        }
    }
}
