// <copyright file="FigureController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 17:30</date>

using BrickGame.Scripts.Models;
using BrickGame.Scripts.Utils;
using UnityEngine;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// FigureController - component for controlling figureMatrix behaviour in the playground.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BrickGame/Figures/FigureController")]
    [RequireComponent(typeof(FigureControls), typeof(FigureBuilder))]
    public class FigureController : GameBehaviour
    {
        private const float Step = 1.0F;
        //================================       Public Setup       =================================
        [Tooltip("Spaw point for figures")]
        public Vector2 SpawnPoint;
        [Tooltip("Falling speed of a figureMatrix")]
        public float Speed = 1F;
        //================================    Systems properties    =================================
        private float _position;
        private FigureBuilder _builder;
        private IFigureControls _controls;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Get links to components
        /// </summary>
        private void Start()
        {
            _builder = GetComponent<FigureBuilder>();
            _controls = GetComponent<IFigureControls>();
            Context.AddListener(PlaygroundNotification.Start, PlaygroundHandler);
            enabled = false;
        }

        private void OnDestroy()
        {
            Context.RemoveListener(PlaygroundNotification.Start, PlaygroundHandler);
        }

        private void PlaygroundHandler(string n = null)
        {
            //Create new figureMatrix
            FigureMatrix figureMatrix = _builder.Pop();
            figureMatrix.x = (int)SpawnPoint.x;
            figureMatrix.y = (int)SpawnPoint.y;
            Debug.Log("Create new figureMatrix: \n" + figureMatrix.Format(true));
            SendMessage(MessageReceiver.UpdateFigure, figureMatrix);
            BroadcastNofitication(FigureNotification.Changed);
            //
            if (n == PlaygroundNotification.Start)
                enabled = true;
        }

        /// <summary>
        /// Update figureMatrix position
        /// </summary>
        private void Update()
        {
            _position = Mathf.MoveTowards(_position, Step, Time.deltaTime * Speed);
            if (!(_position >= Step)) return;
            //set position to zero
            _position = 0;
            //Move figureMatrix down
            if (_controls.CanMoveVertical(1))
            {
                _controls.MoveVertical(1);
                return;
            }
            //Current figureMatrix can't fall further. Get a new figureMatrix.
            PlaygroundHandler();
        }
    }
}