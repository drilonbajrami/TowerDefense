using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Slowdown Debuff.
    /// </summary>
    public class SlowDownDebuff : DamageType
    {
        /// <summary>
        /// Duration of the slowdown debuff.
        /// </summary>
        [SerializeField] private float _slowDownDuration = 3f;

        public override void ApplyDamage(Collider[] colliders, float damage)
        {
            for (int i = 0; i < colliders.Length - 1; i++)
                ApplyDamage(colliders[i], damage);
        }

        public override void ApplyDamage(Collider collider, float damage)
        {
            if (collider.gameObject.TryGetComponent(out ISlowDown slowable))
                slowable.SlowDown(_slowDownDuration);

            if (collider.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(damage);
        }
    }
}