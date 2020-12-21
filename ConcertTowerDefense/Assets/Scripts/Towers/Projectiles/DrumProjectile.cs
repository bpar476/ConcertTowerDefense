using UnityEngine;

public class DrumProjectile : ProjectileParent
{
    [SerializeField]
    private float explosionDamage;

    [SerializeField]
    private float explosionRadius;

    [SerializeField]
    private ParticleSystem explosionEffect;

    public override void Hit(GameObject ghost)
    {
        var hitPos = ghost.transform.position;
        var effect = Instantiate(explosionEffect, hitPos, Quaternion.identity);

        var system = effect.main;
        system.startSpeed = explosionRadius / system.startLifetime.constant;

        effect.Play();

        var hits = Physics2D.OverlapCircleAll(hitPos, explosionRadius);
        foreach (var hit in hits)
        {
            var health = hit.GetComponent<GhostHealth>();
            if (health != null)
            {
                health.Damage(explosionDamage);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 2);
    }

}
