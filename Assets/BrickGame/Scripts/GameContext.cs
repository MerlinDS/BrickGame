﻿// <copyright file="GameContext.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:12</date>

using BrickGame.Scripts.Controllers.Commands;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Models.Session;
using BrickGame.Scripts.Utils.Input;
using MiniMoca;

namespace BrickGame.Scripts
{
    /// <summary>
    /// GameContext - global game context class.
    /// </summary>
    public sealed class GameContext : MocaContext
    {

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected internal override void Initialize()
        {
            MapInstance(new InputManager());
            //Add score model to context
            MapInstance<CacheModel>();
            MapInstance<RestoreModel>();
            MapInstance<ScoreModel>();
            MapInstance<SessionModel>();
            //Map commands
            CommandMap.MapCommand<RestoreGameCommand>(StateNotification.Restore);
            CommandMap.MapCommand<UpdateScoreCommand>(GameNotification.ScoreUpdated);
            CommandMap.MapCommand<StartGameCommand>(StateNotification.Start);
            //Session command
            CommandMap.MapCommand<StateChangeCommand>(StateNotification.Start);
            CommandMap.MapCommand<StateChangeCommand>(StateNotification.End);
            CommandMap.MapCommand<StateChangeCommand>(StateNotification.Pause);
            //Cache command
            CommandMap.MapCommand<UpdateCacheCommand>(GameNotification.MuteSound);
            CommandMap.MapCommand<UpdateCacheCommand>(GameNotification.ColorChanged);
            CommandMap.MapCommand<UpdateCacheCommand>(StateNotification.End);
            CommandMap.MapCommand<UpdateCacheCommand>(StateNotification.Pause);
            CommandMap.MapCommand<UpdateCacheCommand>(GameNotification.ScoreUpdated);
        }
    }
}