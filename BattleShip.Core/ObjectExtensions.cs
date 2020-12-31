using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Core
{
    public static class ObjectExtensions
    {
        public static bool ContainsShip(this int space)
        {
            return space == (int)GridPositionType.Ship;
        }

        public static string ConvertToAlpabet(this int number)
        {
            char c = (char)(65 + (number));
            return c.ToString();
        }
    }
}
