using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    [System.Serializable]
    public class GhostStats : Stats<GhostBlueprint>
    {
        public int ghostID;

        public GhostStats(GhostBlueprint data) : base(data)
        {
            ghostID = data.ghostID;
        }
    }
}
