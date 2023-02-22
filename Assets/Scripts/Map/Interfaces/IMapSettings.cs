namespace TowerDefense.Map
{
    /// <summary>
    /// Interface for map settings.
    /// </summary>
    public interface IMapSettings
    {
        /// <summary>
        /// Map's width in tiles.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Map's height in tiles.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Map's tile size.
        /// </summary>
        float TileSize { get; }

        /// <summary>
        /// Validates any incorrectly set dimensions.
        /// </summary>
        void ValidateSettings();
    }
}