﻿using UnityEngine;

[RequireComponent(typeof(TowerAnimation))]
public class BeatMappedShooter : MonoBehaviour
{
    [SerializeField]
    private BeatMapper mapper;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float projectileSpeed = 5f;

    private TowerAnimation animator;

    private void Start()
    {
        animator = GetComponent<TowerAnimation>();

        if (mapper != null)
        {
            RegisterOnBeatCallback(mapper);
        }
    }

    private void OnDestroy()
    {
        if (mapper != null)
        {
            mapper.OnBeat -= Shoot;
        }
    }

    public void RegisterOnBeatCallback(BeatMapper mapper)
    {
        this.mapper = mapper;
        this.mapper.OnBeat += Shoot;
    }

    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
        animator.ShootAnimation();
    }
}
