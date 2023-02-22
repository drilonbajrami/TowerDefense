using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Scriptable object for tower blueprints.<br/>
    /// Holds all stats for different levels and monobehaviour prefab for a tower.
    /// </summary>
    [CreateAssetMenu(fileName = "Tower Blueprint", menuName = "Tower Defense/Tower Blueprint")]
    public class TowerBlueprint : ScriptableObject
    {
        /// <summary>
        /// The tower shop icon.
        /// </summary>
        [field: SerializeField] public Sprite ShopIcon { get; private set; }

        /// <summary>
        /// The tower prefab.
        /// </summary>
        [field: SerializeField] public Tower Prefab { get; private set; }

        /// <summary>
        /// Tower levels stats.
        /// </summary>
        [field: SerializeField, NonReorderable] public List<TowerStats> Levels { get; private set; } = new();

        /// <summary>
        /// Creates an tower instance from this blueprint.
        /// </summary>
        /// <returns>Tower instance.</returns>
        public Tower Spawn(int level, Vector3 spawnPosition)
        {
            if (Levels.Count == 0) {
                Debug.LogWarning($"There is no level stats set for {name} blueprint");
                return null;
            }

            level = Mathf.Clamp(level, 0, Levels.Count - 1);

            Tower towerInstance = Instantiate(Prefab, spawnPosition, Quaternion.identity, null);
            towerInstance.SetBlueprint(this);
            towerInstance.SetStats(Levels[level]);
            return towerInstance;
        }

        /// <summary>
        /// Returns the tower stats for the given level.
        /// </summary>
        /// <param name="level">Level number.</param>
        /// <returns>The stats for the selected level.<br/>
        /// <i>If level number is higher than the possible number<br/>
        /// of levels that this blueprint holds, then it will return the highest level instead.</i></returns>
        public TowerStats GetLevelStats(int level)
        {
            if (Levels == null || Levels.Count == 0) return null;
            level = Mathf.Clamp(level, 0, Levels.Count - 1);
            return Levels[level];
        }

        /// <summary>
        /// Checks if there is an available level upgrade for the given level.
        /// </summary>
        /// <param name="fromLevel">The level to upgrade from.</param>
        /// <returns></returns>
        public bool UpgradeAvailableForLevel(int fromLevel)
            => fromLevel < Levels.Count - 1;

        /// <summary>
        /// Sets the level for each set of level data based on the order they are in.
        /// </summary>
        public void OnValidate()
        {
            if (Levels.Count == 0) return;
            for (int i = 0; i < Levels.Count; i++) Levels[i].SetLevel(i);
        }
    }
}