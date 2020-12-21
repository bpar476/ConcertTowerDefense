using UnityEngine;

public abstract class ProjectileParent : MonoBehaviour
{
    private float collisionTime;

    private readonly string ENEMY_TAG = "enemy";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.fixedTime != collisionTime)
        {
            collisionTime = Time.fixedTime;
            if (other.tag == ENEMY_TAG)
            {
                Hit(other.gameObject);

                GameObject.Destroy(this.gameObject);
            }
        }
    }

    public abstract void Hit(GameObject ghost);

}
