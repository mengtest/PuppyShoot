using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class NotificationCenter 
{
    public readonly static NotificationCenter Instance = new NotificationCenter();
    private Dictionary<string,EventHandler> _eventTable=new Dictionary<string, EventHandler>();
    
    private NotificationCenter() { }

    public void PostNotification(string notificationName)
    {
        this.PostNotification(notificationName,this,EventArgs.Empty);
    }

    public void PostNotification(string notificationName, object sender)
    {
        this.PostNotification(notificationName,sender,EventArgs.Empty);
    }

    public void PostNotification(string notificationName, object sender,EventArgs e)
    {
        if (string.IsNullOrEmpty(notificationName))
        {
            Debug.LogError("A notification name is required");
            return;
        }
        if (!_eventTable.ContainsKey(notificationName))
            return;

        _eventTable[notificationName](sender, e);
    }

    public void AddObserver(string notificationName, EventHandler eventHandler)
    {
        if (string.IsNullOrEmpty(notificationName))
        {
            Debug.LogError("A notification name is required");
            return;
        }

        if (_eventTable.ContainsKey(notificationName))
        {
            Debug.Log(notificationName+" has already exist!");
            return;
        }

        _eventTable.Add(notificationName,eventHandler);
    }

    public void RemoveObserver(string notificationName)
    {
        if (string.IsNullOrEmpty(notificationName))
        {
            Debug.LogError("A notification name is required");
            return;
        }

        if (_eventTable.ContainsKey(notificationName))
            _eventTable.Remove(notificationName);
    }



}
