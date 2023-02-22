using TowerDefense.Towers;
using UnityEngine;

namespace TowerDefense.Map
{
    /// <summary>
    /// Tile.
    /// </summary>
    public class Tile : ITile
    {
        /// <inheritdoc/>
        public (int row, int col) Index { get; private set; }

        public Vector3 WorldPosition { get; private set; }

        /// <inheritdoc/>
        public Zone Zone { get; private set; }

        /// <inheritdoc/>
        public bool Occupied => Tower != null;

        public Tower Tower { get; private set; } = null;

        public Tile(int row, int col, Zone zone = Zone.Build)
        {
            Index = (row, col);
            Zone = zone;
        }

        /// <inheritdoc/>
        public void SetZone(Zone zone) => Zone = zone;

        /// <inheritdoc/>
        public void CacheWorldPosition(Vector3 position) => WorldPosition = position;

        public void SetTower(Tower tower) => Tower = tower;
    }
}