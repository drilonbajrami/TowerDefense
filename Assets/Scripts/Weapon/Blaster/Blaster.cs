using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Blaster weapon.
    /// </summary>
    public class Blaster : MonoBehaviour, IWeapon
    {
        /// <summary>
        /// Left chamber of the blaster turret where the beam projectile is spawned.
        /// </summary>
        [SerializeField] private Transform _leftChamber;

        /// <summary>
        /// Right chamber of the blaster turret where the beam projectile is spawned.
        /// </summary>
        [SerializeField] private Transform _rightChamber;

        /// <summary>
        /// Reference to beam loaded in the left chamber.
        /// </summary>
        private BlasterBeam _leftBlasterBeam;

        /// <summary>
        /// Reference to beam loaded in the right chamber.
        /// </summary>
        private BlasterBeam _rightBlasterBeam;

        /// <summary>
        /// Reference to a blaster beam projectile pool.
        /// </summary>
        [field: SerializeField] public BlasterBeamPool Pool { get; private set; }

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
            _leftBlasterBeam.Fire();
            _rightBlasterBeam.Fire();
            HasFired = true;
        }

        public void Reload()
        {
            if (Pool != null) {
                _leftBlasterBeam = Pool.Get(_leftChamber);
                _leftBlasterBeam.SetLayerMask(_projectileLayer);
                _leftBlasterBeam.SetLayerMaskToHit(LayersToHit);
                _leftBlasterBeam.SetDamageType(DamageType);
                _leftBlasterBeam.SetDamage(DamagePoints);
                _rightBlasterBeam = Pool.Get(_rightChamber);
                _rightBlasterBeam.SetLayerMask(_projectileLayer);
                _rightBlasterBeam.SetLayerMaskToHit(LayersToHit);
                _rightBlasterBeam.SetDamageType(DamageType);
                _rightBlasterBeam.SetDamage(DamagePoints);
                HasFired = false;
                CooldownTime = 60f / RPM;
            }
        }

        public void SetRPM(int rpm) => RPM = rpm;
    }
}