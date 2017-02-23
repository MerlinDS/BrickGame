﻿// <copyright file="AbstractStrategy.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/23/2017 19:23</date>

using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Playgrounds.Strategies
{
    /// <summary>
    /// AbstractStrategy
    /// </summary>
    public abstract class AbstractStrategy : GameBehaviour
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private float _timeout;
        private float _cooldown;
        [NotNull]private Matrix<bool> _matrix = new PlaygroundMatrix(10, 20);
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            _matrix = matrix ?? new PlaygroundMatrix(10, 20);
        }
        //================================ Private|Protected methods ================================
        private void Awake()
        {
            Context.AddListener(GameState.Start, StateHandler);
            Context.AddListener(GameState.End, StateHandler);
            Context.AddListener(GameState.Pause, StateHandler);
            enabled = false;
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameState.Start, StateHandler);
            Context.RemoveListener(GameState.End, StateHandler);
            Context.RemoveListener(GameState.Pause, StateHandler);
        }

        private void StateHandler(string s)
        {
            switch (s)
            {
                case GameState.Start:
                    _timeout = Context.GetActor<GameModeManager>().CurrentRules.StartegyTimeout;
                    _cooldown = 0;
                    enabled = true;
                    break;
                case GameState.Pause:
                    enabled = !enabled;
                    break;
                case GameState.End:
                    enabled = false;
                    break;
            }
        }

        private void Update()
        {
            if((_cooldown += Time.deltaTime) < _timeout)return;
            Apply(_matrix);
            _cooldown = 0;
        }

        protected abstract void Apply(Matrix<bool> matrix);
    }
}