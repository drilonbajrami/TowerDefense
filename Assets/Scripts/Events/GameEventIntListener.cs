using System;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    // This makes sure that the IntEvent is editable in Unity Inspector.
    // Try removing [serializable] and see what would happen.
    [Serializable]
    public class IntEvent : UnityEvent<int> { }

    public class GameEventIntListener : MonoBehaviour
    {
        [SerializeField] private GameEventInt Event;

        public IntEvent Response;

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
        public void OnEventRaised(int intValue) => Response.Invoke(intValue);
    }
}