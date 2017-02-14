// <copyright file="PlaygroundController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:29</date>

using System;
using System.Collections.Generic;
using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Playground
{
    /// <summary>
    /// PlaygroundController - main playground controller, contains main setups and links.
    /// Moving figures to down edge.
    /// Finalizing playground on end of figures movement.
    /// </summary>
    [RequireComponent(typeof(PlaygroundBehaviour))]
    public class PlaygroundController : GameBehaviour
    {
        /// <summary>
        /// Enum of internal controller's states
        /// </summary>
        [Flags]
        private enum InternalState
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
        /// Link to Playground model
        /// </summary>
        public PlaygroundModel Model { get; private set; }
        //================================    Systems properties    =================================
        private bool _started;
        private InternalState _state;
        private float _speed = 1F;
        private float _timer;
        private ScoreModel _scoreModel;
        private PlaygroundBehaviour _view;
        private IFigureController[] _figureControllers;
        //================================      Public methods      =================================
        /// <summary>
        /// Inintialize controllers and rebuild playground if it needed
        /// </summary>
        [ExecuteInEditMode]
        public void Start()
        {
            Model = new PlaygroundModel(Width, Height);
            _view = GetComponent<PlaygroundBehaviour>();
            _view.Rebuild(Model);
            _state = InternalState.Initialized;
            enabled = false;
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Preinitialize controller and add listeners to context notifications
        /// </summary>
        private void Awake()
        {
            Context.AddListener(GameNotification.Start, GameNotificationHandler);
            Context.AddListener(GameNotification.EndOfGame, GameNotificationHandler);
            _scoreModel = Context.GetActor<ScoreModel>();
            _scoreModel.SetRules(Rules);
        }

        /// <summary>
        /// Remove context listners
        /// </summary>
        private void OnDestroy()
        {
            Context.RemoveListener(GameNotification.Start, GameNotificationHandler);
            Context.RemoveListener(GameNotification.EndOfGame, GameNotificationHandler);
        }

        /// <summary>
        /// Move active figure to down edge
        /// </summary>
        private void LateUpdate()
        {
            //Check if cooldwon was passed
            if ((_timer += Time.deltaTime) < _speed) return;
            _timer = 0;
            int n = _figureControllers.Length;
            for (int i = 0; i < n; i++)
            {
                IFigureController figureController = _figureControllers[i];
                if (!figureController.MoveDown())
                {
                    if (!figureController.OutOfBounds)
                    {
                        /*
                            End of the turn.
                            Figure can't be moved feuthur.
                            Need to finalize playground.
                        */
                        figureController.Remove();
                        //TODO TASK: Add time gap
                        FinalizePlayground();
                    }
                    else
                    {
                        /*
                            One of the figures is upper than top edge.
                            The game is over.
                        */
                        figureController.Remove();
                        Debug.Log("Game was ended with score: " + _scoreModel.Score);
                        //TODO TASK: Execute end of the game animation
                        BroadcastNofitication(GameNotification.EndOfGame);
                    }
                }
            }
        }

        /// <summary>
        ///  Handler for a game notifications
        /// </summary>
        /// <param name="notification">Name of the notification</param>
        private void GameNotificationHandler(string notification)
        {
            if (notification == GameNotification.Start)
            {
                if ((_state & InternalState.Started) != InternalState.Started)
                {
                    Debug.Log("Start new game on " + gameObject.name);
                    Model.Reset();
                    _scoreModel.Reset();
                    _speed = Rules.StartingSpeed;
                    _figureControllers = GetComponents<IFigureController>();
                    SendMessage(PlaygroundMessage.CreateFigure);
                    _state |= InternalState.Started;
                }
                else
                {
                    //Change pause state
                    if ((_state & InternalState.OnPause) == InternalState.OnPause)
                        _state ^= InternalState.OnPause;
                    else
                        _state |= InternalState.OnPause;
                    //TODO TASK: Save playground to cache
                }
            }
            else if (notification == GameNotification.EndOfGame)
            {
                //Remove started and pause flags
                _state = InternalState.Initialized;
            }
            //Change global state of the component
            enabled = (_state & InternalState.Started) == InternalState.Started;
            if (enabled) enabled = (_state & InternalState.OnPause) != InternalState.OnPause;
        }

        /// <summary>
        /// Finalize playground: Check lines for fullness, check game for ending
        /// </summary>
        private void FinalizePlayground()
        {
            List<int> lines = new List<int>();
            for (int y = 0; y < Model.Height; y++)
            {
                if (Model.FullLine(y))lines.Add(y);
            }
            //Full lines were not found. Create a new figure.
            if (lines.Count == 0)
            {
                SendMessage(PlaygroundMessage.CreateFigure);
                return;
            }
            //Full lines exist, need to remove these lines and create a new figure.
            _view.EndOfBlinking += RemoveCells;
            _view.Blink(lines.ToArray());
            //Remove lines from playground
            foreach (int y in lines)
            {
                for (int x = 0; x < Model.Width; ++x)
                    Model[x, y] = false;
            }
            Model.MoveDown(lines[0] - 1, lines[lines.Count - 1]);
            //Update score and level speed
            _scoreModel.AddLines(lines.Count);
            _speed = Rules.GetSpeed( _scoreModel.Level);
            //Stop updating and wait for callback from the view.
            enabled = false;
            BroadcastNofitication(GameNotification.ScoreUpdated);
            //TODO TASK: Save score to cahce
        }

        /// <summary>
        /// Handler for playground view callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveCells(object sender, EventArgs e)
        {
            _view.EndOfBlinking -= RemoveCells;
            SendMessage(PlaygroundMessage.CreateFigure);
            enabled = true;
        }

    }
}