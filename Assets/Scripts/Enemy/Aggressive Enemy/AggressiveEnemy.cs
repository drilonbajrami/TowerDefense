using TowerDefense.Weapons;
using UnityEngine;

namespace TowerDefense.Enemy
{
    /// <summary>
    /// Aggressive enemy actor.
    /// </summary>
    public class AggressiveEnemy : EnemyActor<AggressiveEnemy, AggressiveEnemyStats>
    {
        public IWeapon Weapon { get; private set; }
        [field: SerializeField] public TargetDetector TargetDetector { get; private set; }

        /// <summary>
        /// Cache the weapon component.
        /// </summary>
        private void Awake() => Weapon = gameObject.GetComponentInChildren<IWeapon>();

        /// <summary>
        /// Aims and shoots at targets if there are any within range.
        /// </summary>
        private void Update()
        {
            if (TargetDetector.CurrentTarget != null)
                Weapon.AimAt(TargetDetector.CurrentTarget.transform.position);
        }

        public override void SetStats(AggressiveEnemyStats stats)
        {
            base.SetStats(stats);
            Weapon.DamagePoints = stats.DamagePoints;
        }
    }

    /// <summary>
    /// Data class for <see cref="AggressiveEnemy"/> stats.
    /// </summary>
    [System.Serializable]
    public class AggressiveEnemyStats : EnemyStats
    {
        [field: SerializeField] public int DamagePoints { get; private set; }
        [field: SerializeField] public int RPM { get; private set; }
    }
}