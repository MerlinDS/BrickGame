// <copyright file="PlaygroundController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:29</date>

using System;
using System.Collections;
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
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlaygroundBehaviour), typeof(IFigureController))]
    public class PlaygroundController : AbstractPlaygroundController
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private float _colldown;
        private Coroutine _finalization;
        private PlaygroundBehaviour _view;
        private IFigureController _figureController;
        //================================      Public methods      =================================
        /// <summary>
        /// Inintialize controllers and rebuild playground if it needed
        /// </summary>
        public void Start()
        {
            _view = GetComponent<PlaygroundBehaviour>();
            _figureController = GetComponent<IFigureController>();
            if (Application.isPlaying)enabled = false;
            BroadcastNofitication(PlaygroundNotification.Restore, new SessionDataProvider(name, Rules));
        }
        //================================ Private|Protected methods ================================

        /// <summary>
        /// Move active figure to down edge
        /// </summary>
        private void LateUpdate()
        {
            //Check if cooldwon was passed
            if ((_colldown += Time.deltaTime) < Speed) return;
            _colldown = 0;
            //Try to move figure down
            if (!_figureController.MoveDown())
            {
                if (!_figureController.OutOfBounds)
                {
                    /*
                        End of the turn.
                        Figure can't be moved further.
                        Need to finalize playground.
                    */
                    if(_finalization == null)
                        _finalization = StartCoroutine(FinalizePlayground());
                }
                else
                {
                    /*
                        One of the figures is upper than top edge.
                        The game is over.
                    */
                    _figureController.Remove();
                    //TODO TASK: Execute end of the game animation
                    BroadcastNofitication(PlaygroundNotification.End);
                }
                return;
            }
            //Figure moded down, remove finalization
            if (_finalization == null) return;
            StopCoroutine(_finalization);
            _finalization = null;
        }

        /// <summary>
        /// Finalize playground: Check lines for fullness, check game for ending
        /// </summary>
        private IEnumerator FinalizePlayground()
        {
            yield return new WaitForSeconds(Rules.FinalizingGap);
            if (_figureController.MoveDown())yield break;
            _figureController.Remove();
            //Search for filled lines
            List<int> lines = new List<int>();
            for (int y = 0; y < Model.Height; y++)
            {
                if (Model.FullLine(y))lines.Add(y);
            }
            //Full lines were not found. Create a new figure.
            if (lines.Count == 0)
            {
                SendMessage(PlaygroundMessage.CreateFigure);
                yield break;
            }
            //Full lines exist, need to remove these lines and create a new figure.
            _view.EndOfBlinking += RemoveCells;
            _view.Blink(lines.ToArray());
            RemoveLines(lines);
            enabled = false;//Stop updating
            yield return null;
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
            enabled = true;//Start updating
        }

    }
}