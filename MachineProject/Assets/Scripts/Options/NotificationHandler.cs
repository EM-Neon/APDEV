using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.UI;
public class NotificationHandler : MonoBehaviour
{
    public Text dataText;
    public Slider slider;
    private void BuildDefaultNotificationChannel()
    {
        string channel_id = "default";

        string title = "Default Channel";

        Importance importance = Importance.Default;

        string description = "Default Channel for this game";

        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);
    }

    private void BuildRepeatNotificationChannel()
    {
        string channel_id = "repeat";

        string title = "Repeat Channel";

        Importance importance = Importance.Default;

        string description = "Channel for repeating notifs";

        AndroidNotificationChannel repeatChannel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(repeatChannel);
    }

    private void Awake()
    {
        BuildDefaultNotificationChannel();
        BuildRepeatNotificationChannel();
    }

    private void Start()
    {
        CheckIntentData();
    }

    public void SendSimpleNotif()
    {
        string notif_title = "Simple Notif";
        string notif_message = "This is a simple notification";

        System.DateTime fireTime = System.DateTime.Now.AddSeconds(1);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_message, fireTime);

        Debug.Log("Notification");
        AndroidNotificationCenter.SendNotification(notif, "default");
        Debug.Log($"Notification Sent {fireTime}");
    }

    public void SendRepeatNotif()
    {
        string notif_title = "Repeat Notif";
        string notif_message = "This is a repeat notification";

        System.DateTime fireTime = System.DateTime.Now.AddSeconds(slider.value);

        System.TimeSpan interval = new System.TimeSpan(0, 10, 0);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_message, fireTime, interval);

        AndroidNotificationCenter.SendNotification(notif, "repeat");
    }

    public void SendDataNotif()
    {
        string notif_title = "Data Notif";
        string notif_message = "This has data";

        System.DateTime fireTime = System.DateTime.Now.AddSeconds(slider.value);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_message, fireTime);
        notif.IntentData = "FIRE! FIRE!";
        
        AndroidNotificationCenter.SendNotification(notif, "default");
    }

    public void CheckIntentData()
    {
        AndroidNotificationIntentData data = AndroidNotificationCenter.GetLastNotificationIntent();
        if(data == null)
        {
            dataText.gameObject.SetActive(false);
        }
        else
        {
            dataText.gameObject.SetActive(true);
            dataText.text = data.Notification.IntentData;
        }
    }
}
