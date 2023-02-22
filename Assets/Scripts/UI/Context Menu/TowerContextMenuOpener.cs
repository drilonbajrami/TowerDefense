using TowerDefense.Towers;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefense.ContextMenu
{

    public class TowerContextMenuOpener : MonoBehaviour
    {
        [SerializeField] private TowerContextMenu _menu;
        [SerializeField] private Money _playerMoney;
        // Tower for which the context menu is opened for.
        private Tower tower;

        public LayerMask raycastLayers;

        /// <summary>
        /// Opens/closes the tower context menu when clicking on towers,
        /// </summary>
        private void Update()
        {
            // Right click
            if (Input.GetMouseButtonDown((int)MouseButton.RightMouse)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayers.value, QueryTriggerInteraction.Ignore)) {

                    hit.collider.gameObject.TryGetComponent(out tower);
                    if (tower) {
                        _menu.OpenMenu();
                        _menu.SetPosition(Input.mousePosition);
                        _menu.AddListenerToUpgradeButton(tower.Upgrade);
                        _menu.AddListenerToSellButton(tower.Sell);
                        _menu.EnableUpgradeButton(tower.CanUpgrade(_playerMoney.Amount, out int towerCost));
                        _menu.SetUpgradeCost(towerCost);
                        _menu.SetSellCost(tower.Stats.SellCost);
                    }
                }
                else
                    _menu.CloseMenu();
            }

            // Left click
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayers.value, QueryTriggerInteraction.Ignore))
                    _menu.CloseMenu();
            }
        }
    }
}
