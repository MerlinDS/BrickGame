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
    /// PlaygroundController - main playground controller, contains main setups and links
    /// </summary>
    [RequireComponent(typeof(PlaygroundBehaviour))]
    public class PlaygroundController : GameBehaviour
    {
        //================================       Public Setup       =================================
        [Tooltip("Width of playground matrix, count of collumnts")]
        public int Width = 10;//Default by classic rules
        [Tooltip("Height of playground matrix, count of rows")]
        public int Height = 20;//Default by classic rules
        [Tooltip("Score rules of the current game")]
        public GameRules Rules;

        public PlaygroundModel Model { get; private set; }
        //================================    Systems properties    =================================
        private bool _started;
        private float _speed = 1F;
        private float _timer;
        private ScoreModel _scoreModel;
        private PlaygroundBehaviour _view;
        private IFigureController[] _figureControllers;
        //================================      Public methods      =================================
        /// <summary>
        /// Rebuild playground
        /// </summary>
        [ExecuteInEditMode]
        public void Start()
        {
            Model = new PlaygroundModel(Width, Height);
            _view = GetComponent<PlaygroundBehaviour>();
            _view.Rebuild(Model);
            enabled = false;
        }
        //================================ Private|Protected methods ================================
        private void Awake()
        {
            Context.AddListener(GameNotification.Start, GameNotificationHandler);
            _scoreModel = Context.GetActor<ScoreModel>();
            _scoreModel.SetRules(Rules);
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameNotification.Start, GameNotificationHandler);
        }

        /// <summary>
        /// Move figure down
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
                        //Figure can't be moved down, need to finalize playground
                        figureController.Remove();
                        FinalizePlayground();
                    }
                    else
                    {
                        figureController.Remove();
                        Debug.Log("End of the game with score: " + _scoreModel.Score);
                        BroadcastNofitication(GameNotification.EndOfGame);
                        _started = enabled = false;
                    }
                }
            }
        }

        private void GameNotificationHandler(string notification)
        {
            if (notification == GameNotification.Start)
            {
                if (!_started)
                {
                    Debug.Log("Start new game on " + gameObject.name);
                    Model.Reset();
                    _scoreModel.Reset();
                    _speed = Rules.StartingSpeed;
                    _figureControllers = GetComponents<IFigureController>();
                    SendMessage(PlaygroundMessage.CreateFigure);
                    _started = enabled = true;
                }
                else
                {
                    //Pause game
                    enabled = !enabled;
                }
            }
        }

        // ReSharper disable once UnusedMember.Local
        private void FinalizePlayground()
        {
            List<int> lines = new List<int>();
            for (int y = 0; y < Model.Height; y++)
            {
                if (Model.FullLine(y))lines.Add(y);
            }
            if (lines.Count == 0)
            {
                SendMessage(PlaygroundMessage.CreateFigure);
                return;
            }
            _view.EndOfBlinking += RemoveCells;
            _view.Blink(lines.ToArray());
            //Remove lines from playground
            foreach (int y in lines)
            {
                for (int x = 0; x < Model.Width; ++x)
                    Model[x, y] = false;
            }
            Model.MoveDown(lines[0] - 1, lines[lines.Count - 1]);
            _scoreModel.AddLines(lines.Count);
            _speed = Rules.GetSpeed( _scoreModel.Level);
            enabled = false;//Wiat for callback
            BroadcastNofitication(GameNotification.ScoreUpdated);
        }

        private void RemoveCells(object sender, EventArgs e)
        {
            _view.EndOfBlinking -= RemoveCells;
            SendMessage(PlaygroundMessage.CreateFigure);
            enabled = true;
        }

    }
}