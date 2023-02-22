using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Ballista arrow projectile.
    /// </summary>
    public class BallistaArrow : Projectile
    {
        /// <summary>
        /// The projectile pool this arrow is part of.
        /// </summary>
        public BallistaArrowPool ProjectilePool { get; private set; }

        [SerializeField] private float _force = 10f;
        [SerializeField] private float _offMapReleaseDistance = 50f;

        void FixedUpdate()
        {
            if (IsFired) {
                // If outside of map then release.
                if (Vector3.Distance(transform.position, Vector3.zero) > _offMapReleaseDistance)
                    ProjectilePool.Pool.Release(this);
            }
        }

        public override void Fire()
        {
            IsFired = true;
            _rigidBody.AddForce(transform.forward * _force, ForceMode.Impulse);
            transform.parent = null;
        }

        public override void SetPool(object pool) => ProjectilePool = pool as BallistaArrowPool;

        private void OnCollisionEnter(Collision collision)
        {
            DamageType.ApplyDamage(collision.collider, DamagePoints);
            ProjectilePool.Pool.Release(this);
        }

        public override void ResetState()
        {
            IsFired = false;
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;
            transform.localScale = new Vector3(1f, 1f, 0.5f);
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
        }
    }
}