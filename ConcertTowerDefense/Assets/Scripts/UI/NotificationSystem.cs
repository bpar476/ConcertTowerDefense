using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    private static readonly string ANIM_TRIGGER_NOTIFY = "Notify";
    private static readonly string ANIM_STATE_IDLE = "Idle";

    private Animator animator;
    private TMPro.TMP_Text textUI;

    private Queue<string> notificationQueue;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        textUI = GetComponent<TMPro.TMP_Text>();
        notificationQueue = new Queue<string>();
    }

    public void PublishNotification(string notificationText)
    {
        notificationQueue.Enqueue(notificationText);
    }

    private void Update()
    {
        if (notificationQueue.Count > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName(ANIM_STATE_IDLE))
        {
            var text = notificationQueue.Dequeue();
            textUI.text = text;

            animator.SetTrigger(ANIM_TRIGGER_NOTIFY);
        }
    }
}
