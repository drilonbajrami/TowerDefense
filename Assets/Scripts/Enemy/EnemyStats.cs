using UnityEngine;

namespace TowerDefense.Enemy
{
    /// <summary>
    /// Abstract data class for holding enemy stats.
    /// </summary>
    [System.Serializable]
    public abstract class EnemyStats
    {
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public float HP { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float MinSpeed { get; private set; }
        [field: SerializeField] public int Money { get; private set; }

        public void SetLevel(int level) => Level = level;
    }
}