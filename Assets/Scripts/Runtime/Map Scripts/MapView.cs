using UnityEngine;

namespace TowerDefense.Runtime
{
    public class MapView : MonoBehaviour
    {
        public Tile _tilePrefab;

        [SerializeField]
        private MapSettings _mapSettings;

        Tile[,] _mapGrid;

        public void SpawnMap(int[,] map)
        {
            if (_mapGrid == null) {
                _mapGrid = new Tile[map.GetLength(0), map.GetLength(1)];
                for (int row = 0; row < map.GetLength(0); row++)
                    for (int col = 0; col < map.GetLength(1); col++) {
                        _mapGrid[row, col] = Instantiate(_tilePrefab, transform);
                        _mapGrid[row, col].gameObject.transform.position = new Vector3(col + 0.5f, 0, row + 0.5f);
                    }
            }
            else {
                if (_mapGrid.GetLength(0) != map.GetLength(0) || _mapGrid.GetLength(1) != map.GetLength(1)) {
                    for (int row = 0; row < _mapGrid.GetLength(0); row++)
                        for (int col = 0; col < _mapGrid.GetLength(1); col++)
                            Destroy(_mapGrid[row, col].gameObject);

                    _mapGrid = new Tile[map.GetLength(0), map.GetLength(1)];
                    for (int row = 0; row < map.GetLength(0); row++)
                        for (int col = 0; col < map.GetLength(1); col++) {
                            _mapGrid[row, col] = Instantiate(_tilePrefab, transform);
                            _mapGrid[row, col].gameObject.transform.position = new Vector3(col + 0.5f, 0, row + 0.5f);
                        }
                }
            }
        }

        public void ShowMap(int[,] map)
        {
            for (int row = 0; row < map.GetLength(0); row++)
                for (int col = 0; col < map.GetLength(1); col++)
                    _mapGrid[row, col].SetColor(map[row, col]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                int[,] map = Map.Generate(_mapSettings);
                SpawnMap(map);
                ShowMap(map);
            }
        }
    }
}
