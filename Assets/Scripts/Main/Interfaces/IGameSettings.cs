using TowerDefense.Map;

namespace TowerDefense.Main
{
    /// <summary>
    /// Interface for Tower Defense game settings.
    /// </summary>
    public interface IGameSettings
    {
        /// <summary>
        /// Map settings.
        /// </summary>
        IMapSettings MapSettings { get; }

        /// <summary>
        /// Validates incorrect settings.
        /// </summary>
        void ValidateSettings();
    }
}