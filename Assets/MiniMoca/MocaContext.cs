// <copyright file="MocaContext.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 9:23</date>

using System;

namespace MiniMoca
{
    /// <summary>
    /// MocaContext - abstract context, facade for accessing to MiniMoca public methods
    /// </summary>
    public abstract class MocaContext
    {
        //================================       Public Setup       =================================
        /// <summary>
        /// Validity internal flag
        /// </summary>
        // ReSharper disable once InconsistentNaming
        internal bool _isValid;
        /// <summary>
        /// Validity flag of current context
        /// </summary>
        public bool IsValid{ get { return _isValid; } }
        //================================    Systems properties    =================================
        /// <inheritdoc />
        protected readonly CommandMap CommandMap;
        /// <inheritdoc />
        protected readonly ActorMap ActorMap;
        /// <inheritdoc />
        protected readonly NotificationDispatcher NoticationDispatcher;
        //================================      Public methods      =================================

        //Actors
        /// <inheritdoc />
        public void MapInstance(IMocaActor actor, Type asType)
        {
            ActorMap.MapInstance(actor, asType);
        }

        /// <inheritdoc />
        public void MapInstance(IMocaActor actor)
        {
            ActorMap.MapInstance(actor);
        }

        /// <inheritdoc />
        public void MapInstance(Type actorType)
        {
            ActorMap.MapInstance(actorType);
        }

        /// <inheritdoc />
        public void MapInstance<T>() where T : IMocaActor
        {
            ActorMap.MapInstance<T>();
        }

        /// <inheritdoc />
        public IMocaActor Instantiate(Type actorType)
        {
            return ActorMap.Instantiate(actorType);
        }

        /// <inheritdoc />
        public IMocaActor GetActor(Type actorType)
        {
            return ActorMap.GetActor(actorType);
        }

        /// <inheritdoc />
        public T GetActor<T>() where T : IMocaActor
        {
            return ActorMap.GetActor<T>();
        }

        /// <inheritdoc />
        public bool HasActor(Type type)
        {
            return ActorMap.HasActor(type);
        }

        public bool HasActor(IMocaActor instance)
        {
            return ActorMap.HasActor(instance.GetType());
        }


        //-------------------------- Notifications
        /// <summary>
        ///
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool HasCommand(string notification)
        {
            return CommandMap.HasMapping(notification);
        }

        /// <summary>
        /// Dispatch notification to context and propagate it to an observers
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="data">DataProvider for notification</param>
        public void Notify(string notification, DataProvider data = null)
        {
            if(CommandMap.HasMapping(notification))
                CommandMap.Execute(notification, data);
            NoticationDispatcher.Notify(this, notification, data);
        }

        /// <summary>
        /// Add listener for notification with data provider & sender instance
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        public void AddListener(string notification, Action<string> listener)
        {
            NoticationDispatcher.Add(notification, listener);
        }

        /// <summary>
        /// Add listener for notification with data provider & sender instance
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        /// <typeparam name="T">data provider type, must inherited DataProvider class</typeparam>
        public void AddListener<T>(string notification, Action<string, T> listener) where T : DataProvider
        {
            NoticationDispatcher.Add(notification, listener);
        }

        /// <summary>
        /// Add listener for notification with data provider & sender instance
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        /// <typeparam name="T">data provider type, must inherited DataProvider class</typeparam>
        /// <typeparam name="T1">sender instance type must be inherited from MocaContext</typeparam>
        public void AddListener<T, T1>(string notification, Action<T, string, T1> listener) where T : MocaContext where T1 : DataProvider
        {
            NoticationDispatcher.Add(notification, listener);
        }

        /// <summary>
        /// Check if listener was added to dispatcher
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        public bool HasListener(string notification, Action<string> listener)
        {
            return NoticationDispatcher.Has(notification, listener);
        }

        /// <summary>
        /// Check if listener was added to dispatcher
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        /// <typeparam name="T">data provider type, must inherited DataProvider class</typeparam>
        public bool HasListener<T>(string notification, Action<string, T> listener) where T : DataProvider
        {
            return NoticationDispatcher.Has(notification, listener);
        }

        /// <summary>
        /// Check if listener was added to dispatcher
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        /// <typeparam name="T">data provider type, must inherited DataProvider class</typeparam>
        /// <typeparam name="T1">sender instance type must be inherited from MocaContext</typeparam>
        public bool HasListener<T, T1>(string notification, Action<T, string, T1> listener) where T : MocaContext where T1 : DataProvider
        {
            return NoticationDispatcher.Has(notification, listener);
        }

        /// <summary>
        /// Removal listener from notification
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        public void RemoveListener(string notification, Action<string> listener)
        {
            NoticationDispatcher.Remove(notification, listener);
        }

        /// <summary>
        /// Removal listener from notification
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        /// <typeparam name="T">data provider type, must inherited DataProvider class</typeparam>
        public void RemoveListener<T>(string notification, Action<string, T> listener) where T : DataProvider
        {
            NoticationDispatcher.Remove(notification, listener);
        }

        /// <summary>
        /// Removal listener from notification
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="listener">Listener action with data provider</param>
        /// <typeparam name="T">data provider type, must inherited DataProvider class</typeparam>
        /// <typeparam name="T1">sender instance type must be inherited from MocaContext</typeparam>
        public void RemoveListener<T, T1>(string notification, Action<T, string, T1> listener) where T : MocaContext where T1 : DataProvider
        {
            NoticationDispatcher.Remove(notification, listener);
        }

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected MocaContext()
        {
            ActorMap = new ActorMap();
            CommandMap = new CommandMap();
            NoticationDispatcher = new NotificationDispatcher();
        }
        /// <summary>
        /// Initialize concrete context
        /// </summary>
        protected internal abstract void Initialize();
    }
}