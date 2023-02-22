using UnityEngine;

namespace TowerDefense.ContextMenu
{
    /// <summary>
    /// Context menu with options (buttons).
    /// </summary>
    public class ContextMenu : MonoBehaviour
    {
        public void OpenMenu() => gameObject.SetActive(true);
        public void CloseMenu() => gameObject.SetActive(false);
        public void SetPosition(Vector3 position)
            => gameObject.GetComponent<RectTransform>().position = position;
    }
}