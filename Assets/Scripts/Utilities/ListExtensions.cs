using System;
using System.Collections.Generic;

namespace TowerDefense
{
    public static class ListExtensions
    {
        private static Random rng = new();

        /// <summary>
        /// Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <typeparam name="T">List element type.</typeparam>
        /// <param name="list">List to shuffle.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
