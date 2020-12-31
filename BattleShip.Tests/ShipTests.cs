using BattleShip.Core;
using BattleShip.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Models = BattleShip.Core.Models;

namespace BattleShip.Tests
{
    public class ShipTests
    {

        [Fact]
        public void BattleShip_Should_Have_A_Size_Five_When_Created()
        {
            var ship = new Models.BattleShip();

            Assert.Equal(ShipSize.BATTLESHIP, ship.Size);
        }

        [Fact]
        public void Destroyer_Should_Have_A_Size_Four_When_Created()
        {
            var ship = new Destroyer();

            Assert.Equal(ShipSize.DESTROYER, ship.Size);
        }

        [Fact]
        public void Ship_Should_Have_Zero_Hits_When_Created()
        {
            var ship = GivenAShip();

            Assert.Equal(0, ship.Hits);
        }

        [Fact]
        public void Ship_Should_Be_Afloat_When_Created()
        {
            var ship = GivenAShip();

            Assert.False(ship.IsSunk);
        }

        [Fact]
        public void Ships_Hits_Should_Increase_By_One_After_Increase_isCalled()
        {
            var ship = GivenAShip();
            ship.IncreaseHit();

            Assert.Equal(1, ship.Hits);
        }

        [Fact]
        public void Ship_Should_Sink_When_Hits_Equals_Its_Size()
        {
            var ship = GivenAShip();

            var i = 0;
            while (i < ship.Size)
            {
                ship.IncreaseHit();
                i++;
            }

            Assert.True(ship.IsSunk);
        }
        
        [Fact]
        public void Ship_Should_Throw_Error_When_Invalid_Direction_Is_Set()
        {
            var ship = GivenAShip();

            var exception = Assert.Throws<ArgumentNullException>(() => ship.SetDirection(ShipDirection.Unknown));
            Assert.Equal("please specify ship direction", exception.ParamName);
        }

        [Fact]
        public void Ship_Should_Set_Direction_After_SetDirection_IsCalled()
        {
            var ship = GivenAShip();
            var direction = ShipDirection.Vertical;

            ship.SetDirection(direction);

            Assert.Equal(direction, ship.Direction);
        }

        [Fact]
        public void Ship_Should_Throw_Error_When_Invalid_Position_Is_Set()
        {
            var ship = GivenAShip();

            var exception = Assert.Throws<ArgumentNullException>(() => ship.SetPosition(null));
            Assert.Equal("position cannot be null", exception.ParamName);
        }

        [Fact]
        public void Ship_Should_Set_Position_After_SetPosition_IsCalled()
        {
            var ship = GivenAShip();
            var position = new GridCordinate { HorizontalAxis = 0, VeriticalAxis = 0 };

            ship.SetPosition(position);

            Assert.Equal(position, ship.Position);
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(1, 0, true)]
        [InlineData(2, 0, true)]
        [InlineData(3, 0, true)]
        [InlineData(4, 0, false)]
        [InlineData(4, 8, false)]
        [InlineData(0, 1, false)]
        public void Ship_Should_Say_When_Its_Hit(int horizontalAxis, int verticalAxis, bool expectedResult)
        {
            var ship = new Destroyer();
            ship.SetDirection(ShipDirection.Vertical);
            var shipPosition = new GridCordinate { HorizontalAxis = 0, VeriticalAxis = 0 };
            ship.SetPosition(shipPosition);
            var attackPosition = new GridCordinate { HorizontalAxis = horizontalAxis, VeriticalAxis = verticalAxis };

            var ishit = ship.IsHit(attackPosition);

            Assert.Equal(expectedResult, ishit);
        }

        private IShip GivenAShip()
        {
            var size = new Random().Next(ShipSize.DESTROYER, ShipSize.BATTLESHIP);

            if(size == ShipSize.BATTLESHIP)
            {
                return new Models.BattleShip();
            }

            return new Destroyer();
        }
    }
}
