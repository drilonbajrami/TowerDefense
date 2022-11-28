using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    public class RareFactory : MonsterFactory
    {
        public override Zombie CreateZombie()
        {
            throw new System.NotImplementedException();
        }

        public override Goblin CreateGoblin()
        {
            throw new System.NotImplementedException();
        }

        public override Ghost CreateGhost()
        {
            throw new System.NotImplementedException();
        }
    }
}