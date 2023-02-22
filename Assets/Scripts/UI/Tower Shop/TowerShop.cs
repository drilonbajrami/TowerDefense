using System.Collections.Generic;
using TowerDefense.Towers;
using UnityEngine;

namespace TowerDefense
{
    public class TowerShop : MonoBehaviour
    {
        /// <summary>
        /// Tower blueprints for the shop.
        /// </summary>
        [field: SerializeField] public List<TowerBlueprint> towerBlueprints;

        /// <summary>
        /// Option prefab.
        /// </summary>
        public TowerShopOption _optionPrefab;

        private List<TowerShopOption> options = new();

        [field: SerializeField] public Money PlayerMoney { get; private set; }
        [field: SerializeField] public TowerPlacer TowerPlacer { get; private set; }

        /// <summary>
        /// Setup all available options on start.
        /// </summary>
        private void Start()
        {
            if (towerBlueprints != null) {
                for (int i = 0; i < towerBlueprints.Count; i++) {
                    TowerShopOption option = Instantiate(_optionPrefab, transform);
                    option.SetBlueprint(towerBlueprints[i]);
                    option.Shop = this;
                    options.Add(option);
                }
            }
        }

        private void Update() => Refresh();

        /// <summary>
        /// Refreshes the shop by disabling any unavalaible towers.
        /// </summary>
        public void Refresh()
        {
            for (int i = 0; i < options.Count; i++)
                options[i].RefreshOption(PlayerMoney.Amount);
        }

        /// Shows the shop.
        /// </summary>
        public void Show() => gameObject.SetActive(true);
        
        /// <summary>
        /// Hides the shop.
        /// </summary>
        public void Hide() => gameObject.SetActive(false);
    }
}
