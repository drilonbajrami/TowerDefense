using TowerDefense.Map;

namespace TowerDefense.Main
{
    /// <summary>
    /// Interface for Tower Defense game.
    /// </summary>
    public interface ITowerDefenseGame
    {
        /// <summary>
        /// Map of tiles.
        /// </summary>
        ITile[,] Map { get; }

        /// <summary>
        /// Game settings.
        /// </summary>
        IGameSettings Settings { get; }

        /// <summary>
        /// Map view.
        /// </summary>
        IMapView MapView { get; }

        /// <summary>
        /// Map generator.
        /// </summary>
        IMapGenerator MapGenerator { get; }

        /// <summary>
        /// Map view pointer handler.
        /// </summary>
        public IMapViewPointerHandler MapViewPointerHandler { get; }

        /// <summary>
        /// Starts the game.
        /// </summary>
        void StartGame();
    }
}