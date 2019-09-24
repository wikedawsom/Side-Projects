using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class BattleshipGame
    {
        private BattleshipBoard Player1;
        private BattleshipBoard Player2;
        private bool VsAI;

        public BattleshipGame(int humanPlayerCount)
        {
            Player1 = new BattleshipBoard();
            Player2 = new BattleshipBoard();
            if (humanPlayerCount == 1)
                VsAI = true;
            else
                VsAI = false;
        }

        public bool GetVsAI()
        {
            return VsAI;
        }
        
        public void AITurn()
        {

        }
    }
}
