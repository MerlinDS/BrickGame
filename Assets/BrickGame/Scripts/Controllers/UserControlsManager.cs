// <copyright file="UserControlsManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 20:31</date>

using System;
using System.Collections.Generic;
using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Utils.Input;
using UnityEngine;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// UserControlsManager - Manager for user input.
    /// </summary>
    public class UserControlsManager : GameManager
    {
        //================================       Public Setup       =================================

        [Range(2F, 0.5F)]
        public float Sensifity = 1F;
        //================================    Systems properties    =================================
        private int _direction;
        private int _breaker;
        private float _slop;
        private float _deltaX;
        private float _topBorder;
        private List<IFigureControls> _controls;

        private Vector2 _began;

        private IInputAdapter _input;

        //================================      Public methods      =================================
        /// <summary>
        /// Refresh game controllers
        /// </summary>
        public void RefreshControllers()
        {
            _controls.Clear();
            _slop = Mathf.Round(20F / 252F * Screen.dpi) * 4;
            _topBorder = Screen.height - (Screen.height / 6F);

            GameObject[] playgrounds = GameObject.FindGameObjectsWithTag(SRTags.Player);
            foreach (GameObject playground in playgrounds)
            {
                IFigureControls controller = playground.GetComponent<IFigureControls>();
//                Debug.LogFormat("Refreshed {0} controller", controller);
                _controls.Add(controller);
            }
        }

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize manager, adding of necessary listeners and links.
        /// </summary>
        private void Awake()
        {
            _began = new Vector2();
            _controls = new List<IFigureControls>();
            _input = Context.GetActor<InputManager>().GetInputAdapter();
            Context.AddListener(StateNotification.Start, GameNotificationHandler);
            Context.AddListener(StateNotification.Pause, GameNotificationHandler);
            Context.AddListener(FigureNotification.Changed, GameNotificationHandler);
            Context.AddListener(GameNotification.ModeChanged, GameNotificationHandler);
        }

        /// <summary>
        /// Remove listeners
        /// </summary>
        private void OnDestroy()
        {
            Context.RemoveListener(StateNotification.Start, GameNotificationHandler);
            Context.RemoveListener(StateNotification.Pause, GameNotificationHandler);
            Context.RemoveListener(FigureNotification.Changed, GameNotificationHandler);
            Context.RemoveListener(GameNotification.ModeChanged, this.GameNotificationHandler);
            _controls.Clear();
        }


        /// <summary>
        /// Handler for a game notifications
        /// </summary>
        /// <param name="notification"></param>
        private void GameNotificationHandler(string notification)
        {
            if (notification == GameNotification.ModeChanged)
            {
                _direction = (int)Context.GetActor<GameModeManager>()
                    .CurrentRules.FallingDirection;
                return;
            }
            if (notification == StateNotification.Start)
            {
                RefreshControllers();
                enabled = true;
            }
            else if (notification == StateNotification.Pause)
                enabled = !enabled;
            else if (notification == FigureNotification.Changed)
                _input.Reset();
        }
        /// <summary>
        /// Getting a user input and providing game actions.
        /// </summary>
        private void Update()
        {
            Touch touch;
            InputGesture gesture;
            if (!_input.Detect(out touch, out gesture)) return;
            switch (touch.phase)
            {
                case TouchPhase.Ended:
                    //TODO: avoid turn if position out of playground
                    if (gesture == InputGesture.Tap) Turn();
                    //TODO: avoid movement
                    else if (gesture == InputGesture.Swipe) MoveDown(20);
                    _began = Vector2.zero;
                    _deltaX = 0;
                    break;
                case TouchPhase.Began:
                    if (touch.position.y > _topBorder)
                    {
                        _input.Reset();
                        return;
                    }
                    _began = touch.position;
                    break;
                case TouchPhase.Moved:
                    if (touch.deltaPosition.y * _direction < 0)
                        _began = touch.position;
                    //Falling speedup
                    Vector2 delta = touch.position - _began;
                    float sensifity = Sensifity;
                    if (Math.Abs(delta.y) >= _slop * sensifity)
                    {
                        if (++_breaker > 4)
                        {
                            MoveDown(1);
                            _breaker = 0;
                        }
                        sensifity *= 2; //Decrise sensivity temporary
                    }

                    //Moving horisontaly
                    _deltaX += touch.deltaPosition.x;
                    if (Math.Abs(_deltaX) < _slop * sensifity)break;
                    MoveHorizontal(touch.deltaPosition.x);
                    _began = touch.position;
                    _deltaX = 0;
                    break;
            }
        }


        /// <summary>
        /// Execute turning of figures on playground
        /// </summary>
        private void Turn()
        {
            int n = _controls.Count;
            for (int i = 0; i < n; i++)
            {
//                var controller = _controls[i] as Component;
                //controller.transform
                _controls[i].Rotate();
            }
        }

        /// <summary>
        /// Move figures down to bottom edge of a playground.
        /// </summary>
        /// <param name="count">Count of iterations</param>
        private void MoveDown(int count)
        {
            for (var j = 0; j < count; j++)
            {
                int n = _controls.Count;
                for (int i = 0; i < n; i++)
                {
                    _controls[i].MoveVertical(_direction);
                }
            }
        }

        /// <summary>
        /// Move figures horizontally
        /// </summary>
        /// <param name="h">Horisontal factor</param>
        private void MoveHorizontal(float h)
        {
            int n = _controls.Count;
            for (int i = 0; i < n; i++)
            {
                _controls[i].MoveHorizontal(h < 0 ? 1 : -1);
            }
        }
    }
}