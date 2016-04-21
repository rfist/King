using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    // source http://unity3d.com/ru/learn/tutorials/modules/intermediate/live-training-archive/events-creating-simple-messaging-system
    private Dictionary<string, UnityEvent> eventDictionary;

    public static string SHOW_SCORE = "SHOW_SCORE";
    public static string HIDE_SCORE = "HIDE_SCORE";

    public static string SHOW_SCORE_TABLE = "SHOW_SCORE_TABLE";
    public static string HIDE_SCORE_TABLE = "HIDE_SCORE_TABLE";
    
    public static string SHOW_SELECT_WINDOW = "SHOW_SELECT_WINDOW";
    public static string HIDE_SELECT_WINDOW = "HIDE_SELECT_WINDOW"; 
    public static string SELECT_LEVEL = "SELECT_LEVEL";

    public static string MENU_DESELECT_ALL = "MENU_DESELECT_ALL";

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}