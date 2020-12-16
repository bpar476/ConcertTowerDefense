using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StageHealth : MonoBehaviour
{
    public float Strength { get; private set; }

    [SerializeField]
    private TMPro.TMP_Text textUi;

    [SerializeField]
    private AudioClip[] damageSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Strength = 100;
        UpdateTextUI();
    }

    public void Damage(float damage)
    {
        Strength -= damage;

        UpdateTextUI();

        PlayDamageSound();

        if (Strength <= 0)
        {
            GameOver();
        }
    }

    private void UpdateTextUI()
    {
        textUi.text = string.Format("{0}%", Mathf.Max(Strength, 0));
    }

    private void PlayDamageSound()
    {
        var clip = damageSounds[Random.Range(0, damageSounds.Length)];
        audioSource.PlayOneShot(clip);
    }

    private void GameOver()
    {
        Debug.Log("Stage was destroyed");
    }
}

