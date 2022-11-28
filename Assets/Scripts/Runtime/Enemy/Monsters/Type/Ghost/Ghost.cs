using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    [System.Serializable]
    public class Ghost : Monster<GhostStats, GhostBlueprint>
    {
        public override GhostStats GetStats(GhostBlueprint data)
        {
            return new GhostStats(data);
        }
    }
}