using UnityEngine;

namespace TowerDefense.Enemy
{
    /// <summary>
    /// Passive enemy blueprint.
    /// </summary>
    [CreateAssetMenu(fileName = "Passive Enemy BP", menuName = "Tower Defense/Enemy Blueprints/Passive Enemy")]
    public class PassiveEnemyBlueprint : EnemyBlueprint<PassiveEnemy, PassiveEnemyStats> { }
}