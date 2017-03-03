// <copyright file="NotificationControls.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/02/2017 21:54</date>

using System.Collections.Generic;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Models.Session;
using BrickGame.Scripts.Utils.Input;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// NotificationControls - component for controlling notification pop up
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class NotificationControls : GameBehaviour
    {
        //================================       Public Setup       =================================
        [Tooltip("Message for pause pop up")] public string PauseMessage = "Pause\ntap to resume";

        [Tooltip("Message end of game pop up")] public string ScoreMessage = "You score:\n";

        //================================    Systems properties    =================================
        private const string Hide = "hide";

        private bool _tapped;
        [SerializeField] private Text _message;
        private Queue<string> _queue;
        private IInputAdapter _input;

        private Animator _animator;
        private ScoreModel _scoreModel;

        private SessionModel _sessionModel;

        //================================      Public methods      =================================
        public void ShowNext()
        {
            _tapped = false;
            if (_queue.Count == 0)
            {
                if (_sessionModel.Has(SessionState.OnPause))
                    BroadcastNofitication(StateNotification.Pause);
                else if (_sessionModel.Has(SessionState.Ended))
                    BroadcastNofitication(StateNotification.Start);
                gameObject.SetActive(false);
                return;
            }
            _message.text = _queue.Dequeue();
            gameObject.SetActive(true);
        }

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize component
        /// </summary>
        private void Awake()
        {
            _queue = new Queue<string>();
            _animator = GetComponent<Animator>();
            _scoreModel = Context.GetActor<ScoreModel>();
            _sessionModel = Context.GetActor<SessionModel>();
            _input = Context.GetActor<InputManager>().GetInputAdapter();
            Context.AddListener(StateNotification.Pause, Handler);
            Context.AddListener(StateNotification.End, Handler);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Destroy component, remove listener
        /// </summary>
        private void OnDestroy()
        {
            Context.RemoveListener(StateNotification.Pause, Handler);
            Context.RemoveListener(StateNotification.End, Handler);
        }

        /// <summary>
        /// Handle notifications from context
        /// </summary>
        /// <param name="n">Notification type</param>
        private void Handler(string n)
        {
            switch (n)
            {
                case StateNotification.Pause:
                    if (_sessionModel.Has(SessionState.OnPause))
                        _queue.Enqueue(PauseMessage);
                    break;
                case StateNotification.End:
                    _queue.Enqueue(ScoreMessage + _scoreModel[ScoreModel.FieldName.Score]);
                    break;
            }
            if (gameObject.activeInHierarchy) return;
            ShowNext();
        }
        /// <summary>
        /// User input detection
        /// </summary>
        private void Update()
        {
            if (_tapped) return;
            Touch touch;
            InputGesture gesture;
            if (!_input.Detect(out touch, out gesture)) return;
            if (gesture != InputGesture.Tap) return;
            _animator.SetTrigger(Hide);
            _tapped = true;
        }

    }
}