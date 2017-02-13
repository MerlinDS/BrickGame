// <copyright file="GameManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:30</date>

using MiniMoca.Helpers;

namespace BrickGame.Scripts
{
    /// <summary>
    /// GameManager - basic game manager class.
    /// Use this class insted of MonoBehaviour where it will be added as actor to context
    /// </summary>
    public abstract class GameManager : MocaManager<GameContext>
    {

    }
}