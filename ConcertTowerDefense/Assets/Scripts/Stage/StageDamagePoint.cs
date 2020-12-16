﻿using System.Collections;
using UnityEngine;

/// <summary>
/// A location which can be attacked to deal damage to the stage
/// </summary>
public class StageDamagePoint : MonoBehaviour
{

    [SerializeField]
    private StageHealth stage;

    /// <summary>
    /// The number of frames that the speaker will remain in the "flash" color when it is damaged
    /// </summary>
    [SerializeField]
    private int flashFrames = 15;

    /// <summary>
    /// The radius that the speaker will tumble in when damaged
    /// </summary>
    [SerializeField]
    private float tumbleRadius = 0.05f;

    private SpriteRenderer spriteRenderer;

    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(float damage)
    {
        PlayDamageEffect();

        stage.Damage(damage);
    }

    private void PlayDamageEffect()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        spriteRenderer.color = Color.white;
        Vector2 originalPos = transform.position;

        for (int i = 0; i < flashFrames; i++)
        {
            transform.position = originalPos + (Random.insideUnitCircle * tumbleRadius);
            yield return new WaitForEndOfFrame();
        }

        transform.position = originalPos;

        spriteRenderer.color = originalColor;
    }
}
