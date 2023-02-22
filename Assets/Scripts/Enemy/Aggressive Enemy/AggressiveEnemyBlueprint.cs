using UnityEngine;

namespace TowerDefense.Enemy
{
    /// <summary>
    /// Aggressive enemy blueprint.
    /// </summary>
    [CreateAssetMenu(fileName = "Aggressive Enemy BP", menuName = "Tower Defense/Enemy Blueprints/Aggressive Enemy")]
    public class AggressiveEnemyBlueprint : EnemyBlueprint<AggressiveEnemy, AggressiveEnemyStats> { }
}