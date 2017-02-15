// <copyright file="GameContext.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:12</date>

using BrickGame.Scripts.Controllers.Commands;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Utils.Input;
using MiniMoca;
using UnityEngine;

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
            //Add user input adapter as an actor to context
            IInputAdapter adapter = Input.touchSupported
                ?  (IInputAdapter) new TouchInputAdapter()
                : new MouseInputAdapter();
            Debug.Log("Input adapter was intialized: " + adapter);
            MapInstance(adapter, typeof(IInputAdapter));
            //Add score model to context
            MapInstance<ScoreModel>();
            MapInstance<CacheModel>();
            //Map commands
            CommandMap.MapCommand<UpdateCacheCommand>(PlaygroundNotification.Start);
            CommandMap.MapCommand<UpdateCacheCommand>(GameNotification.MuteSound);
            CommandMap.MapCommand<UpdateCacheCommand>(PlaygroundNotification.End);
            CommandMap.MapCommand<UpdateCacheCommand>(GameNotification.ScoreUpdated);
        }
    }
}