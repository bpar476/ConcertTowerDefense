using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    public float StartingHealth;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = StartingHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

}
