// <copyright file="AbstractStrategy.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/23/2017 19:23</date>

using System;
using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.Figures;
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
        private float _timer;
        private float _timeout;

        private Figure _figure;
        private Playground _playground;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void Awake()
        {
            Context.AddListener(GameState.Start, StateHandler);
            Context.AddListener(GameState.End, StateHandler);
            Context.AddListener(GameState.Pause, StateHandler);
            enabled = false;
        }

        protected virtual void OnDestroy()
        {
            _figure = null;
            _playground = null;
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
                    _timer = 0;
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

        private void LateUpdate()
        {
            if((_timer += Time.deltaTime) < _timeout)return;
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