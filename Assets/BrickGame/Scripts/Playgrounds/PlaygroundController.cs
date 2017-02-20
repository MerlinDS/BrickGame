// <copyright file="PlaygroundController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 23:37</date>

using System.Collections;
using System.Collections.Generic;
using BrickGame.Scripts.Bricks;
using BrickGame.Scripts.Effects;
using BrickGame.Scripts.Models;
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
        private List<Brick> _briks;//For effects
        private PlaygroundMatrix _matrix;
        private PlaygroundDrawer _drawer;
        private BricksBlinkingEffectBehaviour _bricksBlinking;
        private SceneBlinkingEffect _sceneBlinking;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            _matrix = (PlaygroundMatrix)matrix ?? new PlaygroundMatrix(0, 0);
            _bricksBlinking = GetComponent<BricksBlinkingEffectBehaviour>();
            _sceneBlinking = GetComponent<SceneBlinkingEffect>();
            _drawer = GetComponentInChildren<PlaygroundDrawer>();
        }

        [UsedImplicitly]
        public void FinishSession()
        {
            if(_sceneBlinking != null)
                _sceneBlinking.Execute();
            BroadcastNofitication(PlaygroundNotification.End);
        }
        /// <summary>
        ///
        /// </summary>
        [UsedImplicitly]
        public void AppendFigure()
        {
            //Find all filled lines and delete them
            List<int> lines = new List<int>();
            for (int y = 0; y < _matrix.Height; ++y)
            {
                if(_matrix.RowContainsValue(y, true))
                    lines.Add(y);
            }
            int count = lines.Count;
            if (count <= 0)
            {
                //Full lines were not found. Nothing to do.
                BroadcastMessage(MessageReceiver.ChangeFigure);
                return;
            }
            //Full lines exist, need to remove these lines.
            //TODO: Send lines to score and update figure speed
            float delay = 0.0F;
            if(_bricksBlinking != null && _drawer != null)
            {
                _drawer.Pause();
                if(_briks == null)_briks = new List<Brick>();
                _briks.Clear();
                foreach (int y in lines) _drawer.GetRow(y, ref _briks);
                delay = _bricksBlinking.Execute(_briks);
            }
            StartCoroutine(RemoveLines(lines, delay));
        }
        //================================ Private|Protected methods ================================

        private IEnumerator RemoveLines(List<int> lines, float delay)
        {
            yield return new WaitForSeconds(delay);
            foreach (int y in lines)_matrix.FillRow(y, false);
            _matrix.MoveDownRows(lines[0] - 1, lines[lines.Count - 1]);
            BroadcastMessage(MessageReceiver.ChangeFigure);
            if(_drawer != null)_drawer.Resume();
            yield return null;
        }
    }
}