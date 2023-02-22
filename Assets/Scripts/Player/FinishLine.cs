using UnityEngine;

namespace TowerDefense
{
    public class FinishLine : MonoBehaviour
    {
        /// <summary>
        /// Number of lives.
        /// </summary>
        [SerializeField] private int _lives;

        [SerializeField] private BoxCollider _collider;
        [SerializeField] private LayerMask _layersToDetect;

        // Event to raise when lives/touchdowns are depleted.
        [SerializeField] public GameEvent GameOverEvent;

        // Life counter UI element.
        [SerializeField] private LifeCounter _livesCounterUI;

        private void Start()
        {
            if (_livesCounterUI) _livesCounterUI.SetLifeCounter(_lives);
        }

        public void SetLives(int lives)
        {
            _lives = lives;
            if (_livesCounterUI)
                _livesCounterUI.SetLifeCounter(_lives);
        }

        public void SetSizeAndPosition(float size, Vector3 position)
        {
            _collider.size = new Vector3(size, _collider.size.y, size);
            _collider.center = new Vector3(0, _collider.size.y / 2f, 0);
            transform.position = position;
        }

        /// <summary>
        /// Removes a life and raises Game Over Event if there are no lives left.
        /// </summary>
        public void UpdateLivesCount()
        {
            if (_lives > 0) {
                _lives--;
                _livesCounterUI.RemoveLife();
                if (_lives == 0) GameOverEvent.Raise();
            }
        }

        /// <summary>
        /// If enemy enters the finish line trigger than update lives count.
        /// </summary>
        public void OnTriggerEnter(Collider other)
        {
            if (_layersToDetect.ContainsLayer(other.gameObject.layer)) {
                Destroy(other.gameObject);
                UpdateLivesCount();
            }
        }
    }
}