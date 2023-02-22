namespace TowerDefense
{
    /// <summary>
    /// Entities that inherit from this interface can be slowed down from debuff attacks.
    /// </summary>
    public interface ISlowDown
    {
        /// <summary>
        /// If the entity is currently slowed down.
        /// </summary>
        bool IsCurrentlySlowedDown { get; }

        /// <summary>
        /// Slows down the entity for the given time duration.
        /// </summary>
        void SlowDown(float duration);
    }
}