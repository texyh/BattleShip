using BattleShip.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Core
{
    public interface IShip
    {
        int Size { get; }

        GridCordinate Position { get; }

        ShipDirection Direction { get; }

        int Hits { get; }

        bool IsSunk { get; }

        bool IsHit(GridCordinate positon);

        void SetShipPosition(GridCordinate position);

        void SetShipDirection(ShipDirection direction);

        void IncreaseHit();
    }
}
