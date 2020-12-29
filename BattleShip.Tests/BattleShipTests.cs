using BattleShip.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BattleShip.Tests
{
    public class BattleShipTests
    {
        [Fact]
        public void OceanGrid_Should_Be_Initialized_When_Game_IsCreated()
        {
            var game = new BattleFieldBoard();

            Assert.Equal(10, game.OceanGrid.GetLength(0));
            Assert.Equal(10, game.OceanGrid.GetLength(1));
        }

        [Fact]
        public void OceanGroid_Should_Have_One_BattleShip_When_Game_IsCreated()
        {
            var game = new BattleFieldBoard();
            var expectedNumberOfBattleShipSquares = 5;
            var totalNumberOfBattleShip = 0;

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    if(game.OceanGrid[row, column] == 2)
                    {
                        totalNumberOfBattleShip++;
                    }
                }
            }

            Assert.Equal(expectedNumberOfBattleShipSquares, totalNumberOfBattleShip);
        }
    }
}
