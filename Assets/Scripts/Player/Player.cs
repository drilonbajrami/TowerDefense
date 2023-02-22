using UnityEngine;

namespace TowerDefense.Main
{
    /// <summary>
    /// Player class.
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _startingMoney = 2500;

        public Money Money { get; private set; }

        private void Awake()
        {
            if (Money == null) Money = GetComponent<Money>();
            Money.SetAmount(_startingMoney);
        }
    }
}