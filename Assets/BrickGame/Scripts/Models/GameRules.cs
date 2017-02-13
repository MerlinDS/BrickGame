// <copyright file="GameRules.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 20:00</date>

using System;
using UnityEngine;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// GameRules - model of the game rules
    /// </summary>
    public class GameRules : ScriptableObject
    {
        //================================       Public Setup       =================================
        [Tooltip("Level divider")]
        public int LevelDivider;

        [Tooltip("Starting speed of the game")]
        public float StartingSpeed;

        [Tooltip("Score by deleted lines")]
        public int[] Score;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <summary>
        /// Calculate score from game rules
        /// </summary>
        /// <param name="lines">Count of removed lines</param>
        /// <returns>Output of the calculated score</returns>
        /// <exception cref="ArgumentException">Out of the rules score index</exception>
        public int CalculateScore(int lines)
        {
            if (lines > Score.Length)
                throw new ArgumentException("Out of the rules score index");
            return Score[lines - 1];
        }

        public float GetSpeed(int level)
        {
            float speed = StartingSpeed - level * 0.1F;
            if (speed < 0) speed = 0;
            return speed;
        }
        //================================ Private|Protected methods ================================
    }
}