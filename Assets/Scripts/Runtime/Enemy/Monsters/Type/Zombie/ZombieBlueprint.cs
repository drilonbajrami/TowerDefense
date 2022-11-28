using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    [CreateAssetMenu(fileName = "Zombie", menuName = "Monster Data/Zombie Blueprint")]
    public class ZombieBlueprint : Blueprint
    {
        public int zombieID;
    }
}