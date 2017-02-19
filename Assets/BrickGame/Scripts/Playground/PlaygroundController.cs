// <copyright file="PlaygroundController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 11:57</date>

using System;
using System.Collections.Generic;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds;
using UnityEngine;

namespace BrickGame.Scripts.Playground
{
    /// <summary>
    /// PlaygroundController - main playground controller, contains main setups and links.
    /// Moving figures to down edge.
    /// Finalizing playground on end of figures movement.
    /// </summary>
    [Obsolete("Depricated class.")]
    public abstract class PlaygroundController : GameBehaviour, IModelMessageResiver
    {
        /// <summary>
        /// Enum of internal controller's states
        /// </summary>
        [Flags]
        private enum InternalState
        {
            /// <summary>
            /// FigureMatrix was initialized
            /// </summary>
            Initialized = 0x1,
            /// <summary>
            /// Game was started
            /// </summary>
            Started = 0x2,
            /// <summary>
            /// Game on pause
            /// </summary>
            OnPause = 0x4
        }
        //================================       Public Setup       =================================
        [Tooltip("Width of playground matrix, count of collumnts")]
        public int Width = 10;//Default by classic rules

        [Tooltip("Height of playground matrix, count of rows")]
        public int Height = 20;//Default by classic rules

        [Tooltip("Score rules of the current game")]
        public GameRules Rules;

        /// <summary>
        /// Get copy of playground matrix
        /// </summary>
        public bool[] Matrix
        {
            get
            {
                return Model == null ? new bool[0] : Model.Matrix;
            }
        }

        //================================    Systems properties    =================================
        private InternalState _state;

        private int _levels;

        private int _score;

        protected float Speed { get; private set; }

        protected PlaygroundModel Model { get; private set; }
        //================================      Public methods      =================================

        /// <inheritdoc />
        public void UpdateModel(PlaygroundModel model)
        {
            Model = model;
        }

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
                    StartSession();
                    break;
                case PlaygroundNotification.Pause:
                    PauseSession();
                    break;
                case PlaygroundNotification.End:
                    FinishSession();
                    break;
            }
            //Change global state of the component
            enabled = (_state & InternalState.Started) == InternalState.Started;
            if (enabled) enabled = (_state & InternalState.OnPause) != InternalState.OnPause;
        }

        private void StartSession()
        {
            if ((_state & InternalState.Started) == InternalState.Started)
            {
                Debug.LogWarningFormat("Game already started on playground {0}", name);
                return;
            }
            Debug.LogFormat("Game started on {0}", name);
            Speed = Rules.GetSpeedByLines(Model.RemovedLines);
            _state |= InternalState.Started;
        }

        /// <summary>
        /// Switch between pause and resume of session
        /// </summary>
        private void PauseSession()
        {
            if ((_state & InternalState.OnPause) == InternalState.OnPause)
                _state ^= InternalState.OnPause;
            else
                _state |= InternalState.OnPause;
        }

        /// <summary>
        /// Clean playground from current session
        /// </summary>
        private void FinishSession()
        {
            Debug.Log("Game was ended.");
            //Remove started and pause flags
            _state = InternalState.Initialized;
        }

        /// <summary>
        /// Remove lines from playground
        /// </summary>
        /// <param name="lines"></param>
        protected void RemoveLines(List<int> lines)
        {
            //Remove lines from playground
            int count = lines.Count;
            if (count <= 0) return;
            foreach (int y in lines)Model.RemoveLine(y);
            Model.MoveDown(lines[0] - 1, lines[lines.Count - 1]);
            //UpdateColors speed by total count of removed lines
            Speed = Rules.GetSpeedByLines(Model.RemovedLines);
            BroadcastNofitication(GameNotification.ScoreUpdated,
                new ScoreDataProvider(Rules, gameObject.name, count));
        }

    }
}