namespace TowerDefense.Enemy
{
    /// <summary>
    /// Passive enemy actor.
    /// </summary>
    public class PassiveEnemy : EnemyActor<PassiveEnemy, PassiveEnemyStats> { }

    /// <summary>
    /// Data class for <see cref="PassiveEnemy"/> stats.
    /// </summary>
    [System.Serializable]
    public class PassiveEnemyStats : EnemyStats { }
}