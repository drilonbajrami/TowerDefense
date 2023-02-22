using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Data class for holding tower stats.
    /// </summary>
    [System.Serializable]
    public class TowerStats
    {
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public float HP { get; private set; }
        [field: SerializeField] public float DamagePoints { get; private set; }
        [field: SerializeField] public int RPM { get; private set; }
        [field: SerializeField] public int UpgradeCost { get; private set; }
        [field: SerializeField] public int SellCost { get; private set; }

        /// <summary>
        /// Set tower level number for these stats.
        /// </summary>
        /// <param name="level">Level number.</param>
        public void SetLevel(int level) => Level = level;
    }
}