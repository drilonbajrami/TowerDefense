using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Cannon ball projectile.
    /// </summary>
    public class CannonBall : Projectile
    {
        /// <summary>
        /// The projectile pool this cannon ball is part of.
        /// </summary>
        public CannonBallPool ProjectilePool { get; private set; }

        [SerializeField] private float _force = 5f;
        [SerializeField] private float _offMapReleaseDistance = 50f;

        void FixedUpdate()
        {
            if (IsFired) {
                if (Vector3.Distance(transform.position, Vector3.zero) > _offMapReleaseDistance)
                    ProjectilePool.Pool.Release(this);
            }
        }

        public override void Fire()
        {
            _rigidBody.AddForce(transform.forward * _force, ForceMode.Impulse);
            IsFired = true;
            transform.parent = null;
        }

        public override void SetPool(object pool) => ProjectilePool = pool as CannonBallPool;

        private void OnCollisionEnter(Collision collision)
        {
            if (DamageType is AreaOfEffect) {
                int maxColliders = 10;
                Collider[] hitColliders = new Collider[maxColliders];
                int numColliders = Physics.OverlapSphereNonAlloc(transform.position, 5f, hitColliders,
                                                                 LayersToHit, QueryTriggerInteraction.Ignore);
                for (int i = 0; i < numColliders; i++)
                    DamageType.ApplyDamage(hitColliders[i], DamagePoints);
            }
            else
                DamageType.ApplyDamage(collision.collider, DamagePoints);

            ProjectilePool.Pool.Release(this);
        }

        public override void ResetState()
        {
            IsFired = false;
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;
            transform.localScale = new Vector3(1f, 1f, 1f);
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
        }
    }
}