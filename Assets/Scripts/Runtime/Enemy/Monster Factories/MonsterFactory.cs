using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    public abstract class MonsterFactory : MonoBehaviour
    {
        [SerializeField] protected Zombie _zombiePrefab;
        [SerializeField] protected Goblin _goblinPrefab;
        [SerializeField] protected Ghost _ghostPrefab;

        [SerializeField] protected ZombieBlueprint _zombieBlueprint;
        [SerializeField] protected GhostBlueprint _ghostBlueprint;
        [SerializeField] protected GoblinBlueprint _goblinBlueprint;

        public virtual Zombie CreateZombie()
        {
            Zombie zombie = Instantiate(_zombiePrefab, Vector3.zero, Quaternion.identity);
            zombie.Initialize(_zombieBlueprint);
            zombie.gameObject.transform.position = Random.insideUnitSphere;
            return zombie;
        }

        public virtual Goblin CreateGoblin()
        {
            Goblin goblin = Instantiate(_goblinPrefab, Vector3.zero, Quaternion.identity);
            goblin.Initialize(_goblinBlueprint);
            goblin.gameObject.transform.position = Random.insideUnitSphere;
            return goblin;
        }

        public virtual Ghost CreateGhost()
        {
            Ghost ghost = Instantiate(_ghostPrefab, Vector3.zero, Quaternion.identity);
            ghost.Initialize(_ghostBlueprint);
            ghost.gameObject.transform.position = Random.insideUnitSphere;
            return ghost;
        }
    }
}