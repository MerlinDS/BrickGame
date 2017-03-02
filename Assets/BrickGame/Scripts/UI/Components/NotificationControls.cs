// <copyright file="NotificationControls.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/02/2017 21:54</date>

using System.Collections.Generic;
using BrickGame.Scripts.Utils.Input;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// NotificationControls
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class NotificationControls : GameBehaviour
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        [SerializeField]
        private Text _message;
        private Queue<string> _queue;
        private IInputAdapter _input;

        private Animator _animator;

        private bool _tapped;

        private bool _onPause;
        //================================      Public methods      =================================
        public void ShowNext()
        {
            _tapped = false;
            if (_queue.Count == 0)
            {
                if(_onPause)BroadcastNofitication(GameState.Pause);
                gameObject.SetActive(false);
                return;
            }
            Debug.Log("Notification shown");
            _message.text = _queue.Dequeue();
            gameObject.SetActive(true);
        }
        //================================ Private|Protected methods ================================
        private void Awake()
        {
            _queue = new Queue<string>();
            _input = Context.GetActor<InputManager>().GetInputAdapter();
            _animator = GetComponent<Animator>();
            Context.AddListener(GameState.Pause, Handler);
            Context.AddListener(GameState.End, Handler);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameState.Pause, Handler);
            Context.RemoveListener(GameState.End, Handler);
        }

        private void Update()
        {
            if(_tapped)return;
            Touch touch;
            InputGesture gesture;
            if(!_input.Detect(out touch, out gesture))return;
            if(gesture != InputGesture.Tap)return;
            _animator.SetTrigger("hide");
            _tapped = true;
        }

        private void Handler(string s)
        {
            switch (s)
            {
                case GameState.Pause:
                    if (!_onPause) _queue.Enqueue("Pause\ntap to resume");
                    _onPause = !_onPause;
                    break;
                case GameState.End:
                    _queue.Enqueue("You score:\n1000000000");
                    break;
            }
            if (gameObject.activeInHierarchy)return;
            ShowNext();
        }


    }
}