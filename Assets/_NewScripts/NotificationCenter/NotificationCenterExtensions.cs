using System;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public static class NotificationCenterExtensions
{
    public static void PostNotification(this object obj,string notificationName)
    {
        NotificationCenter.Instance.PostNotification(notificationName,obj);
    }

    public static void PostNotification(this object obj, string notificationName, EventArgs e)
    {
        NotificationCenter.Instance.PostNotification(notificationName,obj,e);
    }

    public static void AddObserver(this object obj, string notificationName,EventHandler eventHandler)
    {
        NotificationCenter.Instance.AddObserver(notificationName,eventHandler);
    }

    public static void RemoveObserver(this object obj, string notificationName)
    {
        NotificationCenter.Instance.RemoveObserver(notificationName);
    }
    
}
