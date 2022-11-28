using UnityEngine;

namespace TowerDefense.Runtime
{
    /// <summary>
    /// Class for holding stats data for an enemy.
    /// </summary>
    [System.Serializable]
    public abstract class Stats<T> where T : Blueprint
    {
        /// <summary>
        /// Basic stats.
        /// </summary>
        [SerializeField] public BaseStats baseStats;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Stats(T data)
        {
            baseStats = new BaseStats(data.baseStats);
        }
    }

    [System.Serializable]
    public class BaseStats
    {
        [SerializeField] private float _hp;
        [SerializeField] private float _speed;
        [SerializeField] private int _money;

        public float HP => _hp;
        public float Speed => _speed;
        public int Money => _money;

        public BaseStats(float hp, float speed, int money)
        {
            _hp = hp;
            _speed = speed;
            _money = money;
        }

        public BaseStats(BaseStats baseStats)
        {
            _hp = baseStats.HP;
            _speed = baseStats.Speed;
            _money = baseStats.Money;
        }

        public bool TakeDamage(float damage)
        {
            _hp -= damage;
            return _hp <= 0;
        }

        public int DropMoney()
        {
            int reward = _money;
            _money = 0;
            return reward;
        }
    }
}