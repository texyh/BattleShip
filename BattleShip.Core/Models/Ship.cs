using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Core.Models
{
    public abstract class Ship : IShip
    {
        public Ship(int size)
        {
            Size = size;
        }

        public int Size { get; private set; }

        public GridCordinate Position { get; private set; }

        public ShipDirection Direction { get; private set; }

        public int Hits { get; private set; }

        public bool IsSunk => Hits == Size;

        public void IncreaseHit()
        {
            Hits++;
        }

        public bool IsHit(GridCordinate positon)
        {
            var horizontalAxis = Position.HorizontalAxis;
            var verticalAxis = Position.VeriticalAxis;

            for (int i = 0; i < Size; i++)
            {
                if (positon.HorizontalAxis == horizontalAxis && positon.VeriticalAxis == verticalAxis)
                {
                    return true;
                }

                if (Direction == ShipDirection.Horizontal)
                {
                    verticalAxis++;
                }
                else
                {
                    horizontalAxis++;
                }
            }

            return false;
        }

        public void SetShipDirection(ShipDirection direction)
        {
            if (direction == ShipDirection.Unknown)
            {
                throw new ArgumentNullException("please specify ship direction");
            }

            Direction = direction;
        }

        public void SetShipPosition(GridCordinate position)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position cannot be null");
            }

            Position = position;
        }
    }
}
