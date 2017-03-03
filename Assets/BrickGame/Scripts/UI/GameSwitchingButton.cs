// <copyright file="GameSwitchingButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 9:50</date>

using System;
using BrickGame.Scripts.Models.Session;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// GameSwitchingButton - switching a game state
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class GameSwitchingButton : GameBehaviour
    {
        /// <summary>
        /// Enum of button states
        /// </summary>
        private enum ButtonState
        {
            Start,
            Pause,
            Resume
        }

        //================================       Public Setup       =================================
        [Tooltip("Rebuild button label text")] public string StartLable = "START";

        [Tooltip("Pause button label text")] public string PauseLabel = "PAUSE";
        [Tooltip("Resume button label text")] public string ResumeLabel = "RESUME";

        [Tooltip("Label text field")] public Text Label;

        //================================    Systems properties    =================================
        private ButtonState _state;

        private Button _button;
        private SessionModel _sessionModel;

        //================================      Public methods      =================================
        /// <summary>
        /// Reset button to starting state
        /// </summary>
        public void ResetState()
        {
            Label.text = StartLable;
            _state = ButtonState.Start;
        }

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize button and add context listeners
        /// </summary>
        private void Awake()
        {
            ResetState();
            _sessionModel = Context.GetActor<SessionModel>();
            _sessionModel.EndEvent += ResetState;
            _sessionModel.StartEvent += ChangeState;
            _sessionModel.PauseEvent += ChangeState;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                BroadcastNofitication(_sessionModel
                    .Any(SessionState.None, SessionState.Ended)
                    ? StateNotification.Start
                    : StateNotification.Pause);
                //TODO: Remove after tests
                //BroadcastNofitication(StateNotification.Start);
            });
        }
        /// <summary>
        /// Remove listeners form the button
        /// </summary>
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            _sessionModel.EndEvent -= ResetState;
            _sessionModel.StartEvent -= ChangeState;
            _sessionModel.PauseEvent -= ChangeState;
        }

        private void ChangeState()
        {
            ChangeState(false);
        }
        /// <summary>
        /// Cahange button statte
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ChangeState(bool onPause)
        {
            switch (_state)
            {
                case ButtonState.Start:
                    Label.text = PauseLabel;
                    _state = ButtonState.Pause;
                    break;
                case ButtonState.Pause:
                    Label.text = ResumeLabel;
                    _state = ButtonState.Resume;
                    break;
                case ButtonState.Resume:
                    Label.text = PauseLabel;
                    _state = ButtonState.Pause;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}