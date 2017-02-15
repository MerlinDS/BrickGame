// <copyright file="GameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:18</date>

using MiniMoca;

namespace BrickGame.Scripts
{
    /// <summary>
    /// GameCommand - helper for moka commands
    /// </summary>
    public abstract class GameCommand : MocaCommand<GameContext>
    {

    }
    /// <summary>
    /// GameCommand - helper for moka commands
    /// </summary>
    public abstract class GameCommand<T> : MocaCommand<GameContext, T> where T : DataProvider
    {

    }
}