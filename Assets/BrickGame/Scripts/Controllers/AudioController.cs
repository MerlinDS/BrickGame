// <copyright file="AudioController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:52</date>

using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds;
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
        [Tooltip("Turn figureMatrix sound")] public AudioClip Turn;

        /// <inheritdoc />
        public bool Muted
        {
            get { return _mainSource.mute; }
        }

        //================================    Systems properties    =================================
        private AudioSource _mainSource;

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
            }
            _mainSource.mute = !_mainSource.mute;
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
            _mainSource = GetComponent<AudioSource>();
            Context.AddListener(GameNotification.ScoreUpdated, GameNotificationHandler);
            Context.AddListener(PlaygroundNotification.End, GameNotificationHandler);
            Context.AddListener(FigureNotification.Turned, GameNotificationHandler);
            Context.AddListener(FigureNotification.Moved, GameNotificationHandler);
            Context.AddListener(GameNotification.MuteSound, GameNotificationHandler);
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameNotification.ScoreUpdated, GameNotificationHandler);
            Context.RemoveListener(PlaygroundNotification.End, GameNotificationHandler);
            Context.RemoveListener(FigureNotification.Turned, GameNotificationHandler);
            Context.RemoveListener(FigureNotification.Moved, GameNotificationHandler);
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
                case PlaygroundNotification.End:
                    PlaySfx(Lose);
                    break;
                case FigureNotification.Turned:
                case FigureNotification.Moved:
                    //avoiding doubling of a sound
                    if (_mainSource.clip == Turn && Time.time - _delay < 0.1F) return;
                    PlaySfx(Turn, notification == FigureNotification.Moved ? 1.1F : 0.9F);
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