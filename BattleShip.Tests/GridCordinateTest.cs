using BattleShip.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BattleShip.Tests
{
    public class GridCordinateTest
    {

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(-1, -0, true)]
        [InlineData(11, 0, true)]
        [InlineData(3, 0, false)]
        [InlineData(4, 11, true)]
        [InlineData(4, 8, false)]
        [InlineData(0, 1, false)]
        public void Can_Validate_Position(int horizontalAxis, int verticalAxis, bool expectedResult)
        {
            var position = new GridCordinate { HorizontalAxis = horizontalAxis, VeriticalAxis = verticalAxis };

            var isInvalid = position.IsInvalid();

            Assert.Equal(expectedResult, isInvalid);
        }
    }
}
