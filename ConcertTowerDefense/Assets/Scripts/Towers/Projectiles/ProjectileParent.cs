using UnityEngine;

public abstract class ProjectileParent : MonoBehaviour
{
    private readonly string ENEMY_TAG = "enemy";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ENEMY_TAG)
        {
            Hit(other.gameObject);

            GameObject.Destroy(this);
        }
    }

    public abstract void Hit(GameObject ghost);

}
