// <copyright file="MonetizationService.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/01/2017 12:33</date>

using UnityEngine;

namespace BrickGame.Scripts.Services.Monitization
{
    /// <summary>
    /// MonetizationService
    /// </summary>
    public abstract class MonetizationService : GameManager
    {
        //================================       Public Setup       =================================
        [Tooltip("Is ads rewarded")]
        [SerializeField]
        private bool _isRewarder;
        [Tooltip("Tyrs before showing an adv")]
        [SerializeField]
        private int _trys = 3;
        [Tooltip("Time out between ads in second")]
        [SerializeField]
        private float _timeout = 30;
        //================================    Systems properties    =================================
        protected abstract bool IsShowing { get; set; }

        private bool _isAvailable;
        private int _trysCounter;
        private float _timer;

        private string _response;
        //================================      Public methods      =================================
        //================================ Private|Protected methods ================================

        protected abstract void Initialize(bool isRewarded,  out bool isAvailable);
        protected abstract void TryToShowVideo();

        protected void FinishShow()
        {
            _timer = Time.realtimeSinceStartup;
            _trysCounter = 0;
            Context.Notify(_response);
        }

        private void OnDestroy()
        {
            if(!Context.HasListener(AdvNotification.ShowVideo, NotificationHandler))return;
            Context.RemoveListener(AdvNotification.ShowVideo, NotificationHandler);
        }

        private void Start()
        {
            Initialize(_isRewarder, out _isAvailable);
            _timer = Time.realtimeSinceStartup;
            Context.AddListener(AdvNotification.ShowVideo, NotificationHandler);
        }

        private void NotificationHandler(string n)
        {
            if(!_isAvailable)return;
            if (_trysCounter++ < _trys || Time.realtimeSinceStartup - _timer < _timeout) return;
            if (IsShowing)
            {
                Debug.LogWarning("Advertising is already showing");
                return;
            }
            if (n != AdvNotification.ShowVideo) return;
            _response = AdvNotification.VideoShown;
            TryToShowVideo();
        }
    }
}