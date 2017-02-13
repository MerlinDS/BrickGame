// <copyright file="GameContext.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:12</date>

using BrickGame.Scripts.Models;
using BrickGame.Scripts.Utils.Input;
using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts
{
    /// <summary>
    /// GameContext - global game context
    /// </summary>
    public sealed class GameContext : MocaContext
    {

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected internal override void Initialize()
        {
            //TODO: Add touch input
            IInputAdapter adapter = Input.touchSupported
                ?  (IInputAdapter) new TouchInputAdapter()
                : new MouseInputAdapter();
            Debug.Log("Input adapter was intialized: " + adapter);
            MapInstance(adapter, typeof(IInputAdapter));
            MapInstance<ScoreModel>();
        }
    }
}