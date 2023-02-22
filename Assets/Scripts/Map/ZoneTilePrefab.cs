using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Map
{
    /// <summary>
    /// Holds prefabs for a zone type.
    /// </summary>
    [System.Serializable]
    public class ZoneTilePrefab
    {
        /// <summary>
        /// Zone type.
        /// </summary>
        [field: SerializeField]
        public Zone Zone { get; private set; }

        /// <summary>
        /// Zone tile prefab.
        /// </summary>
        [field: SerializeField]
        public List<GameObject> Prefabs { get; private set; }
    }
}