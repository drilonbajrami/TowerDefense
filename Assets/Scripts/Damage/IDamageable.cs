namespace TowerDefense
{
    /// <summary>
    /// All entities that inherit from this interface become
    /// susceptible to health damage.
    /// </summary>
    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}