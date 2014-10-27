using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;
        private rules.INewWinStrategy m_winRule;


        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetHitRule();
            m_winRule = a_rulesFactory.GetWinRule();
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(m_deck, this, a_player);   
            }
            return false;
        }

        public bool Stand(Dealer a_dealer)
        {
            if (m_deck != null)
            {
                while (m_hitRule.DoHit(this))
                {
                    a_dealer.ShowHand();
                    GetShowAndDealCardToDealer(true, a_dealer);
                }
            }
            return false;
        }

        public void GetShowAndDealCardToPlayer(bool showIt, Player thePlayer)
        {
            BlackJack.controller.Observer pause = new BlackJack.controller.Observer();
            pause.PauseIt();

            Card c;
            c = m_deck.GetCard();
            c.Show(showIt);
            thePlayer.DealCard(c);
        }

        public void GetShowAndDealCardToDealer(bool showIt, Dealer theDealer)
        {
            BlackJack.controller.Observer pause = new BlackJack.controller.Observer();
            pause.PauseIt();

            Card c;
            c = m_deck.GetCard();
            c.Show(showIt);
            theDealer.DealCard(c);
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
                GetShowAndDealCardToPlayer(true, a_player);
                return true;
            }
            return false;
        }

        public bool IsDealerWinner(Player a_player)
        {
            bool dealerWon = m_winRule.Verdict();

            if (a_player.CalcScore() > g_maxScore)
            {
                return true;
            }

            else if (CalcScore() > g_maxScore)
            {
                return false;
            }

            else if (a_player.CalcScore() == CalcScore())
            {
                return dealerWon;
            }
            
            return CalcScore() >= a_player.CalcScore();
        }

        public bool IsGameOver()
        {
            if (m_deck != null && /* CalcScore() >= g_hitLimit */ m_hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }
    }
}
