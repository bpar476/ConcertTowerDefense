using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    public float StartingHealth;

    private float currentHealth;

    private AudioSource audioSource;

    private void Awake()
    {
        currentHealth = StartingHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        audioSource.Play();

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
