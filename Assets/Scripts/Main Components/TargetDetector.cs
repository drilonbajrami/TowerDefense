using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Target detector component.
    /// </summary>
    public class TargetDetector : MonoBehaviour
    {
        /// <summary>
        /// The trigger collider of the target detector.
        /// </summary>
        [SerializeField] private SphereCollider _collider;

        /// <summary>
        /// The layer mask of the target to detect.
        /// </summary>
        [SerializeField] private LayerMask layersToTarget;

        public List<GameObject> AvailableTargets { get; private set; } = new();
        public GameObject CurrentTarget { get; private set; }

        /// <summary>
        /// If there is no selected target then tries to set a new one.
        /// </summary>
        private void Update()
        {
            if (CurrentTarget == null) SetNewTarget();
        }

        /// <summary>
        /// Sets the radius of the target detector sphere collider/trigger.
        /// </summary>
        public void SetDetectorRadius(float radius) => _collider.radius = radius;

        /// <summary>
        /// Register game object as an available target.
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (!layersToTarget.ContainsLayer(other.gameObject.layer)) return;
            if (AvailableTargets.Contains(other.gameObject)) return;
            AvailableTargets.Add(other.gameObject);
        }

        /// <summary>
        /// Deregister game object as an available target.
        /// </summary>
        private void OnTriggerExit(Collider other)
        {
            if (!layersToTarget.ContainsLayer(other.gameObject.layer)) return;
            if (!AvailableTargets.Contains(other.gameObject)) return;
            AvailableTargets.Remove(other.gameObject);
        }

        /// <summary>
        /// Sets a new target if there is one within range of the detector.
        /// </summary>
        private void SetNewTarget()
        {
            if (AvailableTargets.Count == 0) return;

            foreach (GameObject target in AvailableTargets) {
                if (target != null) {
                    CurrentTarget = target;
                    return;
                }
            }
        }
    }
}