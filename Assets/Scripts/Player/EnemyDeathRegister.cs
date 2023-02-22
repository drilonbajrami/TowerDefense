using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Updates player's money when enemies die.
    /// </summary>
    public class EnemyDeathRegister : MonoBehaviour
    {
        [SerializeField] private Money _playerMoney;

        /// <summary>
        /// Adds the enemy money to the players money.
        /// </summary>
        /// <param name="rewardMoney"></param>
        public void OnEnemyDied(int rewardMoney) => _playerMoney.AddAmount(rewardMoney);
    }
}