﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        [System.Serializable]
        public class Event : UnityEvent<System.Object> { }

        private Dictionary<string, Event> _eventDictionary;
        private static EventManager eventManager;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public static EventManager Instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!eventManager)
                        Debug.LogError("Need 1 active EventManger script on GameObject in your scene.");
                    else
                        eventManager.Init();
                }
                return eventManager;
            }
        }

        /// <summary>
        /// init properties
        /// </summary>
        void Init()
        {
            if (_eventDictionary == null)
            {
                _eventDictionary = new Dictionary<string, Event>();
            }
        }


        /// <summary>
        /// start listening according to listener type
        /// </summary>
        /// <param name="eventName">Listener type</param>
        /// <param name="listener">listener method</param>
        public static void StartListening(string eventName, UnityAction<System.Object> listener)
        {
            Event thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new Event();
                thisEvent.AddListener(listener);
                Instance._eventDictionary.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// stop listening according to listener type
        /// </summary>
        /// <param name="eventName">Listener type</param>
        /// <param name="listener">listener method</param>
        public static void StopListening(string eventName, UnityAction<System.Object> listener)
        {
            if (Instance == null) return;
            Event thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        /// <summary>
        /// trigger event according to listener type
        /// </summary>
        /// <param name="eventName">Listener type</param>
        /// /// <param name="eventParam">from listener method's params</param>
        public static void TriggerEvent(string eventName, System.Object eventParam = null)
        {
            Event thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(eventParam);
            }
        }
    }
}