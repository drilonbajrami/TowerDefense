using TowerDefense.Map;
using UnityEngine;

namespace TowerDefense.Towers
{
    public class TowerPlacer : MonoBehaviour
    {
        [SerializeField]
        private MapViewPointerHandler _pointer;

        public Tower TowerToPlace { get; private set; }

        public void PlaceTower(Tower tower) => TowerToPlace = tower;
        
        /// <summary>
        /// Takes care of placing the tower on a map tile.
        /// </summary>
        private void Update()
        {
            if (TowerToPlace == null) return;

            if (Input.GetMouseButtonDown(0) && _pointer.SelectedTile != null) {
                TowerToPlace.PlaceOnMap(_pointer.SelectedTile.WorldPosition);
                _pointer.SelectedTile.SetTower(TowerToPlace);
                TowerToPlace = null;
                return;
            }

            TowerToPlace.SetPosition(_pointer.SelectedTile != null ? _pointer.SelectedTile.WorldPosition
                                                                   : _pointer.NoneSelectedTilePosition);
        }
    }
}