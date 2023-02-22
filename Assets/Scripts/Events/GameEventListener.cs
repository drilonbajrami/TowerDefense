﻿using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent Event;

        public UnityEvent Response;

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
        public void OnEventRaised() => Response.Invoke();
    }
}