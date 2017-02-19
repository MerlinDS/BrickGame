﻿namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// Notifications of a figures managers
    /// </summary>
    public static class FigureNotification
    {
        /// <summary>
        /// A new figureMatrix was added to playground. (Old one was destroyed)
        /// </summary>
        public const string Changed = "FigureNotification::Changed";
        /// <summary>
        /// FigureMatrix was turned
        /// </summary>
        public const string Turned = "FigureNotification::Turned";
        /// <summary>
        /// FigureMatrix was turned
        /// </summary>
        public const string Moved = "FigureNotification::Moved";
    }
}