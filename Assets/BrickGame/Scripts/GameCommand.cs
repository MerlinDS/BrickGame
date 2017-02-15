// <copyright file="GameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:18</date>

using BrickGame.Scripts.Playground;
using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts
{
    /// <summary>
    /// GameCommand - helper for moka commands
    /// </summary>
    public abstract class GameCommand : MocaCommand<GameContext>
    {
        protected PlaygroundController[] Playgrounds
        {
            get
            {
                if(_playgrounds == null)
                    _playgrounds = Object.FindObjectsOfType<PlaygroundController>();
                return _playgrounds;
            }
        }

        private PlaygroundController[] _playgrounds;

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();
            _playgrounds = null;
        }
    }
    /// <summary>
    /// GameCommand - helper for moka commands
    /// </summary>
    public abstract class GameCommand<T> :  MocaCommand<GameContext, T> where T : DataProvider
    {

    }
}