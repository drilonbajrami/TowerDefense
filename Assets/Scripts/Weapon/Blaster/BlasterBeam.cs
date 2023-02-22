using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Blaster beam projectile.
    /// </summary>
    public class BlasterBeam : Projectile
    {
        /// <summary>
        /// The projectile pool this beam is part of.
        /// </summary>
        public BlasterBeamPool ProjectilePool { get; private set; }

        [SerializeField] private float force = 10f;
        [SerializeField] private float _offMapReleaseDistance = 50f;

        void FixedUpdate()
        {
            if (IsFired) {
                transform.position += Time.deltaTime * force * transform.forward;

                // If outside of map then release.
                if (Vector3.Distance(transform.position, Vector3.zero) > _offMapReleaseDistance)
                    ProjectilePool.Pool.Release(this);
            }
        }

        public override void Fire()
        {
            IsFired = true;
            transform.parent = null;
        }

        public override void SetPool(object pool) => ProjectilePool = pool as BlasterBeamPool;

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
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
        }
    }
}