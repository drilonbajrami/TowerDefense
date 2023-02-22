using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Damage type.
    /// </summary>
    public abstract class DamageType : MonoBehaviour
    {
        /// <summary>
        /// Applies damage type to multiple entities.
        /// </summary>
        public abstract void ApplyDamage(Collider[] colliders, float damage);

        /// <summary>
        /// Applies damage type to only one entity.
        /// </summary>
        public abstract void ApplyDamage(Collider collider, float damage);
    }
}