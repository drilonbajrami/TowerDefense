using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Projectile behaviour class.
    /// </summary>
    public abstract class Projectile : MonoBehaviour
    {
        /// <summary>
        /// Damage dealing points.
        /// </summary>
        public float DamagePoints { get; private set; }

        [field: SerializeField] protected Rigidbody _rigidBody;
        [field: SerializeField] protected Collider _collider;
        public DamageType DamageType { get; private set; }
        public bool IsFired { get; protected set; }
        [field: SerializeField] public LayerMask LayersToHit { get; protected set; }

        /// <summary>
        /// Sets the damage points for this projectile.
        /// </summary>
        /// <param name="damage"></param>
        public void SetDamage(float damage) => DamagePoints = damage;

        /// <summary>
        /// Sets the damage type for this projectile.
        /// </summary>
        public void SetDamageType(DamageType damageType) => DamageType = damageType;

        /// <summary>
        /// Fires the projectile from the weapon.
        /// </summary>
        public abstract void Fire();

        /// <summary>
        /// Sets the projectile pool for this projectile.
        /// </summary>
        public abstract void SetPool(object pool);

        /// <summary>
        /// Sets the projectile's game object layer mask.
        /// </summary>
        /// <param name="layer">Layer ID.</param>
        public void SetLayerMask(int layer) => gameObject.layer = layer;

        /// <summary>
        /// Sets the layer mask for hit detection.
        /// </summary>
        /// <param name="layerMask"></param>
        public void SetLayerMaskToHit(LayerMask layerMask) => LayersToHit = layerMask;

        /// <summary>
        /// Resets the projectile state when released back to the projectile pool.
        /// </summary>
        public abstract void ResetState();
    }
}