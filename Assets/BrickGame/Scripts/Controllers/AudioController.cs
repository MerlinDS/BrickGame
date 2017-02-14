// <copyright file="AudioController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:52</date>

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
        [Tooltip("Turn figure sound")] public AudioClip Turn;

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

        //================================ Private|Protected methods ================================
        private void Awake()
        {
            _mainSource = GetComponent<AudioSource>();
            Context.AddListener(GameNotification.ScoreUpdated, GameNotificationHandler);
            Context.AddListener(GameNotification.EndOfGame, GameNotificationHandler);
            Context.AddListener(GameNotification.FigureTurned, GameNotificationHandler);
            Context.AddListener(GameNotification.FigureMoved, GameNotificationHandler);
            Context.AddListener(GameNotification.MuteSound, GameNotificationHandler);
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameNotification.ScoreUpdated, GameNotificationHandler);
            Context.RemoveListener(GameNotification.EndOfGame, GameNotificationHandler);
            Context.RemoveListener(GameNotification.FigureTurned, GameNotificationHandler);
            Context.RemoveListener(GameNotification.FigureMoved, GameNotificationHandler);
            Context.RemoveListener(GameNotification.MuteSound, GameNotificationHandler);
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
                case GameNotification.EndOfGame:
                    PlaySfx(Lose);
                    break;
                case GameNotification.FigureTurned:
                case GameNotification.FigureMoved:
                    //avoiding doubling of a sound
                    if (_mainSource.clip == Turn && Time.time - _delay < 0.1F) return;
                    PlaySfx(Turn, notification == GameNotification.FigureMoved ? 1.1F : 0.9F);
                    _delay = Time.time;
                    break;
            }
        }

        private void PlaySfx(AudioClip clip, float pitch = 1)
        {
            if (clip == null) return;
            _mainSource.clip = clip;
            _mainSource.pitch = pitch;
            _mainSource.Play();
        }
    }
}