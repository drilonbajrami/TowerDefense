namespace TowerDefense.Map
{
    /// <summary>
    /// Interface for map view.
    /// </summary>
    public interface IMapView
    {
        /// <summary>
        /// Spawns the view model of the map based on the given map settings.
        /// </summary>
        void SpawnMap(ITile[,] map, IMapSettings settings);
    }
}