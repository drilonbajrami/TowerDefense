using TowerDefense.EnemyWaves;
using TowerDefense.Map;
using UnityEngine;

namespace TowerDefense.Main
{
    /// <summary>
    /// Tower Defense game.
    /// </summary>
    public class TowerDefenseGame : MonoBehaviour, ITowerDefenseGame
    {
        public ITile[,] Map { get; private set; }
        public IGameSettings Settings => _settings;
        [SerializeField] private GameSettings _settings;
        public IMapView MapView { get; private set; }
        public IMapGenerator MapGenerator { get; private set; }
        public IMapViewPointerHandler MapViewPointerHandler { get; private set; }

        public WaveManager WaveManager;

        public void StartGame()
        {
            MapGenerator = GetComponentInChildren<IMapGenerator>();
            MapView = GetComponentInChildren<IMapView>();
            MapViewPointerHandler = GetComponentInChildren<IMapViewPointerHandler>();

            if (MapGenerator != null)
                Map = MapGenerator.GenerateMap(Settings.MapSettings);
            else {
                Debug.LogWarning("Could not find component of type IMapGenerator. Please add the missing component " +
                                 "on a child game object first and try again.");
                return;
            }


            if (MapView != null)
                MapView.SpawnMap(Map, Settings.MapSettings);
            else {
                Debug.LogWarning("Could not find component of type IMapView. Please add the missing component " +
                                 "on a child game object first and try again.");
                return;
            }

            if (MapViewPointerHandler != null)
                MapViewPointerHandler.SetMap(Map, Settings.MapSettings);
            else {
                Debug.LogWarning("Could not find component of type IMapViewPointerHandler. Please add the missing component " +
                                 "on a child game object first and try again.");
                return;
            }

            WaveManager.SetUp();
        }
    }
}
