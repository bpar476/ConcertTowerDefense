using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimation : MonoBehaviour
{
    private static readonly string ANIM_TRIGGER_ATTACK = "attack";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAttacking()
    {
        animator.SetTrigger(ANIM_TRIGGER_ATTACK);
    }
}
