using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Health bar UI element.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _fill;

        /// <summary>
        /// Store main camera for LookAt() rotation.
        /// </summary>
        private Camera _mainCam;

        private void Awake() => _mainCam = Camera.main;

        public void SetMaxHP(float health)
        {
            _slider.maxValue = health;
            _slider.value = health;
            _fill.color = _gradient.Evaluate(1f);
        }

        public void SetHP(float health)
        {
            _slider.value = health;
            _fill.color = _gradient.Evaluate(_slider.normalizedValue);
        }

        /// <summary>
        /// Updates the transform to look at the main camera.
        /// </summary>
        private void Update() => transform.LookAt(_mainCam.transform, Vector3.down);
    }
}