using System;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Timer UI.
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerUIText;

        public void UpdateTime(float time)
            => _timerUIText.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");  
    }
}