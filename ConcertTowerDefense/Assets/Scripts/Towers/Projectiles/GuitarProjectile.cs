using UnityEngine;

public class GuitarProjectile : ProjectileParent
{

    [SerializeField]
    private float damage;

    public override void Hit(GameObject ghost)
    {
        ghost.GetComponent<GhostHealth>().Damage(damage);
    }
}
