using BattleShip.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Models = BattleShip.Core.Models;

namespace BattleShip.Core
{
    public class Game
    {
        public readonly int[,] OceanGrid = new int[Core.OceanGrid.ROWS, Core.OceanGrid.COLUMNS];
        private IList<IShip> Ships = new List<IShip>();
        public int ShipCount => Ships.Count;

        public Game()
        {
            AddBattleShip();
            AddTwoDestroyers();
        }

        public ShotStatus FireMissile(string coordinate)
        {
            var position = coordinate.ToGridCordinate();

            if (CordinateContainsShip(position))
            {
                return AttackPosition(position);
            }

            return ShotStatus.Misses;
        }

        private void AddTwoDestroyers()
        {
            var destroyerCount = 2;

            for (int i = 0; i < destroyerCount; i++)
            {
                var ship = new Destroyer();
                SetShipPositionAndDirection(ship);
                AddShipToOceanGrid(ship);
            }
        }

        private void AddBattleShip()
        {
            var ship = new Models.BattleShip();
            SetShipPositionAndDirection(ship);
            AddShipToOceanGrid(ship);
        }

        private void AddShipToOceanGrid(IShip ship)
        {
            while (ShipIsNotWithinTheOceanGrid(ship))
            {
                SetShipPositionAndDirection(ship);
            }

            var shipPosition = ship.GetInitialPosition();

            for (int i = 0; i < ship.Size; i++)
            {
                OceanGrid[shipPosition.HorizontalAxis, shipPosition.VeriticalAxis] = (int)GridPositionType.Ship;
                shipPosition.MoveToNextPosition(ship.Direction);
            }

            Ships.Add(ship);
        }

        private ShotStatus AttackPosition(GridCordinate position) 
        {
            IShip impactedShip = null;
            var floatingShips = Ships.Where(x => !x.IsSunk);

            foreach (var ship in floatingShips)
            {
                if (ship.IsHit(position))
                {
                    ship.IncreaseHit();
                    impactedShip = ship;
                    OceanGrid[position.HorizontalAxis, position.VeriticalAxis] = (int)GridPositionType.EmptySpace;
                    break;
                }
            }

            return ReportAttackStatus(impactedShip);
        }

        private ShotStatus ReportAttackStatus(IShip impactedShip) 
        {
            if (Ships.All(x => x.IsSunk))
            {
                return ShotStatus.SinkedAllShips;
            }
            else if (impactedShip != null && impactedShip.IsSunk)
            {
                return ShotStatus.Sinks;
            }
            else
            {
                return ShotStatus.Hits;
            }
        }

        private void SetShipPositionAndDirection(IShip ship)
        {
            var direction = ShipDirectionHelper.GetRandomDirection();
            ship.SetDirection(direction);
            ship.SetPosition(PositionHelper.GetRandomGridPosition(direction, ship.Size));
        }

        private bool ShipIsNotWithinTheOceanGrid(IShip ship)
        {
            var shipPosition = ship.GetInitialPosition();
            
            for (int i = 0; i < ship.Size; i++)
            {
                if (CordinateContainsShip(shipPosition))
                {
                    return true;
                }

                shipPosition.MoveToNextPosition(ship.Direction);
            }

            return false;
        }

        private bool CordinateContainsShip(GridCordinate position)
        {
            return OceanGrid[position.HorizontalAxis, position.VeriticalAxis].ContainsShip();
        }

    }
}
