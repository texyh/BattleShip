using System;
using BattleShip.Core.Models;

namespace BattleShip.Core
{
    public static class PositionHelper
    {
        private static Random RandomGenerator = new Random();
        public static GridCordinate GetRandomGridPosition(ShipDirection shipDirection, int shipSize)
        {
            if(shipDirection == ShipDirection.Unknown) 
            {
                throw new ArgumentException("Invalid ship direction, please specify ship direction");
            }

            if (shipDirection == ShipDirection.Vertical)
            {
                return CreateCordinate(RandomGenerator.Next(0, Core.OceanGrid.COLUMNS),
                                       RandomGenerator.Next(0, Core.OceanGrid.ROWS - shipSize));
           
            }

            return CreateCordinate(RandomGenerator.Next(0, Core.OceanGrid.COLUMNS - shipSize),
                                    RandomGenerator.Next(0, Core.OceanGrid.ROWS));
        }

        private static GridCordinate CreateCordinate(int veriticalAxis, int horizontalAxis) 
        {
            return new GridCordinate 
            {
                VeriticalAxis = veriticalAxis,
                HorizontalAxis = horizontalAxis
            };
        }
    }
}