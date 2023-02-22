using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemy
{
    /// <summary>
    /// Abstract class for enemy blueprint as a scriptable object.<br/>
    /// Holds all stats for different levels and actor/monobehaviour prefab for an enemy.
    /// </summary>
    public abstract class EnemyBlueprint<A, S> : ScriptableObject
        where A : EnemyActor<A, S>
        where S : EnemyStats
    {
        /// <summary>
        /// Enemy actor prefab.
        /// </summary>
        [field: SerializeField] public A Actor { get; protected set; }

        /// <summary>
        /// Enemy levels stats.
        /// </summary>
        [field: SerializeField, NonReorderable] public List<S> Levels { get; protected set; } = new();

        /// <summary>
        /// Creates an enemy instance from this blueprint.
        /// </summary>
        /// <returns>Enemy instance.</returns>
        public A Spawn(int level, Vector3 spawnPosition)
        {
            if (Levels.Count == 0) {
                Debug.LogWarning($"There is no level stats set for {name} enemy blueprint");
                return null;
            }

            level = Mathf.Clamp(level, 0, Levels.Count - 1);

            A enemyInstance = Instantiate(Actor, spawnPosition, Quaternion.identity, null);
            enemyInstance.SetBlueprint(this);
            enemyInstance.SetStats(Levels[level]);
            return enemyInstance;
        }

        /// <summary>
        /// Returns the enemy stats for the given level.
        /// </summary>
        /// <param name="level">Level number.</param>
        /// <returns>The stats for the selected level.<br/>
        /// <i>If level number is higher than the number of levels this<br/>
        ///  blueprint holds, then it will return the highest level instead.</i></returns>
        public S GetLevelStats(int level)
        {
            if (Levels == null || Levels.Count == 0) return null;
            level = Mathf.Clamp(level, 0, Levels.Count);
            return Levels[level];
        }

        /// <summary>
        /// Sets the level for each set of level data based on the order they are in.
        /// </summary>
        private void OnValidate()
        {
            if (Levels.Count == 0) return;
            for (int i = 0; i < Levels.Count; i++) Levels[i].SetLevel(i);
        }
    }
}