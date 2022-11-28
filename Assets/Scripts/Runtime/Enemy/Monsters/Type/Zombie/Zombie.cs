using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    [System.Serializable]
    public class Zombie : Monster<ZombieStats, ZombieBlueprint>
    {
        public override ZombieStats GetStats(ZombieBlueprint data)
        {
            // this will have more stuff in it, leave it like this...
            return new ZombieStats(data);
        }
    }
}