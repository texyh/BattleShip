using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Core.Models
{
    public class GridCordinate : IEquatable<GridCordinate>
    {
        public int VeriticalAxis { get; set; }

        public int HorizontalAxis { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as GridCordinate);
        }

        public bool Equals(GridCordinate other)
        {
            return other != null &&
                   VeriticalAxis == other.VeriticalAxis &&
                   HorizontalAxis == other.HorizontalAxis;
        }

        public bool IsInvalid() => (VeriticalAxis < 0 || VeriticalAxis >= OceanGridConstants.COLUMNS) ||
                        (HorizontalAxis < 0 || HorizontalAxis >= OceanGridConstants.ROWS);
    }
}
