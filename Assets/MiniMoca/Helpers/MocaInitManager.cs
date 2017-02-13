// <copyright file="MocaInitManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 12:12</date>

using System;
using UnityEngine;

namespace MiniMoca.Helpers
{
    /// <summary>
    /// MocaInitManager - MiniMoca initialization helper.
    /// This class is a Unity sigleton without public access.
    /// Add this script to empty <code>GameObject</code>..
    /// <para>
    /// Also need to add script to the top of the Script Execution Order Settings
    /// </para>
    /// <seealso cref="https://docs.unity3d.com/Manual/class-ScriptExecution.html"/>
    /// </summary>
    public abstract class MocaInitManager : MonoBehaviour
    {
        //================================       Public Setup       =================================
        //================================    Systems properties    =================================
        private static MocaInitManager _instance;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void Awake()
        {

            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                //avoiding double initialization
                if (!ContextProvider.HasContexts)
                    Initialization();
            }
            else if (_instance != this)
                Destroy(gameObject);
        }

        private void Initialization()
        {
            try
            {
                //Initialize concrete contexts
                Initialize();
            }
            catch (Exception e)
            {
                Debug.LogError("MiniMoca:" + e.Message);
            }
            //Check initialization result
            if (!ContextProvider.HasContexts)
            {
                Debug.LogError("MiniMoca: No context wasn't registered!");
                return;
            }
            Debug.Log("MiniMoca: Initialization complete");
        }
        /// <summary>
        /// Register new context in context provider
        /// </summary>
        /// <param name="context">Instance of context provider</param>
        protected void RegisterContext(MocaContext context)
        {
            IMocaActor[] actors = GetComponents<IMocaActor>();
            foreach (IMocaActor actor in actors)
                context.MapInstance(actor);
            ContextProvider.Register(context);
        }
        /// <summary>
        /// Initialize concrete contexts
        /// </summary>
        protected abstract void Initialize();
    }
}