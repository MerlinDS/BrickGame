// <copyright file="GameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:18</date>

using BrickGame.Scripts.Playgrounds;
using JetBrains.Annotations;
using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts
{
    /// <summary>
    /// GameCommand - helper for moka commands
    /// </summary>
    public abstract class GameCommand : MocaCommand<GameContext>
    {
        protected Playground[] Playgrounds
        {
            get
            {
                return _playgrounds ?? (_playgrounds = Object.FindObjectsOfType<Playground>());
            }
        }

        [CanBeNull]
        private Playground[] _playgrounds;

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