using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.ContextMenu
{
    /// <summary>
    /// Context menu button.
    /// </summary>
    public class ContextMenuButton : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TMP_Text Text { get; private set; }

        public void AddListener(Action action)
            => Button.onClick.AddListener(() => action.Invoke());

        public void RemoveAllListeners()
            => Button.onClick.RemoveAllListeners();
    }
}