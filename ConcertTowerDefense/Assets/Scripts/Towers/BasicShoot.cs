using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    [SerializeField]
    private BeatMapper manager;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float projectileSpeed = 5f;

    private void Start()
    {
        if (manager != null)
        {
            RegisterOnBeatCallback(manager);
        }
    }

    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.OnBeat -= Shoot;
        }
    }

    public void RegisterOnBeatCallback(BeatMapper manager)
    {
        this.manager = manager;
        manager.OnBeat += Shoot;
    }

    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
    }
}
