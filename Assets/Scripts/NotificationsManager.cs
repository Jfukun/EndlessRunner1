using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager m_Instance;

    private const string CHANNEL_ID = "tycoon_notifications_channel";

    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupNotificationsChannel();
            RequestNotificationPermissions();
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.CancelAllDisplayedNotifications();

            Send("HOLA", "Exito?", 30);

        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    private void SetupNotificationsChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = CHANNEL_ID,
            Name = "Buildings and Production",
            Importance = Importance.Default,
            Description = "Notifications for Tycoon game events about buildings and productions",


        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
    private void RequestNotificationPermissions()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

   

    public void Send(string thisTitle, string thisDescription, float delayInSeconds)
    {
        var notification = new AndroidNotification();
        notification.Title = thisTitle;
        notification.Text = thisDescription;

        notification.FireTime = System.DateTime.Now.AddSeconds(delayInSeconds);

        notification.SmallIcon = "small_Icon";
        notification.LargeIcon = "LArge_Icon";


        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
