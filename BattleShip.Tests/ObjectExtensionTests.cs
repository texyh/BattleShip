using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BattleShip.Core;

namespace BattleShip.Tests
{
    public class ObjectExtensionTests
    {
        [Theory]
        [InlineData(0, "A")]
        [InlineData(1, "B")]
        public void ConvertToAlphabet_Returns_The_Corresponding_Letter_Of_Number(int number, string expectedResult)
        {
            var result = number.ConvertToAlpabet();

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(10, false)]

        public void Contains_Returns_True_If_Space_IsShip(int space, bool expectResult)
        {
            var result = space.ContainsShip();

            Assert.Equal(expectResult, result);
        }


    }
}
