using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    [SerializeField]
    private TrackManager manager;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float projectileSpeed = 5f;


    private void Start()
    {
        manager.OnBeat += Shoot;
    }

    private void OnDestroy()
    {
        manager.OnBeat -= Shoot;
    }

    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
    }
}
