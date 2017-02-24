// <copyright file="PlaygroundController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 23:37</date>

using System.Collections;
using System.Collections.Generic;
using BrickGame.Scripts.Bricks;
using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.Effects;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds.Strategies;
using BrickGame.Scripts.Utils;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Playgrounds
{
    /// <summary>
    /// PlaygroundController - controller for playground game object
    /// </summary>
    public class PlaygroundController : GameBehaviour, MessageReceiver.IPlaygroundReceiver
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private bool _changeFigure;
        private VerticalDirection _direction;
        private List<Brick> _briks;//For effects
        private PlaygroundMatrix _matrix;
        private PlaygroundDrawer _drawer;
        private BricksBlinkingEffectBehaviour _bricksBlinking;
        private SceneBlinkingEffect _sceneBlinking;
        private IStrategy _strategy;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            _matrix = (PlaygroundMatrix)matrix ?? new PlaygroundMatrix(0, 0);
            _briks = new List<Brick>();
            _bricksBlinking = GetComponent<BricksBlinkingEffectBehaviour>();
            _sceneBlinking = GetComponent<SceneBlinkingEffect>();
            _drawer = GetComponentInChildren<PlaygroundDrawer>();
            _strategy = GetComponent<IStrategy>();
            _direction = Context.GetActor<GameModeManager>().CurrentRules.FallingDirection;
        }
        [UsedImplicitly]
        public void FinishSession()
        {
            if(_sceneBlinking != null)
                _sceneBlinking.Execute();
        }
        /// <summary>
        ///
        /// </summary>
        [UsedImplicitly]
        public void AppendFigure()
        {
            //Find all filled rows and delete them
            int[] rows = _matrix.RemoveRows(_direction);
            if (rows.Length < 1)
            {
                //Full lines were not found. Nothing to do.
                SendMessage(MessageReceiver.UpdateScore, rows.Length);
                BroadcastMessage(MessageReceiver.ChangeFigure);
                return;
            }
            //Execute blinking of rows if they were found
            float delay = 0.0F;
            if(_bricksBlinking != null && _drawer != null)
            {
                _drawer.Pause();
                _briks.Clear();
                foreach (int y in rows) _drawer.GetRow(y, ref _briks);
                delay = _bricksBlinking.Execute(_briks);
            }
            if(_strategy != null)_strategy.Pause();
            SendMessage(MessageReceiver.UpdateScore, rows.Length);
            //Change figure with delay
            StartCoroutine(RemoveLines(delay));
        }
        //================================ Private|Protected methods ================================

        private IEnumerator RemoveLines(float delay)
        {
            if(delay > 0)yield return new WaitForSeconds(delay);
            BroadcastMessage(MessageReceiver.ChangeFigure);
            if(_drawer != null)_drawer.Resume();
            if(_strategy != null)_strategy.Resume();
            yield return null;
        }
    }
}