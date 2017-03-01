// <copyright file="AudioController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:52</date>

using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// AudioController
    /// </summary>
    [RequireComponent(typeof(AudioListener), typeof(AudioSource))]
    public class AudioController : GameManager
    {
        //================================       Public Setup       =================================
        [Tooltip("Lose sound")] public AudioClip Lose;

        [Tooltip("Delete lines sound")] public AudioClip Delete;
        [Tooltip("Rotate figure sound")] public AudioClip Turn;
        [Tooltip("Stop figure sound")] public AudioClip Stop;

        /// <inheritdoc />
        public bool Muted
        {
            get { return _mainSource.mute; }
        }

        //================================    Systems properties    =================================
        private AudioSource _mainSource;
        private AudioSource _bgSource;

        private float _delay;

        //================================      Public methods      =================================
        /// <summary>
        /// Mute or unmute sounds
        /// </summary>
        public void Mute()
        {
            if (!_mainSource.mute)
            {
                _mainSource.Stop();
                _mainSource.clip = null;
                _bgSource.Pause();
            }
            else
            {
                _bgSource.Play();
            }
            _mainSource.mute = !_mainSource.mute;
            _bgSource.mute = _mainSource.mute;
        }

        /// <summary>
        /// Stopn all sound fx
        /// </summary>
        public void StopSfx()
        {
            _mainSource.Stop();
            _mainSource.clip = null;
        }
        //================================ Private|Protected methods ================================
        private void Awake()
        {
            var audioSources = GetComponents<AudioSource>();
            foreach (AudioSource source in audioSources)
            {
                if (source.loop)
                    _bgSource = source;
                else
                    _mainSource = source;
            }
            Context.AddListener(GameNotification.ScoreUpdated, GameNotificationHandler);
            Context.AddListener(GameState.End, GameNotificationHandler);
            Context.AddListener(AudioNotification.Stop, GameNotificationHandler);
            Context.AddListener(AudioNotification.Loos, GameNotificationHandler);
            Context.AddListener(AudioNotification.Click, GameNotificationHandler);
            Context.AddListener(AudioNotification.Move, GameNotificationHandler);
            Context.AddListener(GameNotification.MuteSound, GameNotificationHandler);
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameNotification.ScoreUpdated, GameNotificationHandler);
            Context.RemoveListener(GameState.End, GameNotificationHandler);
            Context.RemoveListener(AudioNotification.Stop, GameNotificationHandler);
            Context.RemoveListener(AudioNotification.Loos, GameNotificationHandler);
            Context.RemoveListener(AudioNotification.Click, GameNotificationHandler);
            Context.RemoveListener(AudioNotification.Move, GameNotificationHandler);
            Context.RemoveListener(GameNotification.MuteSound, GameNotificationHandler);
        }

        private void Start()
        {
            if(Context.GetActor<CacheModel>().AudioMuted)Mute();
        }


        private void GameNotificationHandler(string notification)
        {
            if (notification == GameNotification.MuteSound) Mute();
            if (_mainSource.mute) return;

            switch (notification)
            {
                case GameNotification.ScoreUpdated:
                    PlaySfx(Delete);
                    break;
                case GameState.End:
                case AudioNotification.Loos:
                    PlaySfx(Lose);
                    break;
                case AudioNotification.Stop:
                    PlaySfx(Stop);
                    break;
                case AudioNotification.Move:
                case AudioNotification.Click:
                    //avoiding doubling of a sound
                    if (_mainSource.clip == Turn && Time.time - _delay < 0.1F) return;
                    PlaySfx(Turn);//, notification == AudioNotification.Move ? 1.1F : 0.9F);
                    _delay = Time.time;
                    break;
            }
        }

        private void PlaySfx(AudioClip clip, float pitch = 1)
        {
            if (clip == null) return;
            //Prevent playing desebled audio source
            if(!_mainSource.isActiveAndEnabled) return;
            _mainSource.clip = clip;
            _mainSource.pitch = pitch;
            _mainSource.Play();
        }
    }
}