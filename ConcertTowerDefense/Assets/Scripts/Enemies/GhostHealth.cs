using System.Collections;
using UnityEngine;

public class GhostHealth : MonoBehaviour
{

    private static readonly string MATERIAL_PARAM_FLASH = "_FlashAmount";

    /// <summary>
    /// The amount of currency the player gets when this ghost is destroyed
    /// </summary>
    [SerializeField]
    private int bounty = 2;

    public float StartingHealth;

    private float currentHealth;

    private AudioSource audioSource;
    private Material material;

    private void Awake()
    {
        currentHealth = StartingHealth;
        audioSource = GetComponent<AudioSource>();
        material = GetComponentInChildren<SpriteRenderer>().material;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage * DamageMultiplierForBandLevel(BandProgression.Instance.BandLevel);
        audioSource.Play();
        material.SetFloat(MATERIAL_PARAM_FLASH, 1.0f);

        if (currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(StopFlashAfterSoundEnds());
    }

    private void Die()
    {
        TowerCurrency.Instance.AddCurrency(bounty);
        Destroy(this.gameObject);
    }

    public static float DamageMultiplierForBandLevel(int bandLevel)
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

    private IEnumerator StopFlashAfterSoundEnds()
    {
        yield return new WaitForSeconds(0.2f);

        material.SetFloat(MATERIAL_PARAM_FLASH, 0);
    }

}
