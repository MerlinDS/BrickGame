// <copyright file="GameNotification.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:14</date>
namespace BrickGame.Scripts
{
    /// <summary>
    /// GameNotification - global game notifications list (of const)
    /// </summary>
    public static class GameNotification
    {
        /// <summary>
        /// Start new game
        /// </summary>
        public const string Start = "GameNotification::Start";
        public const string EndOfGame = "GameNotification::EndOfGame";
        public const string ScoreUpdated = "GameNotification::ScoreUpdated";
        public const string FigureChanged = "GameNotification::FigureChanged";
    }
}