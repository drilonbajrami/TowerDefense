using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Map
{
    /// <summary>
    /// The map view.
    /// </summary>
    public class MapView : MonoBehaviour, IMapView
    {
        /// <summary>
        /// Tile zone prefabs.
        /// </summary>
        [SerializeField, NonReorderable]
        private List<ZoneTilePrefab> TileZonePrefabs;

        /// <summary>
        /// Store the home/end tile position for enemy navmesh agents.
        /// </summary>
        [SerializeField] private Vector3Value HomeTilePosition;

        /// <summary>
        /// Store the spawn/start tile position for the enemy navmesh agents.
        /// </summary>
        [SerializeField] private Vector3Value SpawnTilePosition;

        [SerializeField] private FinishLine _finishLine;

        /// <summary>
        /// Path tiles (straight and turn tiles) prefab index and rotation.
        /// </summary>
        private readonly Dictionary<string, (float, int)> PathTiles = new()
        {
            {"1001", (0f, 1)}, {"1100", (90f, 1)}, {"0110", (180f, 1)}, {"0011", (-90f, 1)},
            {"1010", (0f, 0)}, {"0101", (90f, 0)}
        };

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SpawnMap(ITile[,] map, IMapSettings settings)
        {
            if (TryGetComponent(out MeshRenderer meshRenderer))
                DestroyImmediate(meshRenderer);

            if (TryGetComponent(out MeshFilter meshFilter))
                DestroyImmediate(meshFilter);

            if (TryGetComponent(out MeshCollider meshCollider))
                DestroyImmediate(meshCollider);

            float tileSize = settings.TileSize;
            int walkLayer = LayerMask.NameToLayer("Walkable");

            for (int row = 0; row < map.GetLength(0); row++)
                for (int col = 0; col < map.GetLength(1); col++) {
                    GameObject tile = map[row, col].Zone == Zone.Path ?
                                      InstantiatePathTile(map, (row, col), tileSize) :
                                      InstantiateTile(map[row, col], tileSize);
                    if (map[row, col].Zone != Zone.Build)
                        tile.layer = walkLayer;
                }

            GetComponent<NavMeshSurface>().BuildNavMesh();
            GetComponent<MeshCombiner>().CombineMeshes(false);

            if (_finishLine)
                _finishLine.SetSizeAndPosition(settings.TileSize, HomeTilePosition.runtimeValue);

            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
        }

        /// <summary>
        /// Instantiates a tile prefab based on its zone and sets its position and size.
        /// </summary>
        /// <param name="tile">The tile to spawn the view game object for.</param>
        /// <param name="size">Size of tile.</param>
        /// <returns>Tile game object.</returns>
        private GameObject InstantiateTile(ITile tile, float size)
        {
            GameObject tileInstance = Instantiate(TileZonePrefabs.Find(x => x.Zone == tile.Zone).Prefabs[0], transform);
            tileInstance.name = $"Tile [{tile.Index.row},{tile.Index.col}]";
            tileInstance.transform.position = new Vector3(size * (tile.Index.col + 0.5f), 0, size * (tile.Index.row + 0.5f));
            if (tile.Zone == Zone.Home) HomeTilePosition.runtimeValue = tileInstance.transform.position;
            if (tile.Zone == Zone.Spawn) SpawnTilePosition.runtimeValue = tileInstance.transform.position;
            tileInstance.transform.localScale = new Vector3(size, 1, size);
            tile.CacheWorldPosition(tileInstance.transform.position);
            return tileInstance;
        }

        /// <summary>
        /// Spawns a path tile (straight or turn tile) based on the neighbour tile types around the given tile index.
        /// </summary>
        /// <param name="map">The tile map, on which to check the neighbouring tiles.</param>
        /// <param name="index">The index of the current tile to spawn.</param>
        /// <param name="size">Size of tile.</param>
        /// <returns></returns>
        private GameObject InstantiatePathTile(ITile[,] map, (int row, int col) index, float size)
        {
            (float angle, int index) t = PathTiles[GetNeighborsForTile(map, index)];
            GameObject tileInstance = Instantiate(TileZonePrefabs.Find(x => x.Zone == Zone.Path).Prefabs[t.index], transform);
            tileInstance.name = $"Tile [{index.row},{index.col}]";
            tileInstance.transform.position = new Vector3(size * (index.col + 0.5f), 0, size * (index.row + 0.5f)); ;
            tileInstance.transform.localScale = new Vector3(size, 1, size);
            tileInstance.transform.rotation = Quaternion.Euler(0, t.angle, 0);
            map[index.row, index.col].CacheWorldPosition(tileInstance.transform.position);
            return tileInstance;
        }

        /// <summary>
        /// Returns a string of four characters '0' or '1' depending on type of the North, East, South and West neighbour tiles.
        /// </summary>
        private string GetNeighborsForTile(ITile[,] map, (int row, int col) index)
        {
            // For BUILD tiles we get 0
            // For PATH, HOME and SPAWN tiles we get 1
            string IsNotZero(Zone type) => type == Zone.Build ? "0" : "1";
            StringBuilder s = new();
            s.Append(IsNotZero(map[index.row + 1, index.col].Zone));
            s.Append(IsNotZero(map[index.row, index.col + 1].Zone));
            s.Append(IsNotZero(map[index.row - 1, index.col].Zone));
            s.Append(IsNotZero(map[index.row, index.col - 1].Zone));
            return s.ToString();
        }
    }
}