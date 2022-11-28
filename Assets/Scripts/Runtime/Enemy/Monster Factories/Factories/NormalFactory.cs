using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    public class NormalFactory : MonsterFactory
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                CreateZombie();
            else if (Input.GetKeyDown(KeyCode.S))
                CreateGoblin();
            else if (Input.GetKeyDown(KeyCode.D))
                CreateGhost();
        }
    }
}
