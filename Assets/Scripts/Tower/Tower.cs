using TowerDefense.Weapons;
using UnityEngine;

namespace TowerDefense.Towers
{
    public class Tower : MonoBehaviour, IUpgrade, IDamageable
    {
        /// <summary>
        /// Reference to this tower's blueprint (scriptable object).
        /// </summary>  
        public TowerBlueprint Blueprint { get; private set; }

        /// <summary>
        /// Tower cached stats.
        /// </summary>
        public TowerStats Stats { get; private set; }

        /// <summary>
        /// Current level of the tower.
        /// </summary>
        [field: SerializeField] public int Level { get; private set; }

        // Components
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
        [field: SerializeField] public TargetDetector TargetDetector { get; private set; }

        // Events
        [SerializeField] private GameEventInt OnTowerUpgrade;
        [SerializeField] private GameEventInt OnTowerSell;
        [SerializeField] private GameEventInt OnTowerBuy;

        // Tower weapon
        public IWeapon Weapon { get; private set; }

        /// <summary>
        /// Sets a reference to the blueprint used to create this tower.
        /// </summary>
        /// <param name="blueprint">The tower blueprint.</param>
        public void SetBlueprint(TowerBlueprint blueprint) => Blueprint = blueprint;

        /// <summary>
        /// Gets the weapon component from the attached weapon game object.
        /// </summary>
        public virtual void Awake() => Weapon = GetComponentInChildren<IWeapon>();

        /// <summary>
        /// Seeks for target to shoot at.
        /// </summary>
        private void Update() => SeekTarget();

        /// <summary>
        /// Sets the data for this enemy.<br/>
        /// <i><b>Warning</b>: If you override this method, make sure to call the base method as well:</i><br/>
        /// <i>base.<b>SetStats(S stats)</b></i>
        /// </summary>
        /// <param name="stats">Enemy blueprint.</param>
        public virtual void SetStats(TowerStats stats)
        {
            Stats = stats;
            Level = stats.Level;
            Health.SetHP(stats.HP);
            Weapon.SetRPM(stats.RPM);
            Weapon.DamagePoints = stats.DamagePoints;
        }

        /// <summary>
        /// Checks if there is an available upgrade for this tower,
        /// and calculates the cost of the available upgrade.
        /// </summary>
        /// <returns>Whether the upgrade is possible or not.</returns>
        public bool CanUpgrade(int moneyAvailable, out int upgradeCost)
        {
            if (Blueprint.UpgradeAvailableForLevel(Level)) {
                upgradeCost = Blueprint.Levels[Level + 1].UpgradeCost;
                return upgradeCost <= moneyAvailable;
            }
            else {
                upgradeCost = 0;
                return false;
            }
        }

        /// <inheritdoc/>
        public void Upgrade()
        {
            if (Blueprint.UpgradeAvailableForLevel(Level)) {
                SetStats(Blueprint.GetLevelStats(Level + 1));
                OnTowerUpgrade.Raise(Stats.UpgradeCost);
            }
        }

        public void Sell()
        {
            OnTowerSell.Raise(Stats.SellCost);
            Destroy(gameObject);
        }

        /// <summary>
        /// Places the tower on the map and invokes the OnTowerBuy event.
        /// </summary>
        /// <param name="position">Position on the map.</param>
        public void PlaceOnMap(Vector3 position)
        {
            SetPosition(new Vector3(position.x, position.y + 0.2f, position.z));
            // Enable tower collider if finally place on the map during shopping.
            Collider.enabled = true;
            OnTowerBuy.Raise(Stats.UpgradeCost);
        }

        public void SetPosition(Vector3 position) => transform.position = position;

        public void TakeDamage(float damage) => Health.DepleteHP(damage);

        public void SeekTarget()
        {
            if (TargetDetector.CurrentTarget != null)
                Weapon.AimAt(TargetDetector.CurrentTarget.transform.position);
        }
    }
}