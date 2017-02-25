// <copyright file="GameProgress.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/25/2017 6:28</date>
namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// GameProgress - structure for collecting and holding game progress data
    /// </summary>
    public struct GameProgress
    {
        /// <summary>
        /// Current level of the game mode
        /// </summary>
        public int Level;
        /// <summary>
        /// Removed lines on the playground
        /// </summary>
        public int Lines;
        /// <summary>
        /// Current score of the game mode
        /// </summary>
        public int Score;
        /// <summary>
        /// Combination multiplier
        /// </summary>
        public int Multiplier;
    }
}