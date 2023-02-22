using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Area Of Effect.
    /// </summary>
    public class AreaOfEffect : DamageType
    {
        public override void ApplyDamage(Collider[] colliders, float damage)
        {
            for (int i = 0; i < colliders.Length - 1; i++)
                ApplyDamage(colliders[i], damage);
        }

        public override void ApplyDamage(Collider collider, float damage)
        {
            if (collider.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(damage);
        }
    }
}