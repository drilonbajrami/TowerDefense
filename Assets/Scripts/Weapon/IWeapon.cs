using UnityEngine;

namespace TowerDefense.Weapons
{
    /// <summary>
    /// Weapon interface.
    /// </summary>
    public interface IWeapon
    {
        /// <summary>
        /// Rate of fire in rounds per minute.
        /// </summary>
        int RPM { get; }

        /// <summary>
        /// Cooldown time until next shot.
        /// </summary>
        float CooldownTime { get; }

        /// <summary>
        /// Keeps tracks of when the weapon shoots.
        /// </summary>
        bool HasFired { get; }

        /// <summary>
        /// Damage points for the weapon's projectiles.
        /// </summary>
        float DamagePoints { get; set; }

        /// <summary>
        /// Damage type for the weapon's projectiles.
        /// </summary>
        DamageType DamageType { get; }

        /// <summary>
        /// Aims at the given target position.
        /// </summary>
        /// <param name="targetPosition"></param>
        void AimAt(Vector3 targetPosition);

        /// <summary>
        /// Shoots the weapon.
        /// </summary>
        void Shoot();

        /// <summary>
        /// Reloads the weapon.
        /// </summary>
        void Reload();

        /// <summary>
        /// Sets the rate of fire.
        /// </summary>
        /// <param name="rpm">Rounds per minute.</param>
        void SetRPM(int rpm);
    }
}