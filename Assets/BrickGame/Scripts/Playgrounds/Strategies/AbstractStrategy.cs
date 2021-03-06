﻿// <copyright file="AbstractStrategy.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/23/2017 19:23</date>

using System;
using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Playgrounds.Strategies
{
    /// <summary>
    /// AbstractStrategy
    /// </summary>
    public abstract class AbstractStrategy : GameBehaviour, IStrategy
    {
        private const float Step = 30.0F;
        //================================       Public Setup       =================================
        /// <inheritdoc />
        public bool OnPasue { get { return !enabled; } }
        //================================    Systems properties    =================================
        private float _timer;
        private float _speed;

        private StrategySetup _setup;
        private Figure _figure;
        private Playground _playground;
        private ScoreModel _model;
        //================================      Public methods      =================================
        public void Initialize(StrategySetup setup)
        {
            _setup = setup;
            UpdateSpeed();
            Pause();
        }
        /// <inheritdoc />
        public void Pause()
        {
            if(OnPasue)return;
            enabled = false;
        }

        /// <inheritdoc />
        public void Resume()
        {
            if(!OnPasue)return;
            enabled = true;
            ScoreHandler(string.Empty);
        }

        /// <inheritdoc />
        public void UpdateSpeed(int factor = 1)
        {
            _speed = _setup.Speed + _setup.SpeedIncrease * (factor - 1);
            _timer = 0;
        }

        //================================ Private|Protected methods ================================
        private void Awake()
        {
            _model = Context.GetActor<ScoreModel>();
            Context.AddListener(GameState.Start, StateHandler);
            Context.AddListener(GameState.End, StateHandler);
            Context.AddListener(GameState.Pause, StateHandler);
            Context.AddListener(GameNotification.ScoreUpdated, ScoreHandler);
            enabled = false;
        }


        private void OnDestroy()
        {
            _model = null;
            _figure = null;
            _playground = null;
            Context.RemoveListener(GameState.Start, StateHandler);
            Context.RemoveListener(GameState.End, StateHandler);
            Context.RemoveListener(GameState.Pause, StateHandler);
            Context.RemoveListener(GameNotification.ScoreUpdated, ScoreHandler);
        }

        private void ScoreHandler(string s)
        {
            if(_playground == null)return;
            var factor = _model[ScoreModel.FieldName.Level, _playground.SessionName];
            UpdateSpeed(factor);
        }

        private void StateHandler(string s)
        {
            switch (s)
            {
                case GameState.Pause:
                    if (OnPasue) Resume();
                    else Pause();
                    break;
                case GameState.Start:
                    Resume();
                    break;
                case GameState.End:
                    Pause();
                    break;
            }
        }

        private void LateUpdate()
        {
            _timer = Mathf.MoveTowards(_timer, Step, Time.deltaTime * _speed);
            if (_timer < Step) return;

            if (_playground == null)
                _playground = GetInEnvironment<Playground>();
            if (_figure == null)
                _figure = GetInEnvironment<Figure>();
            Apply(_playground, _figure);
            _timer = 0;
        }


        [NotNull]
        private T GetInEnvironment<T>() where T : Component
        {
            T component = GetComponent<T>();
            if (component == null)
                component = GetComponentInChildren<T>();
            if (component == null)
                component = GetComponentInParent<T>();
            if(component == null)
                throw new ArgumentException("Can't find component: " + typeof(T));
            return component;
        }

        protected abstract void Apply([NotNull]Playground playground, [NotNull]Figure figure);
    }
}