using UnityEngine;

public class BassProjectile : ProjectileParent
{
    /// <summary>
    /// How much damage the projectile deals
    /// </summary>
    [SerializeField]
    private float damage = 5;

    /// <summary>
    /// The amount the projectile reduces the ghost's movement speed by
    /// </summary>
    [SerializeField]
    private float slowFraction = 0.2f;

    /// <summary>
    /// The duration that the projectile slows the ghost for
    /// </summary>
    [SerializeField]
    private float slowDuration = 0.5f;

    public override void Hit(GameObject ghost)
    {
        var health = ghost.GetComponent<GhostHealth>();
        var movement = ghost.GetComponent<GhostMovement>();

        health.Damage(damage);
        movement.Slow(slowFraction, slowDuration);
    }


}
