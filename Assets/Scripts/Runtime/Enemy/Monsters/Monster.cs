using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{
    /// <summary>
    /// Monster base class.
    /// </summary>
    /// <typeparam name="S">Type of stats.</typeparam>
    /// <typeparam name="BP">Type of monster data.</typeparam>
    [System.Serializable]
    public abstract class Monster<S, BP> : MonoBehaviour
        where S : Stats<BP> 
        where BP : Blueprint
    { 
        /// <summary>
        /// Monster stats.
        /// </summary>
        public S _stats;

        /// <summary>
        /// Sets the stats based on the passed in monster data.
        /// </summary>
        /// <param name="data">Monster data.</param>
        public void Initialize(BP data) => _stats = GetStats(data);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract S GetStats(BP data);
    }                                           
}