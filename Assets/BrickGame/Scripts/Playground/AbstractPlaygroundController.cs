// <copyright file="AbstractPlaygroundController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 11:57</date>

using System;
using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Playground
{
    /// <summary>
    /// AbstractPlaygroundController - abstract class for playground controller.
    /// </summary>
    public abstract class AbstractPlaygroundController : GameBehaviour
    {
        /// <summary>
        /// Enum of internal controller's states
        /// </summary>
        [Flags]
        protected enum InternalState
        {
            /// <summary>
            /// Controller was initialized
            /// </summary>
            Initialized = 0x1,
            /// <summary>
            /// Game was started
            /// </summary>
            Started = 0x2,
            /// <summary>
            /// Game on pause
            /// </summary>
            OnPause = 0x4,
            /// <summary>
            /// Game was ended
            /// </summary>
            Ended = 0x8
        }
        [Tooltip("Width of playground matrix, count of collumnts")]
        public int Width = 10;//Default by classic rules

        [Tooltip("Height of playground matrix, count of rows")]
        public int Height = 20;//Default by classic rules

        [Tooltip("Score rules of the current game")]
        public GameRules Rules;


        /// <summary>
        /// Link to Playground model
        /// </summary>
        public PlaygroundModel Model { get; private set; }
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private InternalState _state;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        public void Awake()
        {
            Context.AddListener(PlaygroundNotification.Start, NotificationHandler);
            Context.AddListener(PlaygroundNotification.Pause, NotificationHandler);
            Context.AddListener(PlaygroundNotification.End, NotificationHandler);
            _state = InternalState.Initialized;
        }

        /// <summary>
        /// Remove context listners
        /// </summary>
        private void OnDestroy()
        {
            Context.RemoveListener(PlaygroundNotification.Start, NotificationHandler);
            Context.RemoveListener(PlaygroundNotification.Pause, NotificationHandler);
            Context.RemoveListener(PlaygroundNotification.End, NotificationHandler);
        }

        /// <summary>
        ///  Handler for a playground notifications
        /// </summary>
        /// <param name="notification">Name of the notification</param>
        private void NotificationHandler(string notification)
        {
            switch (notification)
            {
                case PlaygroundNotification.Start:
                    StartGame();
                    break;
                case PlaygroundNotification.Pause:
                    PauseGame();
                    break;
                case PlaygroundNotification.End:
                    EndGame();
                    break;
            }
            //Change global state of the component
            enabled = (_state & InternalState.Started) == InternalState.Started;
            if (enabled) enabled = (_state & InternalState.OnPause) != InternalState.OnPause;
        }

        private void StartGame()
        {
            if ((_state & InternalState.Started) == InternalState.Started)
            {
                Debug.LogWarningFormat("Game already started on playground {0}", gameObject.name);
                return;
            }
            Debug.LogFormat("Game started on {0}", gameObject.name);
            SendMessage(PlaygroundMessage.UpdateModel, new PlaygroundModel(Width, Height));
            _state |= InternalState.Started;
            //Create first figure
            SendMessage(PlaygroundMessage.CreateFigure);
        }

        /// <summary>
        /// Switch between pause and resume
        /// </summary>
        private void PauseGame()
        {
            if ((_state & InternalState.OnPause) == InternalState.OnPause)
                _state ^= InternalState.OnPause;
            else
                _state |= InternalState.OnPause;
        }

        /// <summary>
        /// Clean playground from current game
        /// </summary>
        private void EndGame()
        {
            Debug.Log("Game was ended.");
            //Remove started and pause flags
            _state = InternalState.Initialized;
            Model.Reset();
        }

        private void UpdateModel(PlaygroundModel model)
        {
            Model = model;
        }
    }
}