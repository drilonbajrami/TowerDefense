using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Cannon weapon.
    /// </summary>
    public class Cannon : MonoBehaviour, IWeapon
    {
        /// <summary>
        /// The bore chamber where the cannon balls are spawned.
        /// </summary>
        [SerializeField] private Transform _boreChamber;

        /// <summary>
        /// The cannon ball projectile.
        /// </summary>
        private CannonBall _cannonBall;

        /// <summary>
        /// Reference to a cannon ball projectile pool.
        /// </summary>
        [field: SerializeField] public CannonBallPool Pool { get; private set; }

        public int RPM { get; private set; } = 120;
        public float DamagePoints { get; set; }
        public float CooldownTime { get; private set; }
        public bool HasFired { get; private set; } = false;

        [field: SerializeField] public DamageType DamageType { get; private set; }

        /// <summary>
        /// Angle threshold at which to shoot, when aiming towards the target.
        /// </summary>
        [SerializeField] private float _shootAngleThreshold = 0.5f;

        [SerializeField] private float _rotationSpeed = 100f;

        [SerializeField] private int _projectileLayer;

        [SerializeField] private LayerMask _layersToHit;

        /// <summary>
        /// Reload weapon on start.
        /// </summary>
        private void Start() => Reload();

        /// <summary>
        /// Starts the cooldown time if the weapon is fired.
        /// </summary>
        private void Update()
        {
            if (HasFired) {
                CooldownTime -= Time.deltaTime;
                if (CooldownTime < 0f)
                    Reload();
            }
        }

        public void AimAt(Vector3 targetPosition)
        {
            Vector3 lookPos = targetPosition - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);

            float angle = Vector3.Angle(transform.forward, lookPos);
            if (angle < _shootAngleThreshold && HasFired == false)
                Shoot();
        }

        public void Shoot()
        {
            _cannonBall.Fire();
            HasFired = true;
        }

        public void Reload()
        {
            if (Pool != null) {
                _cannonBall = Pool.Get(_boreChamber);
                _cannonBall.SetLayerMask(_projectileLayer);
                _cannonBall.SetLayerMaskToHit(_layersToHit);
                _cannonBall.SetDamageType(DamageType);
                _cannonBall.SetDamage(DamagePoints);
                HasFired = false;
                CooldownTime = 60f / RPM;
            }
        }

        public void SetRPM(int rpm) => RPM = rpm;
    }
}