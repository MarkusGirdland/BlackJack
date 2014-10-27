using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class Observer
    {
        public void PauseIt()
        {
            int pauseTime = 1500;
            System.Threading.Thread.Sleep(pauseTime);
        }
    }
}
