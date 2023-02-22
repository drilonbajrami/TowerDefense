namespace TowerDefense.Map
{
    /// <summary>
    /// Interface for map view pointer handler.
    /// </summary>
    public interface IMapViewPointerHandler
    {
        /// <summary>
        /// Map of tiles.
        /// </summary>
        ITile[,] Map { get; }

        /// <summary>
        /// Map settings.
        /// </summary>
        IMapSettings Settings { get; }

        /// <summary>
        /// Set the map on which to handle the pointer on.
        /// </summary>
        /// <param name="map">Map of tiles.</param>
        /// <param name="settings">Map settings.</param>
        public void SetMap(ITile[,] map, IMapSettings settings);
    }
}