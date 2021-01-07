using System;
using BattleShip.Core.Models;

namespace BattleShip.Core
{
    public static class PositionHelper
    {
        public static GridCordinate GetRandomGridPosition(ShipDirection shipDirection, int shipSize)
        {
            if (shipDirection == ShipDirection.Vertical)
            {
                return new GridCordinate
                {
                    VeriticalAxis = new Random().Next(0, Core.OceanGrid.COLUMNS),
                    HorizontalAxis = new Random().Next(0, Core.OceanGrid.ROWS - shipSize)
                };
            }
            else
            {
                return new GridCordinate
                {
                    VeriticalAxis = new Random().Next(0, Core.OceanGrid.COLUMNS - shipSize),
                    HorizontalAxis = new Random().Next(0, Core.OceanGrid.ROWS)
                };
            }
        }
    }
}