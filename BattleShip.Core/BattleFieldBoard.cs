using System;

namespace BattleShip.Core
{
    public class BattleFieldBoard
    {
        private const int GAMEROWS = 10;

        private const int GAMECOLUMS = 10;

        private const int EMPTYSPACE = 0;

        public int[,] OceanGrid = new int[GAMEROWS, GAMECOLUMS];

        public BattleFieldBoard()
        {
        }
    }
}