using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense.Map
{
    public class MapViewPointerHandler : MonoBehaviour, IMapViewPointerHandler,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerMoveHandler
    {
        public ITile[,] Map { get; private set; }
        public IMapSettings Settings { get; private set; }

        /// <summary>
        /// Feedback tile prefab.
        /// </summary>
        [SerializeField] private GameObject _feedbackTilePrefab;

        /// <summary>
        /// The transform of the feedback tile.
        /// </summary>
        private Transform _feedbackTile;

        /// <summary>
        /// The renderer of the feedback tile.
        /// </summary>
        private MeshRenderer _feedbackRenderer;
        public MeshCollider MapCollider { get; private set; }

        public AudioSource _audioSource;

        private Vector3 _cachedMousePosition = Vector3.zero;
        private (int row, int col) NoneSelectedTile = (-1, -1);
        public (int row, int col) SelectedTileIndex { get; private set; } = (-1, -1);
        public readonly Vector3 NoneSelectedTilePosition = new(-100, 0, -100);

        private bool _handleInput = false;

        public ITile SelectedTile
            => SelectedTileIndex != NoneSelectedTile ? Map[SelectedTileIndex.row, SelectedTileIndex.col] : null;

        public void SetMap(ITile[,] map, IMapSettings settings)
        {
            Map = map;
            Settings = settings;
            MapCollider = GetComponent<MeshCollider>();

            _handleInput = Map != null && Settings != null && MapCollider != null;

            if (_feedbackTile == null)
                _feedbackTile = Instantiate(_feedbackTilePrefab, null).GetComponent<Transform>();

            _feedbackTile.transform.localScale = new Vector3(Settings.TileSize * 0.99f, Settings.TileSize * 0.99f, 1f);
            _feedbackRenderer = _feedbackTile.GetComponent<MeshRenderer>();
            _feedbackTile.gameObject.SetActive(false);
        }

        /// <summary>
        /// Enables or disables input handling.
        /// </summary>
        public void HandleInput(bool input)
        {
            _handleInput = input;
            if (!input) {
                SelectedTileIndex = NoneSelectedTile;
                _feedbackRenderer.enabled = false;
            }
        }

        /// <summary>
        /// Enables feedback tile if mouse is over the map view.
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_handleInput) return;
            _feedbackTile.gameObject.SetActive(true);
        }

        /// <summary>
        /// Disables feedback tile if mouse is not over the map view.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_handleInput) return;
            SelectedTileIndex = NoneSelectedTile;
            _feedbackTile.gameObject.SetActive(false);
        }

        /// <summary>
        /// Selects the tile from map view that is under the pointer.
        /// </summary>
        public void OnPointerMove(PointerEventData eventData)
        {
            if (!_handleInput) return;

            if (!(_feedbackTile.gameObject.activeSelf || _cachedMousePosition != Input.mousePosition))
                return;

            _cachedMousePosition = Input.mousePosition;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(_cachedMousePosition);

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Tower"), QueryTriggerInteraction.Ignore))
                return;

            int row = (int)(hit.point.z / Settings.TileSize);
            int col = (int)(hit.point.x / Settings.TileSize);

            if (SelectedTileIndex == (row, col)) return;

            SelectedTileIndex = (row, col);

            if (Map[row, col].Zone == Zone.Build && !Map[row, col].Occupied) {
                if (!_audioSource.isPlaying)
                    _audioSource.Play();
                _feedbackRenderer.enabled = true;
                _feedbackTile.position = new(Settings.TileSize * (col + 0.5f),
                                             0.25f,
                                             Settings.TileSize * (row + 0.5f));
            }
            else {
                SelectedTileIndex = NoneSelectedTile;
                _feedbackRenderer.enabled = false;
            }
        }
    }
}