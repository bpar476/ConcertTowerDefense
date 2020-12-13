using UnityEngine;


/// <summary>
/// Test class which queues three notifications on start. If the notification system is working properly, it 
/// should play all three notifications, one after the other.
/// </summary>
public class TestNotifications : MonoBehaviour
{
    public NotificationSystem notification;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < 3; i++)
        {
            notification.PublishNotification(string.Format("Notification {0}", i));
        }
    }
}
