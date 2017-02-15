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
        /// Update current score of the game
        /// </summary>
        public const string ScoreUpdated = "GameNotification::ScoreUpdated";
        /// <summary>
        /// Mute or unmute sound
        /// </summary>
        public const string MuteSound = "GameNotification::MuteSound";
    }

    /// <summary>
    /// Playground external notifications
    /// </summary>
    public static class PlaygroundNotification
    {

        /// <summary>
        /// Start new game
        /// </summary>
        public const string Start = "PlaygroundNotification::Start";
        /// <summary>
        /// The game that was previously started is ended.
        /// </summary>
        public const string End = "PlaygroundNotification::End";

        /// <summary>
        /// Set playground on pause or resume it.
        /// </summary>
        public const string Pause = "PlaygroundNotification:Pause";
    }

    /// <summary>
    /// Notifications of a figures managers
    /// </summary>
    public static class FigureNotification
    {
        /// <summary>
        /// A new figure was added to playground. (Old one was destroyed)
        /// </summary>
        public const string Changed = "FigureNotification::Changed";
        /// <summary>
        /// Figure was turned
        /// </summary>
        public const string Turned = "FigureNotification::Turned";
        /// <summary>
        /// Figure was turned
        /// </summary>
        public const string Moved = "FigureNotification::Moved";
    }
}