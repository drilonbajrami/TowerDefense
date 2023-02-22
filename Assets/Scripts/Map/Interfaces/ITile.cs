using TowerDefense.Towers;
using UnityEngine;

namespace TowerDefense.Map
{
    /// <summary>
    /// Interface for a tile object.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// Index of the tile in a 2D map grid.
        /// </summary>
        (int row, int col) Index { get; }

        /// <summary>
        /// Tile's world position.
        /// </summary>
        Vector3 WorldPosition { get; }

        /// <summary>
        /// Tile zone type.
        /// </summary>
        Zone Zone { get; }

        /// <summary>
        /// Whether the tile has a tower built on it or not.
        /// </summary>
        bool Occupied { get; }

        /// <summary>
        /// The tower that is built on this tile.
        /// </summary>
        Tower Tower { get; }

        /// <summary>
        /// Sets the zone type of the tile.
        /// </summary>
        void SetZone(Zone zone);

        /// <summary>
        /// Caches the position of the game object that represents this tile.
        /// </summary>
        void CacheWorldPosition(Vector3 position);

        /// <summary>
        /// Caches the tower built on this tile.
        /// </summary>
        void SetTower(Tower tower);
    }
}