using TMPro;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Money component.
    /// </summary>
    public class Money : MonoBehaviour
    {
        public int Amount { get; private set; }

        /// <summary>
        /// Money UI.
        /// </summary>
        [SerializeField] private TMP_Text _moneyUI;

        public void SetAmount(int amount)
        {
            Amount = amount;
            UpdateUI();
        }

        public void AddAmount(int amount)
        {
            Amount += amount;
            UpdateUI();
        }

        public void RemoveAmount(int amount)
        {
            Amount -= amount;
            UpdateUI();
        }

        /// <summary>
        /// Updates the money UI text, if there is one assigned.
        /// </summary>
        private void UpdateUI()
        {
            if (_moneyUI) _moneyUI.text = Amount.ToString();
        }
    }
}