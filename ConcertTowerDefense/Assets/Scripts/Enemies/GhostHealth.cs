using UnityEngine;

public class GhostHealth : MonoBehaviour
{

    /// <summary>
    /// The amount of currency the player gets when this ghost is destroyed
    /// </summary>
    [SerializeField]
    private int bounty = 2;

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
        currentHealth -= damage * damageMultiplierForBandLevel(BandProgression.Instance.BandLevel);
        audioSource.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        TowerCurrency.Instance.AddCurrency(bounty);
        Destroy(this.gameObject);
    }

    private static float damageMultiplierForBandLevel(int bandLevel)
    {
        switch (bandLevel)
        {
            case 2:
                return 1.5f;
            case 3:
                return 2f;
            default:
                return 1f;
        }
    }

}
