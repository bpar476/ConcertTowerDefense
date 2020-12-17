using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    /// <summary>
    /// Horizontal movement speed of the ghost in units per second
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1;

    /// <summary>
    /// Color to tint the character when it is under the effect of a slow
    /// </summary>
    [SerializeField]
    private Color slowTint;

    private Color originalColor;

    private SpriteRenderer spriteRenderer;

    private float currentSlow;

    private float slowDuration;

    private float slowStartTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Slows the ghost. Repeated calls to Slow do not stack however they do refresh the slow.
    /// </summary>
    /// <param name="fraction">Amount to reduce the move speed by</param>
    /// <param name="duration">Duration the slow lasts before it is removed automatically</param>
    public void Slow(float fraction, float duration)
    {
        if (currentSlow == 0)
        {
            originalColor = spriteRenderer.color;
            spriteRenderer.color *= slowTint;
        }

        currentSlow = Mathf.Max(fraction, currentSlow);
        slowDuration = Mathf.Max(duration, slowDuration);

        slowStartTime = Time.time;


    }

    private void Update()
    {
        if (currentSlow > 0 && Time.time - slowStartTime > slowDuration)
        {
            ClearSlow();
        }

        var speed = moveSpeed * (1 - currentSlow);
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
    }

    private void ClearSlow()
    {
        currentSlow = 0;
        spriteRenderer.color = originalColor;
    }
}
