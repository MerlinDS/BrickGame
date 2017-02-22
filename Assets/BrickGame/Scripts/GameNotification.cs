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
        /// UpdateColors current score of the game
        /// </summary>
        public const string ScoreUpdated = "GameNotification::ScoreUpdated";
        /// <summary>
        /// Mute or unmute sound
        /// </summary>
        public const string MuteSound = "GameNotification::MuteSound";

        /// <summary>
        /// Application color palette was changed
        /// </summary>
        public const string ColorChanged = "GameNotification:ColorChanged";

        /// <summary>
        /// Application mode was changed
        /// </summary>
        public const string ModeChanged = "GameNotification:ModeChanged";
    }
}