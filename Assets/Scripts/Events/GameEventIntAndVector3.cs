using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "GameEventIntAndVector3", menuName = "Events/EventWithIntAndVector3")]
    public class GameEventIntAndVector3 : ScriptableObject
    {
        private List<GameEventIntAndVector3Listener> _listeners = new List<GameEventIntAndVector3Listener>();

        public void Raise(int intValue, Vector3 vecValue)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised(intValue, vecValue);
        }

        public void RegisterListener(GameEventIntAndVector3Listener listener) => _listeners.Add(listener);
        public void UnregisterListener(GameEventIntAndVector3Listener listener) => _listeners.Remove(listener);
    }
}
