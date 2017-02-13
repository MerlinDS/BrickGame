// <copyright file="NotificationDispatcher.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 11:04</date>

using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
#else
using System.Diagnostics;
#endif

namespace MiniMoca
{
    /// <summary>
    /// NotificationDispatcher
    /// </summary>
    public class NotificationDispatcher
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private const string AddWarning = "Listener from target {0} for notification {1} already registered!";
        private const string RemoveWarning = "Listener from target {0} for notification {1} wasn't registered!";

        private readonly Dictionary<string, List<Delegate>> _listeners;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public NotificationDispatcher()
        {
            _listeners = new Dictionary<string, List<Delegate>>();
        }
        /// <summary>
        /// Check if listener was added to dispatcher
        /// </summary>
        /// <param name="key">notification</param>
        /// <param name="delegate">listener</param>
        /// <returns>True if was added, false in other case</returns>
        public bool Has(string key, Delegate @delegate)
        {
            List<Delegate> delegates;
            if (!_listeners.TryGetValue(key, out delegates)) return false;
            return delegates.Contains(@delegate);
        }

        /// <summary>
        /// Add listener for notification
        /// </summary>
        /// <param name="key">Notification name</param>
        /// <param name="delegate">Listener action</param>
        public void Add(string key, Delegate @delegate)
        {
            List<Delegate> delegates;
            if (!_listeners.TryGetValue(key, out delegates))
            {
                //such key was not registered yet
                delegates = new List<Delegate>();
                _listeners.Add(key, delegates);
            }

            if (delegates.Contains(@delegate))
            {
#if UNITY_EDITOR
                Debug.LogWarningFormat(AddWarning, @delegate.Target.ToString(), key);
#else
                Debug.WriteLine(AddWarning, @delegate.Target.ToString(), key);
#endif
                return;
            }

            delegates.Add(@delegate);
        }

        /// <summary>
        /// Removal listener from notification
        /// </summary>
        /// <param name="key">Notification name</param>
        /// <param name="delegate">Listener action</param>
        public void Remove(string key, Delegate @delegate)
        {
            if (!Has(key, @delegate))
            {
#if UNITY_EDITOR
                Debug.LogWarningFormat(RemoveWarning, @delegate.Target.ToString(), key);
#else
                Debug.WriteLine(RemoveWarning, @delegate.Target.ToString(), key);
#endif
                return;
            }
            List<Delegate> delegates = _listeners[key];
            delegates.Remove(@delegate);
        }
        /// <summary>
        /// Send notification to observers
        /// </summary>
        /// <param name="sender">Notification sender</param>
        /// <param name="notification">Notification name</param>
        /// <param name="dataProvider">Notification data provider</param>
        public void Notify(MocaContext sender, string notification, DataProvider dataProvider)
        {
            List<Delegate> delegates;
            if (!_listeners.TryGetValue(notification, out delegates))
                return;//No listener
            object[] parameters;
            int i, n = delegates.Count;
            for (i = 0; i < n; i++)
            {
                Delegate @delegate = delegates[i];
                int paramLength = @delegate.Method.GetParameters().Length;
                //Detecting type of listener's handler
                if (paramLength == 1) parameters = new object[] {notification};
                else if(paramLength == 2) parameters = new object[] {notification, dataProvider};
                else if (paramLength == 3) parameters = new object[] {sender, notification, dataProvider};
                else throw new Exception("Can't detect type of listener handler!");
                //Invoke handler
                try
                {
                    @delegate.Method.Invoke(@delegate.Target, parameters);
                }
                catch (Exception e)
                {
#if UNITY_EDITOR
                Debug.LogError(e);
#else
                Debug.WriteLine(e);
#endif
                }
            }
        }



        //================================ Private|Protected methods ================================
    }
}