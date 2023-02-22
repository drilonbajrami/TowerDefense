using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "GameEventInt", menuName = "Events/EventWithInt")]
    public class GameEventInt : ScriptableObject
    {
        private List<GameEventIntListener> _listeners = new List<GameEventIntListener>();

        public void Raise(int value)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised(value);
        }

        public void RegisterListener(GameEventIntListener listener) => _listeners.Add(listener);
        public void UnregisterListener(GameEventIntListener listener) => _listeners.Remove(listener);
    }
}