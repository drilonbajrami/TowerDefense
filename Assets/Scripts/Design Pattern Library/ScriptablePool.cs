using UnityEngine;
using UnityEngine.Pool;

namespace TowerDefense
{
    /// <summary>
    /// Scriptable pooling class for MonoBehaviour classes (game objects).
    /// </summary>
    public abstract class ScriptablePool<T> : ScriptableObject where T : MonoBehaviour
    {
        /// <summary>
        /// Perfab.
        /// </summary>
        [SerializeField] protected T _prefab;

        /// <summary>
        /// Default pool capacity.
        /// </summary>
        [SerializeField] protected int _defaultCapacity = 100;

        /// <summary>
        /// Max size of the pool.
        /// </summary>
        [SerializeField] protected int _maxSize = 1000;

        /// <summary>
        /// Object pool.
        /// </summary>
        private ObjectPool<T> _pool;

        /// <summary>
        /// Returns the object pool.
        /// Creates a new one, if it not found.
        /// </summary>
        public ObjectPool<T> Pool {
            get {
                if (_pool == null) {
                    _pool = new ObjectPool<T>(CreatePooledObject,
                                              OnGetPooledObject,
                                              OnReleasePooledObject,
                                              OnDestroyPooledObject,
                                              collectionCheck: false,
                                              _defaultCapacity,
                                              _maxSize);
                }
                return _pool;
            }
        }

        /// <summary>
        /// Transform container for storing pooled game objects.
        /// </summary>
        protected Transform _container;

        /// <summary>
        /// Creates/spawns a game object prefab. 
        /// </summary>
        protected abstract T CreatePooledObject();

        /// <summary>
        /// Applies any logic related to the game object when retreiving it from the pool.
        /// </summary>
        protected abstract void OnGetPooledObject(T pooledObject);

        /// <summary>
        /// Applies any logic related to the game object when releasing it back to the pool.
        /// </summary>
        protected abstract void OnReleasePooledObject(T pooledObject);

        /// <summary>
        /// Applies any logic related to the game object when destroying it from the projectile pool.
        /// </summary>
        protected abstract void OnDestroyPooledObject(T pooledObject);
    }
}
