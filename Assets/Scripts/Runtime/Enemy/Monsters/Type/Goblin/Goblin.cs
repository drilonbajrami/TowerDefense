using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    [System.Serializable]
    public class Goblin : Monster<GoblinStats, GoblinBlueprint>
    {
        public override GoblinStats GetStats(GoblinBlueprint data)
        {
            return new GoblinStats(data);
        }
    }
}
