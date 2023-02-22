using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Ballista weapon.
    /// </summary>
    public class Ballista : MonoBehaviour, IWeapon
    {
        /// <summary>
        /// The arrow slot where the arrow projectile is spawned.
        /// </summary>
        [SerializeField] private Transform _arrowSlot;

        /// <summary>
        /// The ballista arrow projectile.
        /// </summary>
        private BallistaArrow _arrow;

        /// <summary>
        /// Reference to a ballista arrow projectile pool.
        /// </summary>
        [field: SerializeField] public BallistaArrowPool Pool { get; private set; }

        public int RPM { get; private set; } = 120;
        public float DamagePoints { get; set; }
        public float CooldownTime { get; private set; }
        public bool HasFired { get; private set; } = false;

        /// <inheritdoc/>
        [field: SerializeField] public DamageType DamageType { get; private set; }

        /// <summary>
        /// Angle threshold at which to shoot, when aiming towards the target.
        /// </summary>
        [SerializeField] private float _shootAngleThreshold = 0.5f;

        [SerializeField] private float _rotationSpeed = 100f;

        [SerializeField] private int _projectileLayer;

        [SerializeField] private LayerMask LayersToHit;

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
            _arrow.Fire();
            HasFired = true;
        }

        public void Reload()
        {
            if (Pool != null) {
                _arrow = Pool.Get(_arrowSlot);
                _arrow.SetLayerMask(_projectileLayer);
                _arrow.SetLayerMaskToHit(LayersToHit);
                _arrow.SetDamageType(DamageType);
                _arrow.SetDamage(DamagePoints);
                HasFired = false;
                CooldownTime = 60f / RPM;
            }
        }

        public void SetRPM(int rpm) => RPM = rpm;
    }
}