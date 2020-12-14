using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    /// <summary>
    /// Horizontal movement speed of the ghost in units per second
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1;

    private void Update()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }
}
