using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    [System.Serializable]
    public class ZombieStats : Stats<ZombieBlueprint>
    {
        public int zombieID;

        public ZombieStats(ZombieBlueprint data) : base(data)
        {
            zombieID = data.zombieID;
        }
    }
}