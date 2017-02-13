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
    /// UserControlsManager
    /// </summary>
    public class UserControlsManager : GameManager
    {
        //================================       Public Setup       =================================
        [Range(0.01F, 1F)]
        public float HorCooldown = 0.1F;
        [Range(0.01F, 1F)]
        public float VerCooldown = 0.1F;

        [Range(40F, 100F)]
        public float SwapDonw;
        [Range(0F, 20F)]
        public float SwapHorisontal = 5F;
        //================================    Systems properties    =================================

        private float _hTimer;
        private float _vTimer;
        private List<IFigureController> _controllers;

        private Vector2 _began;

        private IInputAdapter _input;

        private double _swapHorisontal;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void Awake()
        {
            _began = new Vector2();
            _controllers = new List<IFigureController>();
            _input = Context.GetActor<IInputAdapter>();
            Context.AddListener(GameNotification.Start, GameNotificationHandler);
            Context.AddListener(GameNotification.FigureChanged, GameNotificationHandler);
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameNotification.Start, GameNotificationHandler);
            Context.RemoveListener(GameNotification.FigureChanged, GameNotificationHandler);
            _controllers.Clear();
        }

        private void RefreshControllers()
        {
            _controllers.Clear();
            GameObject[] playgrounds = GameObject.FindGameObjectsWithTag(SRTags.Playground);
            foreach (GameObject playground in playgrounds)
            {
                var figureControllers = playground.GetComponents<IFigureController>();
                foreach (IFigureController controller in figureControllers)
                    _controllers.Add(controller);
            }
            Debug.LogFormat("Refreshed {0} controllers", _controllers.Count);
        }

        private void GameNotificationHandler(string notification)
        {
            if (notification == GameNotification.Start)
                RefreshControllers();
            else if (notification == GameNotification.FigureChanged)
                _input.Reset();
        }

        private void FixedUpdate()
        {
            Touch touch;
            InputGesture gesture;
            if (!_input.Detect(out touch, out gesture)) return;
            switch (touch.phase)
            {
                case TouchPhase.Ended:
//                    Debug.Log("Ended" + gesture);
                    if (gesture == InputGesture.Tap) Turn();
                    else if (gesture == InputGesture.Swipe) MoveDown(20);
                    _began = Vector2.zero;
                    break;
                case TouchPhase.Began:
                    _began = touch.position;
                    break;
                case TouchPhase.Moved:
                    float delta = touch.position.y - _began.y;
                    float swap = SwapHorisontal;
                    if (delta < -SwapDonw)
                    {
                        MoveDown(1);
                        swap *= 2;
                        //Reset mobing down when touch goes up
                        if (touch.deltaPosition.y < 0)
                            _began = touch.position;
                    }
                    delta = -touch.deltaPosition.x;
                    if (Math.Abs(delta) > swap) MoveHorizontal(delta);
                    break;
            }
            //
        }



        private void Turn()
        {
            int n = _controllers.Count;
            for (int i = 0; i < n; i++)
                _controllers[i].Turn();
        }

        private void MoveDown(int count)
        {
            if (count <= 1)
            {
                if ((_hTimer += Time.deltaTime) < HorCooldown) return;
                _hTimer = 0;
            }
            for (var j = 0; j < count; j++)
            {
                int n = _controllers.Count;
                for (int i = 0; i < n; i++)
                    _controllers[i].MoveDown();
            }

        }

        private void MoveHorizontal(float h)
        {
            if ((_hTimer += Time.deltaTime) < HorCooldown) return;
            _hTimer = 0;

            int n = _controllers.Count;
            for (int i = 0; i < n; i++)
            {
                if (h < 0) _controllers[i].MoveLeft();
                else _controllers[i].MoveRight();
            }
        }
    }
}