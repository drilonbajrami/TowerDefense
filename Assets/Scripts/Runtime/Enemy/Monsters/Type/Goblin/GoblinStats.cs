using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    [System.Serializable]
    public class GoblinStats : Stats<GoblinBlueprint>
    {
        public int goblinID;

        public GoblinStats(GoblinBlueprint data) : base(data)
        {
            goblinID = data.goblinID;
        }
    }
}
