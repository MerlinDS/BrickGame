// <copyright file="GameInitManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:17</date>

using MiniMoca.Helpers;

namespace BrickGame.Scripts
{
    /// <summary>
    /// GameInitManager - Game initialization manager.
    /// First class that awakes on a startup of the game.
    /// This class will register global GameContext
    /// </summary>
    public sealed class GameInitManager : MocaInitManager
    {
        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void Initialize()
        {
            RegisterContext(new GameContext());
        }
    }
}