using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager Instance { get; private set; }

    private const string CHANNEL_ID = "runner_notifications";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            RegisterChannel();
            RequestPermission();
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RegisterChannel()
    {
        var channel = new AndroidNotificationChannel
        {
            Id = CHANNEL_ID,
            Name = "Runner Notifications",
            Importance = Importance.Default,
            Description = "Game event notifications for the Endless Runner"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void RequestPermission()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
    }

    public void SendComeBackNotification()
    {
        Send(
            title: "Ready for another run?",
            text: "Your record is waiting to be broken. Get back in there!",
            delaySeconds: 60 * 120  
        );
    }

    public void SendDailyReminder()
    {
        Send(
            title: "Daily challenge available!",
            text: "A new run awaits. How far can you go today?",
            delaySeconds: 60 * 60 * 24   
        );
    }

    private void Send(string title, string text, float delaySeconds)
    {
        var notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            FireTime = System.DateTime.Now.AddSeconds(delaySeconds),
            SmallIcon = "small_icon",
            LargeIcon = "large_icon"
        };
        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
    }
}