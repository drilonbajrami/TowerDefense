using TMPro;
using TowerDefense.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Tower option for buying a tower from the tower shop.
    /// </summary>
    [System.Serializable]
    public class TowerShopOption : MonoBehaviour
    {
        [SerializeField] private TMP_Text _towerName;
        [SerializeField] private TMP_Text _towerCost;
        [SerializeField] private Image _towerPreview;
        [SerializeField] private Button _button;

        public TowerShop Shop { get; set; }
        public TowerBlueprint Blueprint { get; private set; }

        public void SetBlueprint(TowerBlueprint blueprint)
        {
            Blueprint = blueprint;
            _towerPreview.sprite = blueprint.ShopIcon;
            _towerName.text = blueprint.Prefab.name;
            _towerCost.text = blueprint.Levels[0].UpgradeCost.ToString();
        }

        public void BuyTower() => Shop.TowerPlacer.PlaceTower(Blueprint.Spawn(0, Vector3.zero));
        
        public void RefreshOption(int moneyAmount)
            => _button.interactable = Blueprint.Levels[0].UpgradeCost <= moneyAmount;
    }
}