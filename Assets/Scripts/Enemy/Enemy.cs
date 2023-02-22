using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Enemy
{
    /// <summary>
    /// Abstract base MonoBehaviour class for an enemy actor.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent), typeof(Health), typeof(Money))]
    public abstract class EnemyActor<A, S> : MonoBehaviour, IDamageable, ISlowDown
        where A : EnemyActor<A, S>
        where S : EnemyStats
    {
        /// <summary>
        /// Reference to this enemy's blueprint.
        /// </summary>  
        public EnemyBlueprint<A, S> Blueprint { get; private set; }

        /// <summary>
        /// Cached stats.
        /// </summary>
        public S Stats { get; private set; }

        // Components
        [field: SerializeField] public Health Health { get; protected set; }
        [field: SerializeField] public NavMeshAgent Agent { get; protected set; }
        [field: SerializeField] public Money Money { get; protected set; }
        [field: SerializeField] public Collider Collider { get; protected set; }

        // Events
        [field: SerializeField] public GameEventInt OnDeathMoneyEvent { get; protected set; }
        [field: SerializeField] public GameEvent OnDeathEvent { get; protected set; }
        [field: SerializeField] public GameEventIntAndVector3 OnDeathMoneyAndPositionEvent { get; protected set; }

        /// <summary>
        /// Sets a reference to the blueprint used to create this enemy.
        /// </summary>
        public void SetBlueprint(EnemyBlueprint<A, S> blueprint) => Blueprint = blueprint;

        /// <summary>
        /// Sets the data for this enemy.<br/>
        /// <i><b>Warning</b>: If you override this method, make sure to call the base method as well:</i><br/>
        /// base.<i><b>SetStats(S stats)</b></i>
        /// </summary>
        public virtual void SetStats(S stats)
        {
            Stats = stats;
            Agent.speed = stats.MaxSpeed;
            Health.SetHP(stats.HP);
            Money.SetAmount(stats.Money);
        }

        /// <summary>
        /// Sets the destination for the enemy's navmesh agent.
        /// </summary>
        public void SetDestination(Vector3 destination)
            => Agent.SetDestination(destination);

        /// <summary>
        /// Raises all related events when this enemy dies.
        /// </summary>
        public void OnDestroy()
        {
            OnDeathMoneyAndPositionEvent.Raise(Money.Amount, transform.position);
            OnDeathMoneyEvent.Raise(Money.Amount);
            OnDeathEvent.Raise();
        }

        /// <summary>
        /// Takes damage to its health component.
        /// </summary>
        public void TakeDamage(float damage) => Health.DepleteHP(damage);

        // ISlowDown methods.
        public bool IsCurrentlySlowedDown { get; private set; }

        /// <summary>
        /// Slows down enemy for the given duration.
        /// </summary>
        public void SlowDown(float duration)
        {
            if (IsCurrentlySlowedDown) return;
            Agent.speed = Stats.MinSpeed;
            StartCoroutine(SlowDownTimer(duration));
        }

        /// <summary>
        /// Slowdown coroutine.
        /// </summary>
        private IEnumerator SlowDownTimer(float duration)
        {
            yield return new WaitForSeconds(duration);
            IsCurrentlySlowedDown = false;
            Agent.speed = Stats.MaxSpeed;
        }
    }
}