using TowerDefense.Map;
using UnityEngine;

namespace TowerDefense.Main
{
    /// <summary>
    /// Tower Defense game settings.
    /// </summary>
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Tower Defense/Game Settings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        public IMapSettings MapSettings => mapSettings;
        [SerializeField] private MapSettings mapSettings;

        /// <summary>
        /// Corrects any values that are invalid for the game settings.
        /// </summary>
        private void OnValidate() => ValidateSettings();

        public void ValidateSettings()
        {
            MapSettings.ValidateSettings();
            // Validate Other Data
        }
    }
}
