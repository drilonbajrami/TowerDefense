using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Pooling scriptable object class for projectiles.
    /// </summary>
    /// <typeparam name="T">Type of projectile.</typeparam>
    public abstract class ProjectilePool<T> : ScriptablePool<T> where T : Projectile
    {
        /// <summary>
        /// Calls the <see cref="Pool"/>.Get() method and applies transform values to the projectile's game object transform.
        /// </summary>
        /// <returns>Projectile</returns>
        public T Get(Transform slot)
        {
            T projectile = Pool.Get();
            projectile.transform.SetParent(slot, false);
            return projectile;
        }

        /// <summary>
        /// Creates/spawns a projectile type prefab. 
        /// </summary>
        /// <returns>Projectile type.</returns>
        protected override T CreatePooledObject()
        {
            if (_container == null) _container = new GameObject($"{name} Container").GetComponent<Transform>();
            T projectile = Instantiate(_prefab, Vector3.zero, Quaternion.identity, _container.transform);
            projectile.SetPool(this);
            projectile.gameObject.SetActive(false);
            return projectile;
        }

        /// <summary>
        /// Applies any logic related to the projectile when retreiving it from the projectile pool.
        /// </summary>
        /// <param name="projectile">Projectile type.</param>
        protected override void OnGetPooledObject(T projectile)
        {
            projectile.ResetState();
            projectile.gameObject.SetActive(true);
        }

        /// <summary>
        /// Applies any logic related to the projectile when releasing it to the projectile pool.
        /// </summary>
        /// <param name="projectile">Projectile type.</param>
        protected override void OnReleasePooledObject(T projectile)
        {
            projectile.ResetState();
            projectile.gameObject.SetActive(false);
            projectile.gameObject.transform.SetParent(_container);
        }

        /// <summary>
        /// Applies any logic related to the projectile when destroying it from the projectile pool.
        /// </summary>
        /// <param name="projectile">Projectile type.</param>
        protected override void OnDestroyPooledObject(T projectile) => Destroy(projectile.gameObject);
    }
}