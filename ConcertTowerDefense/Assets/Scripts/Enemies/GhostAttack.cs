using UnityEngine;

[RequireComponent(typeof(GhostMovement), typeof(GhostAnimation))]
public class GhostAttack : MonoBehaviour
{
    /// <summary>
    /// Speed that the ghost attacks the stage in attacks/second
    /// </summary>
    [SerializeField]
    private float attackSpeed;

    /// <summary>
    /// The amount of damage the ghost does to the speaker
    /// /// </summary>
    [SerializeField]
    private float damage;

    private GhostMovement movement;
    private new GhostAnimation animation;

    private bool isAttacking = false;
    private StageDamagePoint currentDamagePoint;
    private float lastAttackTime;
    private float secondsPerAttack;

    private void Awake()
    {
        movement = GetComponent<GhostMovement>();
        animation = GetComponent<GhostAnimation>();
        secondsPerAttack = 1 / attackSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAttacking)
        {
            var damagePoint = other.gameObject.GetComponent<StageDamagePoint>();
            if (damagePoint != null)
            {
                isAttacking = true;
                currentDamagePoint = damagePoint;
                lastAttackTime = Time.time - secondsPerAttack;
                movement.enabled = false;
                animation.StartAttacking();
            }
        }
    }

    private void Update()
    {
        if (isAttacking)
        {
            if (Time.time - lastAttackTime > secondsPerAttack)
            {
                currentDamagePoint.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

}
