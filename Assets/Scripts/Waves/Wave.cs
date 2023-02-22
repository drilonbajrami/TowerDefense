using System.Collections.Generic;
using TowerDefense.Enemy;
using UnityEngine;

namespace TowerDefense.EnemyWaves
{
    /// <summary>
    /// Enemy wave scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "Enemy Wave", menuName = "Tower Defense/Enemy Wave")]
    public class Wave : ScriptableObject
    {
        [field: SerializeField, Range(0f, 10f)] public float SpawnDelay { get; private set; }

        [Header("Aggressive Enemies")]
        [SerializeField, NonReorderable] private List<WaveEnemyAggressive> aggressiveEnemies = new();

        [Space(10f)]
        [Header("Passive Enemies")]
        [SerializeField, NonReorderable] private List<WaveEnemyPassive> passiveEnemies = new();

        // Queues for spawning.
        private Queue<(int enemyBPIndex, int level)> _aggressiveEnemyQueue;
        private Queue<(int enemyBPIndex, int level)> _passiveEnemyQueue;

        /// <summary>
        /// Checks if all enemy spawning queues are empty or not.
        /// </summary>
        public bool HasSpawnedAllEnemies => _aggressiveEnemyQueue.Count == 0 && _passiveEnemyQueue.Count == 0;

        /// <summary>
        /// Total number of the enemies to be spawned by this wave.
        /// </summary>
        public int TotalCountOfEnemies => _aggressiveEnemyQueue.Count + _passiveEnemyQueue.Count;

        /// <summary>
        /// Spawns an enemy from the wave spawn queues (aggressive & passive enemies).
        /// </summary>
        /// <param name="spawnPosition">Spawn position.</param>
        /// <param name="destination">Destination of the enemy.</param>
        /// <returns>True if there are still enemies in the spawn queue, else false.</returns>
        public GameObject Spawn(Vector3 spawnPosition, Vector3 destination)
        {
            int i = Random.Range(0, 2);

            if (_aggressiveEnemyQueue.Count > 0 && i == 0) {
                (int bpIndex, int lvlIndex) e = _aggressiveEnemyQueue.Dequeue();
                AggressiveEnemy enemy = aggressiveEnemies[e.bpIndex].Spawn(e.lvlIndex, spawnPosition);
                enemy.SetDestination(destination);
                return enemy.gameObject;
            }
            else if (_passiveEnemyQueue.Count > 0) {
                (int bpIndex, int lvlIndex) e = _passiveEnemyQueue.Dequeue();
                PassiveEnemy enemy = passiveEnemies[e.bpIndex].Spawn(e.lvlIndex, spawnPosition);
                enemy.SetDestination(destination);
                return enemy.gameObject;
            }

            return null;
        }

        /// <summary>
        /// Initializes and setups all wave settings for gameplay.
        /// </summary>
        public void InitializeWave()
        {
            SetupAggressiveEnemyWave();
            SetupPassiveEnemyWave();
        }

        /// <summary>
        /// Stores all the aggressive enemy wave stats into a shuffled queue for spawning.
        /// </summary>
        private void SetupAggressiveEnemyWave()
        {
            if (aggressiveEnemies == null) return;

            List<(int enemyBPIndex, int level)> enemyAmounts = new();

            for (int bpIndex = 0; bpIndex < aggressiveEnemies.Count; bpIndex++) {
                List<int> lvlAmounts = aggressiveEnemies[bpIndex].GetAllLevelAmounts();
                if (lvlAmounts.Count == 0) continue;
                for (int j = 0; j < lvlAmounts.Count; j++)
                    enemyAmounts.Add((bpIndex, lvlAmounts[j]));
            }

            enemyAmounts.Shuffle();
            _aggressiveEnemyQueue = new();
            for (int i = 0; i < enemyAmounts.Count; i++)
                _aggressiveEnemyQueue.Enqueue(enemyAmounts[i]);
        }

        /// <summary>
        /// Stores all the passive enemy wave stats into a shuffled queue for spawning.
        /// </summary>
        private void SetupPassiveEnemyWave()
        {
            if (passiveEnemies == null) return;

            List<(int enemyBPIndex, int level)> enemyAmounts = new();

            for (int bpIndex = 0; bpIndex < passiveEnemies.Count; bpIndex++) {
                List<int> lvlAmounts = passiveEnemies[bpIndex].GetAllLevelAmounts();
                if (lvlAmounts.Count == 0) continue;
                for (int j = 0; j < lvlAmounts.Count; j++)
                    enemyAmounts.Add((bpIndex, lvlAmounts[j]));
            }

            enemyAmounts.Shuffle();
            _passiveEnemyQueue = new();
            for (int i = 0; i < enemyAmounts.Count; i++)
                _passiveEnemyQueue.Enqueue(enemyAmounts[i]);
        }

        /// <summary>
        /// Validates all wave settings.
        /// </summary>
        private void OnValidate()
        {
            if (aggressiveEnemies != null)
                foreach (WaveEnemyAggressive a in aggressiveEnemies) a.ValidateData();

            if (passiveEnemies != null)
                foreach (WaveEnemyPassive a in passiveEnemies) a.ValidateData();
        }
    }
}