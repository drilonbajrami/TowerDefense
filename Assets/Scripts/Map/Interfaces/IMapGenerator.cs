namespace TowerDefense.Map
{
    /// <summary>
    /// Interface for map generator.
    /// </summary>
    public interface IMapGenerator
    {
        /// <summary>
        /// Generates a map of tiles.
        /// </summary>
        /// <param name="settings">Map settings.</param>
        ITile[,] GenerateMap(IMapSettings settings);
    }
}