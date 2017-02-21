// <copyright file="FigureController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 17:30</date>

using BrickGame.Scripts.Models;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// FigureController - component for controlling figureMatrix behaviour in the playground.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(FigureControls), typeof(FigureBuilder))]
    public class FigureController : GameBehaviour
    {
        /// <summary>
        /// Delay fo coroutine
        /// </summary>
        private const float Delay = 0.8F;
        private const float Step = 1.0F;
        //================================       Public Setup       =================================
        [Tooltip("Spaw point for figures")]
        public Vector2 SpawnPoint;
        [Tooltip("Falling speed of a figureMatrix")]
        [Range(0F, 100F)]
        public float Speed = 1F;
        //================================    Systems properties    =================================
        private float _position;
        private FigureBuilder _builder;
        private IFigureControls _controls;

        private Coroutine _finisher;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Get links to components
        /// </summary>
        private void Awake()
        {
            _builder = GetComponent<FigureBuilder>();
            _controls = GetComponent<IFigureControls>();
            Context.AddListener(GameState.Start, StateHandler);
            Context.AddListener(GameState.Pause, StateHandler);
            enabled = false;
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameState.Start, StateHandler);
            Context.RemoveListener(GameState.Pause, StateHandler);
        }

        /// <summary>
        /// Accelerate figure speed
        /// </summary>
        /// <param name="speed">new speed of the figure</param>
        [UsedImplicitly]
        private void AccelerateFigure(float speed)
        {
            Speed = speed;
        }

        private void ChangeFigure()
        {
            Debug.Log("ChangeFigure");
            //Create new figureMatrix
            FigureMatrix matrix = _builder.Pop();
            matrix.x = (int)(SpawnPoint.x - matrix.Width * 0.5F);
            matrix.y = (int)(SpawnPoint.y - (matrix.Height - 1));
            SendMessage(MessageReceiver.UpdateFigure, matrix);
            BroadcastNofitication(FigureNotification.Changed);
            if (!enabled) enabled = true;
        }

        private void StateHandler(string state = null)
        {
            if(!gameObject.activeInHierarchy)return;
            Debug.Log("StartHandler");
            if (state == GameState.Start)
                ChangeFigure();
            else if (state == GameState.Pause)
                enabled = !enabled;
        }

        /// <summary>
        /// Update figureMatrix position
        /// </summary>
        private void Update()
        {
            _position = Mathf.MoveTowards(_position, Step, Time.deltaTime * Speed);
            if (_position < Step) return;
            //set position to zero
            _position = 0;
            //Move figureMatrix down
            if (_controls.CanMoveVertical(1))
            {
                _controls.MoveVertical(1);
                if (_finisher == null) return;
                StopCoroutine(_finisher);
                _finisher = null;
                return;
            }
            //Current figureMatrix can't fall further.
            if(_finisher == null)//Append figure to playground with delay
                _finisher = StartCoroutine(Try2Append());
        }

        /// <summary>
        /// Try to append figure to playground matrix.
        /// User can move figure horizontally.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Try2Append()
        {
            yield return new WaitForSeconds(Delay);
            //Figure was moved horizontaly and can be moved down  further.
            if (_controls.CanMoveVertical(1))
            {
                StopCoroutine(_finisher);
                _finisher = null;
                yield break;
            }
            enabled = false;
            if (_controls.Y > 0)
                SendMessageUpwards(MessageReceiver.AppendFigure,  SendMessageOptions.DontRequireReceiver);
            else
                SendMessageUpwards(MessageReceiver.FinishSession,  SendMessageOptions.DontRequireReceiver);
            _finisher = null;
            yield return null;
        }
    }
}