using System;
using UnityEngine;

namespace TowerDefense.ContextMenu
{
    /// <summary>
    /// Context menu for towers.
    /// </summary>
    public class TowerContextMenu : ContextMenu
    {
        [field: SerializeField] public ContextMenuButton Upgrade { get; private set; }
        [field: SerializeField] public ContextMenuButton Sell { get; private set; }

        public void EnableUpgradeButton(bool condition) => Upgrade.Button.interactable = condition;

        public void SetUpgradeCost(int cost) => Upgrade.Text.text = "Upgrade " + cost.ToString();

        public void SetSellCost(int cost) => Sell.Text.text = "Sell " + cost.ToString();

        public void AddListenerToUpgradeButton(Action action)
        {
            Upgrade.RemoveAllListeners();
            Upgrade.AddListener(action);
            Upgrade.AddListener(CloseMenu);
        }

        public void AddListenerToSellButton(Action action)
        {
            Sell.RemoveAllListeners();
            Sell.AddListener(action);
            Sell.AddListener(CloseMenu);
        }
    }
}