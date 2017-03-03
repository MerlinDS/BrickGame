// <copyright file="FigureController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 17:30</date>

using System.Collections;
using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Models.Session;
using BrickGame.Scripts.Utils;
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
        private int _direction;
        private float _position;
        private Coroutine _finisher;
        private FigureBuilder _builder;
        private IFigureControls _controls;
        private SessionModel _sessionModel;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Get links to components
        /// </summary>
        private void Awake()
        {
            _builder = GetComponent<FigureBuilder>();
            _controls = GetComponent<IFigureControls>();
            _sessionModel = Context.GetActor<SessionModel>();
            GameRules rules = Context.GetActor<GameModeManager>().CurrentRules;
            _direction = (int)rules.FallingDirection;
            SpawnPoint = rules.SpawPosition;
            //Add listeners
            _sessionModel.StartEvent += StartHandler;
            _sessionModel.PauseEvent += PauseHandler;
            enabled = false;
        }

        /// <summary>
        /// Remove listeners
        /// </summary>
        private void OnDestroy()
        {
            _sessionModel.StartEvent -= StartHandler;
            _sessionModel.PauseEvent -= PauseHandler;
            _sessionModel = null;
            _controls = null;
            _builder = null;
        }

        /// <summary>
        /// Handle start notification
        /// </summary>
        private void StartHandler()
        {
            if(!gameObject.activeInHierarchy)return;
            _position = Step;
            PauseHandler(false);
        }

        /// <summary>
        /// Handle pause event
        /// </summary>
        /// <param name="onPause"></param>
        private void PauseHandler(bool onPause)
        {
            if(!gameObject.activeInHierarchy)return;
            enabled = !onPause;
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

        [UsedImplicitly]
        private void ChangeFigure()
        {
//            Debug.Log("ChangeFigure");
            //Create new figureMatrix
            FigureMatrix matrix = _builder.Pop();
            matrix.x = (int)(SpawnPoint.x - matrix.Width * 0.5F);
            matrix.y = (int) (SpawnPoint.y);
            if(_direction == (int)VerticalDirection.Down)
                matrix.y -= matrix.Height - 1;
            SendMessage(MessageReceiver.UpdateFigure, matrix);
            BroadcastNofitication(FigureNotification.Changed);
            _position = 0;
            if (!enabled) enabled = true;
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
            if (_controls.CanMoveVertical(_direction))
            {
                _controls.MoveVertical(_direction);
                if (_finisher == null) return;
                StopCoroutine(_finisher);
                _finisher = null;
                return;
            }
            /*
                Current figureMatrix can't fall further.
                If a figure can't move horizontaly, then try to append figure immidiatly.
                In other cases start coroutine with delay.
            */

            if (!_controls.CanMoveHorizontal(1) && !_controls.CanMoveHorizontal(-1))
            {
                if(_finisher != null)StopCoroutine(_finisher);
                SendFinishMessage();
                return;
            }

            if(_finisher == null)
                _finisher = StartCoroutine(Try2Append(Delay));
        }

        /// <summary>
        /// Try to append figure to playground matrix.
        /// User can move figure horizontally.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Try2Append(float delay)
        {
            yield return new WaitForSeconds(delay);
            //Figure was moved horizontaly and can be moved down  further.
            if (_controls.CanMoveVertical(_direction))
            {
                StopCoroutine(_finisher);
                _finisher = null;
                yield break;
            }
           SendFinishMessage();
            _finisher = null;
            yield return null;
        }

        /// <summary>
        /// Detect if figure is out of edge and broadcast finishing message.
        /// </summary>
        private void SendFinishMessage()
        {
            enabled = false;
            Context.Notify(AudioNotification.Stop);
            SendMessageUpwards( _controls.OutOfEdge ?
                    MessageReceiver.FinishSession : MessageReceiver.AppendFigure,
                SendMessageOptions.DontRequireReceiver);
        }
    }
}