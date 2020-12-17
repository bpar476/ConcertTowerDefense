using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    /// <summary>
    /// Horizontal movement speed of the ghost in units per second
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1;

    private float currentSlow;

    private float slowDuration;

    private float slowStartTime;

    /// <summary>
    /// Slows the ghost. Repeated calls to Slow do not stack however they do refresh the slow.
    /// </summary>
    /// <param name="fraction">Amount to reduce the move speed by</param>
    /// <param name="duration">Duration the slow lasts before it is removed automatically</param>
    public void Slow(float fraction, float duration)
    {
        currentSlow = Mathf.Max(fraction, currentSlow);
        slowDuration = Mathf.Max(duration, slowDuration);

        slowStartTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - slowStartTime > slowDuration)
        {
            currentSlow = 0;
        }

        var speed = moveSpeed * (1 - currentSlow);
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
