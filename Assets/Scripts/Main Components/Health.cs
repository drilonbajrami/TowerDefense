using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Health component.
    /// </summary>
    public class Health : MonoBehaviour
    {
        public float InitialHP { get; private set; }
        public float CurrentHP { get; private set; }

        /// <summary>
        /// HealthBar UI element.
        /// </summary>
        [field: SerializeField] public HealthBar HealthBar { get; private set; }

        public void SetHP(float value)
        {
            InitialHP = CurrentHP = value;
            if (HealthBar != null) {
                HealthBar.SetMaxHP(value);
                HealthBar.SetHP(value);
            }
        }

        public void DepleteHP(float damage)
        {
            CurrentHP -= damage;
            if (HealthBar != null) HealthBar.SetHP(CurrentHP);
            if (CurrentHP <= 0) Destroy(gameObject);
        }
    }
}