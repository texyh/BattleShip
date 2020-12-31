using BattleShip.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Models = BattleShip.Core.Models;

namespace BattleShip.Core
{
    public class Game
    {
        public readonly int[,] OceanGrid = new int[OceanGridConstants.ROWS, OceanGridConstants.COLUMNS];

        private IList<IShip> Ships = new List<IShip>();

        public Game()
        {
            AddBattleShip();
            AddTwoDestroyers();
        }

        public int ShipCount => Ships.Count;

        public ShotResultStatus FireMissile(string coordinate)
        {
            var position = ExtractGridCordinatesFromOneDimensionalCordinate(coordinate);

            if(OceanGrid[position.HorizontalAxis, position.VeriticalAxis].ContainsShip())
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

                if(Ships.All(x => x.IsSunk))
                {
                    return ShotResultStatus.SinkedAllShips;

                } else if (impactedShip != null && impactedShip.IsSunk)
                {
                    return ShotResultStatus.Sinks;

                } else 
                {
                    return ShotResultStatus.Hits;
                }
            }

            return ShotResultStatus.Misses;
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
            while (ShipHasOverlapped(ship))
            {
                SetShipPositionAndDirection(ship);
            }

            var horizontalAxis = ship.Position.HorizontalAxis;
            var verticalAxis = ship.Position.VeriticalAxis;

            for (int i = 0; i < ship.Size; i++)
            {
                OceanGrid[horizontalAxis, verticalAxis] = (int)GridPositionType.Ship;

                if (ship.Direction == ShipDirection.Vertical)
                {
                    horizontalAxis++;
                }
                else
                {
                    verticalAxis++;
                }
            }

            Ships.Add(ship);
        }

        private void SetShipPositionAndDirection(IShip ship)
        {
            var direction = GetRandomDirection();
            ship.SetShipDirection(direction);
            ship.SetShipPosition(GetRandomGridPosition(direction, ship.Size));
        }

        private GridCordinate GetRandomGridPosition(ShipDirection shipDirection, int shipSize)
        {
            if (shipDirection == ShipDirection.Vertical)
            {
                return new GridCordinate
                {
                    VeriticalAxis = new Random().Next(0, OceanGridConstants.COLUMNS),
                    HorizontalAxis = new Random().Next(0, OceanGridConstants.ROWS - shipSize)
                };

            } else {

                return new GridCordinate
                {
                    VeriticalAxis = new Random().Next(0, OceanGridConstants.COLUMNS - shipSize),
                    HorizontalAxis = new Random().Next(0, OceanGridConstants.ROWS)
                };
            }
        }

        private ShipDirection GetRandomDirection()
        {
            var randomDirection = new Random().Next(0, 2);

            return randomDirection == 0 ? ShipDirection.Vertical : ShipDirection.Horizontal;
        }

        private bool ShipHasOverlapped(IShip ship)
        {
            var horizontalAxis = ship.Position.HorizontalAxis;
            var verticalAxis = ship.Position.VeriticalAxis;

            for (int i = 0; i < ship.Size; i++)
            {
                if (OceanGrid[horizontalAxis, verticalAxis].ContainsShip())
                {
                    return true;
                }

                if (ship.Direction == ShipDirection.Vertical)
                {
                    horizontalAxis++;
                }
                else
                {
                    verticalAxis++;
                }
            }

            return false;
        }

        private GridCordinate ExtractGridCordinatesFromOneDimensionalCordinate(string coordinate)
        {
            if (string.IsNullOrEmpty(coordinate) || coordinate.Length < 2)
            {
                throw new ArgumentException("Invalid Coordinates");
            }

            var column = coordinate.Substring(0, 1);
            var row = coordinate.Substring(1, coordinate.Length - 1);
            var charColumn = column.ToCharArray()[0];

            if(!int.TryParse(row, out var horizontalAxis))
            {
                throw new ArgumentException("Invalid Coordinates");
            }

            if(!char.IsLetter(charColumn))
            {
                throw new ArgumentException("Invalid Coordinates");
            }

            var verticalAxis = char.ToUpper(charColumn) - 65;

            var gridCoordinate = new GridCordinate { HorizontalAxis = horizontalAxis, VeriticalAxis = verticalAxis };

            if(gridCoordinate.IsInvalid())
            {
                throw new ArgumentException("Invalid Coordinates");
            }

            return gridCoordinate;
        }
    }
}