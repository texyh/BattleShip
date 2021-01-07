using System;
using System.Collections.Generic;
using System.Text;
using BattleShip.Core.Models;

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

        public static GridCordinate ToGridCordinate(this string coordinate) 
        {
            if (string.IsNullOrEmpty(coordinate) || coordinate.Length < 2)
            {
                throw new ArgumentException("Invalid Coordinates");
            }

            var column = coordinate.Substring(0, 1);
            var row = coordinate.Substring(1, coordinate.Length - 1);
            var charColumn = column.ToCharArray()[0];

            if (!int.TryParse(row, out var horizontalAxis))
            {
                throw new ArgumentException("Invalid Coordinates");
            }

            if (!char.IsLetter(charColumn))
            {
                throw new ArgumentException("Invalid Coordinates");
            }

            var verticalAxis = char.ToUpper(charColumn) - 65;

            var gridCoordinate = new GridCordinate { HorizontalAxis = horizontalAxis - 1, VeriticalAxis = verticalAxis };

            if (gridCoordinate.IsInvalid())
            {
                throw new ArgumentException("Invalid Coordinates");
            }

            return gridCoordinate;
        }
    }
}
