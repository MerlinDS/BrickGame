// <copyright file="GameSwitchingButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 9:50</date>

using System;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// GameSwitchingButton
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class GameSwitchingButton : GameBehaviour
    {
        private enum ButtonState
        {
            Start, Pause, Resume
        }
        //================================       Public Setup       =================================
        [Tooltip("Start button label text")]
        public string StartLable = "START";
        [Tooltip("Pause button label text")]
        public string PauseLabel = "PAUSE";
        [Tooltip("Resume button label text")]
        public string ResumeLabel = "RESUME";
        [Tooltip("Label text field")]
        public Text Label;
        //================================    Systems properties    =================================
        private ButtonState _state;
        private Button _button;
        //================================      Public methods      =================================
        public void ResetState()
        {
            Label.text = StartLable;
            _state = ButtonState.Start;
        }
        //================================ Private|Protected methods ================================
        private void Start()
        {
            ResetState();
            Context.AddListener(GameNotification.Start, GameNotificationHandler);
            Context.AddListener(GameNotification.EndOfGame, GameNotificationHandler);
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                BroadcastNofitication(GameNotification.Start);
            });
        }


        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            Context.RemoveListener(GameNotification.Start, GameNotificationHandler);
            Context.RemoveListener(GameNotification.EndOfGame, GameNotificationHandler);
        }

        private void GameNotificationHandler(string notification)
        {
            if(notification == GameNotification.EndOfGame)ResetState();
            else ChangeState();
        }

        private void ChangeState()
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