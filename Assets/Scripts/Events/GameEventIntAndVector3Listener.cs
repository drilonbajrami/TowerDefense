using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    [System.Serializable]
    public class IntAndVector3Event : UnityEvent<int, Vector3> { }

    public class GameEventIntAndVector3Listener : MonoBehaviour
    {
        [SerializeField] private GameEventIntAndVector3 Event;

        public IntAndVector3Event Response;

        private void OnEnable() => Event.RegisterListener(this);
        private void OnDisable() => Event.UnregisterListener(this);
        public void OnEventRaised(int intValue, Vector3 vecValue) => Response.Invoke(intValue, vecValue);
    }
}
