using System.Collections.Generic;
using TowerDefense.Enemy;
using UnityEngine;

namespace TowerDefense.EnemyWaves
{
    /// <summary>
    /// Wave for aggressive enemies.
    /// </summary>
    [System.Serializable]
    public class WaveEnemyAggressive : WaveEnemy<AggressiveEnemyBlueprint, AggressiveEnemy, AggressiveEnemyStats> { }

    /// <summary>
    /// Waves for passive enemies.
    /// </summary>
    [System.Serializable]
    public class WaveEnemyPassive : WaveEnemy<PassiveEnemyBlueprint, PassiveEnemy, PassiveEnemyStats> { }

    /// <summary>
    /// Wave enemy with the blueprint type and spawn chances for each level, used only for the editor.
    /// </summary>
    [System.Serializable]
    public class WaveEnemy<T, A, S>
        where T : EnemyBlueprint<A, S>
        where A : EnemyActor<A, S>
        where S : EnemyStats
    {
        /// <summary>
        /// The enemy blueprint.
        /// </summary>
        [SerializeField]
        private T _blueprint;

        /// <summary>
        /// The amount per each given level of the blueprint.
        /// The index of the amount in the list is the index of the level within the blueprint.
        /// </summary>
        [SerializeField, NonReorderable]
        private List<int> _amountsPerLvl;

        /// <summary>
        /// Returns a list of amounts per each level.
        /// The number/index of the levels is repeated n times (n = amount).<br/>
        /// Example: { 0, 0, 0, 1, 1, 3, 3, 3, 3, ....} <br/>
        ///     Level 0 - 3 times<br/>
        ///     Level 1 - 2 times<br/>
        ///     Level 3 - 4 times, ... and so on.<br/>
        /// </summary>
        public List<int> GetAllLevelAmounts()
        {
            List<int> lvls = new();
            for (int i = 0; i < _amountsPerLvl.Count; i++) {
                int amount = _amountsPerLvl[i];
                for (int j = 0; j < amount; j++)
                    lvls.Add(i);
            }
            return lvls;
        }

        /// <summary>
        /// Spawns an instance of the blueprint enemy based on the given level.
        /// </summary>
        public A Spawn(int lvlIndex, Vector3 spawnPosition)
            => _blueprint.Spawn(lvlIndex, spawnPosition);

        /// <summary>
        /// Checks if the enemy blueprint's levels list is populated, if not it will populate it
        /// accordingly to the number of levels the given enemy blueprint has.
        /// </summary>
        public void ValidateData()
        {
            PopulateLevels();
            ClampNumberOfLevels();
            ClampAmountsPerLevel();
        }

        private void PopulateLevels()
        {
            if (_blueprint == null) return;
            if (_amountsPerLvl != null) return;
            _amountsPerLvl = new();
            for (int i = 0; i < _blueprint.Levels.Count; i++)
                _amountsPerLvl.Add(0);
        }

        private void ClampNumberOfLevels()
        {
            if (_blueprint == null || _amountsPerLvl == null) return;
            if (_amountsPerLvl.Count == _blueprint.Levels.Count) return;
            _amountsPerLvl.Clear();
            for (int i = 0; i < _blueprint.Levels.Count; i++)
                _amountsPerLvl.Add(0);
        }

        private void ClampAmountsPerLevel()
        {
            if (_amountsPerLvl != null)
                for (int i = 0; i < _amountsPerLvl.Count; i++)
                    _amountsPerLvl[i] = Mathf.Clamp(_amountsPerLvl[i], 0, 100);
        }
    }
}
