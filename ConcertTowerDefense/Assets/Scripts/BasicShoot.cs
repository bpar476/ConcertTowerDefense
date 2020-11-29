using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float projectileSpeed = 5f;

    private void Start()
    {
        BeatManager.Instance.OnEigth += Shoot;
    }

    private void OnDestroy()
    {
        BeatManager.Instance.OnEigth -= Shoot;
    }

    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
    }
}
