using System;

namespace BattleShip.Core
{
    public static class ShipDirectionHelper
    {
        public static ShipDirection GetRandomDirection()
        {
            var randomDirection = new Random().Next(0, 2);

            return randomDirection == 0 ? ShipDirection.Vertical : ShipDirection.Horizontal;
        }
    }
}