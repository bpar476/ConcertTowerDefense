using UnityEngine;

public class BassProjectile : ProjectileParent
{
    public override void Hit(GameObject ghost)
    {
        var health = ghost.GetComponent<GhostHealth>();
        var movement = ghost.GetComponent<GhostMovement>();

        health.Damage(5);
        movement.Slow(0.2f, 0.5f);
    }


}
