using System;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Timer monobehavior component.
    /// </summary>
    public class Timer : MonoBehaviour
    {
        public bool Running { get; private set; }
        public float ElapsedTime { get; private set; }

        public event Action OnTimerElapsed;

        private void Update() => Tick();

        public void Begin(float interval)
        {
            Running = true;
            ElapsedTime = interval;
        }

        private void Tick()
        {
            if (!Running) return;
            ElapsedTime -= Time.deltaTime;
            if (ElapsedTime <= 0f) {
                Running = false;
                OnTimerElapsed?.Invoke();
            }
        }
    }
}
