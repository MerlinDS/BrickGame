﻿// <copyright file="GameSwitchingButton.cs" company="Near Fancy">
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
            Start, Pause, Resume
        }
        //================================       Public Setup       =================================
        [Tooltip("Rebuild button label text")]
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
            Context.AddListener(GameState.Start, StateHandler);
            Context.AddListener(GameState.Pause, StateHandler);
            Context.AddListener(GameState.End, StateHandler);
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                if(_state == ButtonState.Start)
                    BroadcastNofitication(GameState.Start);
                else
                    BroadcastNofitication(GameState.Pause);
            });
        }


        /// <summary>
        /// Remove listeners form the button
        /// </summary>
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            Context.RemoveListener(GameState.Start, StateHandler);
            Context.RemoveListener(GameState.End, StateHandler);
            Context.RemoveListener(GameState.Pause, StateHandler);
        }

        /// <summary>
        /// Handle game notifications from context and change button state
        /// </summary>
        /// <param name="state">Name of a game state</param>
        private void StateHandler(string state)
        {
            if(state == GameState.End)ResetState();
            else ChangeState();
        }

        /// <summary>
        /// Cahange button statte
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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