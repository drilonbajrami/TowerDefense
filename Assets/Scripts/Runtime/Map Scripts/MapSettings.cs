using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    [CreateAssetMenu(fileName = "Map Settings", menuName = "Tower Defense/Map Settings")]
    public class MapSettings : ScriptableObject
    {
        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;

        public int Width => _width;
        public int Height => _height;

        public (int row, int col) HomeTile => (_height - 2, 0);
        public (int row, int col) SpawnTile => (1, _width - 1);

        private void OnValidate()
        {
            _width = _width == 0 ? 15 : (_width % 2 == 0 ? _width + 1 : _width);
            _width = Mathf.Clamp(_width, 9, _width);
         
            _height = _height == 0 ? 9 : (_height % 2 == 0 ? _height + 1 : _height);
            _height = Mathf.Clamp(_height, 9, _height);
        }
    }
}
