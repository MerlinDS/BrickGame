// <copyright file="AnalyticsService.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/01/2017 12:34</date>

using System.Collections.Generic;
using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.Models;

namespace BrickGame.Scripts.Services.Analytics
{
    /// <summary>
    /// AnalyticsService
    /// </summary>
    public class AnalyticsService : GameManager
    {
        private const string ModeChangedEvent = "Mode";
        private const string SessionStartEvent = "SessionStart";
        private const string SessionEndEvent = "SessionEnd";
        private const string PaletteChangedEvent = "Palette";
        private const string AudioChangedEvent = "Audio";

        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private ScoreModel _socreModel;

        private ColorPaletteManager _palette;
        private AudioController _audio;

        private GameModeManager _modes;
        //================================      Public methods      =================================
        //================================ Private|Protected methods ================================

        private void Start()
        {
            _socreModel = Context.GetActor<ScoreModel>();
            _audio = Context.GetActor<AudioController>();
            _palette = Context.GetActor<ColorPaletteManager>();
            _modes = Context.GetActor<GameModeManager>();
            Context.AddListener(GameState.Start, Handler);
            Context.AddListener(GameState.End, Handler);
            Context.AddListener(GameNotification.ColorChanged, Handler);
            Context.AddListener(GameNotification.ModeChanged, Handler);
            Context.AddListener(GameNotification.MuteSound, Handler);
        }

        private void Handler(string s)
        {
            switch (s)
            {
                case GameState.Start:
                    SessionStart();
                    break;
                case GameState.End:
                    SessionEnd();
                    break;
                case GameNotification.ColorChanged:
                    PaletteChanged();
                    break;
                case GameNotification.ModeChanged:
                    ModeChanged();
                    break;
                case GameNotification.MuteSound:
                    AudioMuted();
                    break;
            }
        }


        private void OnDestroy()
        {
            if (!Context.HasListener(GameState.Start, Handler)) return;
            Context.RemoveListener(GameState.Start, Handler);
            Context.RemoveListener(GameState.End, Handler);
            Context.RemoveListener(GameNotification.ColorChanged, Handler);
            Context.RemoveListener(GameNotification.ModeChanged, Handler);
            Context.RemoveListener(GameNotification.MuteSound, Handler);
        }


        private void SessionStart()
        {
            UnityEngine.Analytics.Analytics.CustomEvent(SessionStartEvent, new Dictionary<string, object>
            {
                {"mode", _modes.Index}
            });
        }

        private void SessionEnd()
        {
            UnityEngine.Analytics.Analytics.CustomEvent(SessionEndEvent, new Dictionary<string, object>
            {
                {"mode", _modes.Index},
                {"score", _socreModel[ScoreModel.FieldName.Score]},
                {"level", _socreModel[ScoreModel.FieldName.Level]},
                {"lines", _socreModel[ScoreModel.FieldName.Lines]}
                //TODO: Add combos
            });
        }

        private void ModeChanged()
        {
            UnityEngine.Analytics.Analytics.CustomEvent(ModeChangedEvent, new Dictionary<string, object>
            {
                {"previous", _modes.PreviousIndex},
                {"current", _modes.Index}
            });
        }

        private void PaletteChanged()
        {
            UnityEngine.Analytics.Analytics.CustomEvent(PaletteChangedEvent, new Dictionary<string, object>
            {
                {"palette", _palette.ColorPaletteIndex}
            });
        }

        private void AudioMuted()
        {
            UnityEngine.Analytics.Analytics.CustomEvent(AudioChangedEvent, new Dictionary<string, object>
            {
                {"muted", _audio.Muted}
            });
        }
    }
}