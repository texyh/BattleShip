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
            var game = GivenGame();

            Assert.Equal(OceanGrid.ROWS, game.OceanGrid.GetLength(0));
            Assert.Equal(OceanGrid.COLUMNS, game.OceanGrid.GetLength(1));
        }

        [Fact]
        public void OceanGrid_Should_Have_Two_Destroyers_And_One_BattleShip_When_Game_IsCreated()
        {
            var numberOfDestroyers = 2;
            var numberOfBatttleShips = 1;
            var expectedTotal = numberOfBatttleShips + numberOfDestroyers;
            var game = GivenGame();

            Assert.Equal(expectedTotal, game.ShipCount);
        }

        [Theory]
        [InlineData("A5")]
        [InlineData("A1")]
        [InlineData("J10")]

        public void Can_Attack_Using_One_Dimentional_Cordinate(string coordinate)
        {
            var game = GivenGame();

            var result = game.FireMissile(coordinate);

            Assert.IsType<ShotResultStatus>(result);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("0")]
        [InlineData(")1")]
        [InlineData(")(")]
        [InlineData("")]
        [InlineData("5a")]
        [InlineData("A11")]
        [InlineData("k1")]
        [InlineData("A0")]
        [InlineData("K0")]
        public void Should_Throw_Error_If_The_Attack_Coordinates_IsInvalid(string coordinate)
        {
            var game = GivenGame();

            var exception = Assert.Throws<ArgumentException>(() => game.FireMissile(coordinate));
            Assert.Equal("Invalid Coordinates", exception.Message);
        }

        [Fact]
        public void No_Ship_Should_Be_On_The_OceanGrid_After_AllShipSunked_Result()
        {
            var game = GivenGame();
            var expectedShipSpaces = 0;
            var totalNumberOfShipSpaces = 0;

            PlayGame(game);

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    if (game.OceanGrid[row, column].ContainsShip())
                    {
                        totalNumberOfShipSpaces++;
                    }
                }
            }

            Assert.Equal(expectedShipSpaces, totalNumberOfShipSpaces);
        }

        private void PlayGame(Game game)
        {
            var gameResult = ShotResultStatus.Misses;
            var coordinateIterator = GenerateCoordinate().GetEnumerator();

            while (gameResult != ShotResultStatus.SinkedAllShips && coordinateIterator.MoveNext())
            {
                gameResult = game.FireMissile(coordinateIterator.Current);
            }
        }

        private IEnumerable<string> GenerateCoordinate()
        {
            for (int row = 0; row < OceanGrid.ROWS; row++)
            {
                for (int column = 0; column < OceanGrid.COLUMNS; column++)
                {
                    var columnAlpha = (column).ConvertToAlpabet();

                    yield return $"{columnAlpha}{row + 1}";
                }
            }
        }

        private Game GivenGame()
        {
            return new Game();
        }
    }
}
