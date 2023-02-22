using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Utilities with extension methods.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Checks if this layer mask contains the given layer.
        /// </summary>
        /// <param name="layerMask">Layer mask.</param>
        /// <param name="layer">The layer index.</param>
        /// <returns>True if this layer mask contains the layer.</returns>
        public static bool ContainsLayer(this LayerMask layerMask, int layer)
            => layerMask == (layerMask | (1 << layer));
    }
}