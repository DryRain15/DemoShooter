using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem Instance;

    private static Dictionary <Type, List<IEventListener>> Subscription = new Dictionary<Type, List<IEventListener>>();

    private List<IEvent> publishedEvents = new List<IEvent>();
    
    public static void Subscribe(Type eventType, IEventListener subscriber)
    {
        if (!Subscription.ContainsKey(eventType))
        {
            Subscription.Add(eventType, new List<IEventListener>());
        }
        
        if (Subscription[eventType].Contains(subscriber))
        {
            Debug.LogWarning($"Listener [{subscriber}] has already subscribed [{eventType}]!");
            return;
        }
        
        Subscription[eventType].Add(subscriber);
    }

    public static void Unsubscribe(Type eventType, IEventListener subscriber)
    {
        if (!Subscription.ContainsKey(eventType))
        {
            Debug.LogWarning($"Listener [{subscriber}] has not subscribed [{eventType}] before!");
            return;
        }

        if (!Subscription[eventType].Remove(subscriber))
        {
            Debug.LogWarning($"Listener [{subscriber}] has not subscribed [{eventType}] before!");
        }
    }

    public static void Publish(IEvent e)
    {
        var type = e.GetType();

        if (!Subscription.ContainsKey(type))
            return;

        bool eventUsed = false;
        
        Instance.publishedEvents.Add(e);
    }

    public static void PublishNow(IEvent e)
    {
        var type = e.GetType();

        if (!Subscription.ContainsKey(type))
            return;

        Debug.Log(e);
        
        bool eventUsed = false;
        
        foreach (var listener in Subscription[type])
        {
            eventUsed = eventUsed || listener.OnEvent(e);
        }
    }

    public static void Send(IEventListener target, IEvent e)
    {
        target.OnEvent(e);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        foreach (var e in publishedEvents)
        {
            PublishNow(e);
        }
        
        publishedEvents.Clear();
    }
}
