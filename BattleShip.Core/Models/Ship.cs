﻿using System;

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

        public GridCordinate GetInitialPosition()
        {
            return new GridCordinate 
            { 
              HorizontalAxis = Position.HorizontalAxis, 
              VeriticalAxis = Position.VeriticalAxis
            };
        }

        public void IncreaseHit()
        {
            Hits++;
        }

        public bool IsHit(GridCordinate missilePosition)
        {
            var shipPosition = GetInitialPosition();
            
            for (int i = 0; i < Size; i++)
            {
                if (missilePosition.HorizontalAxis == shipPosition.HorizontalAxis && 
                    missilePosition.VeriticalAxis == shipPosition.VeriticalAxis)
                {
                    return true;
                }

                shipPosition.MoveToNextPosition(Direction);
            }

            return false;
        }

        public void SetDirection(ShipDirection direction)
        {
            if (direction == ShipDirection.Unknown)
            {
                throw new ArgumentNullException("please specify ship direction");
            }

            Direction = direction;
        }

        public void SetPosition(GridCordinate position)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position cannot be null");
            }

            Position = position;
        }
    }
}
