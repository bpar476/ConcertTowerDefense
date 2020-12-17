using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    private static readonly string ANIM_TRIGGER_FADEIN = "fadeIn";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void FadeIn()
    {
        animator.SetTrigger(ANIM_TRIGGER_FADEIN);
    }
}
