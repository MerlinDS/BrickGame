// <copyright file="GameRules.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 20:00</date>

using System;
using BrickGame.Scripts.Playgrounds.Strategies;
using BrickGame.Scripts.Utils;
using UnityEngine;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// GameRules - model of the game rules
    /// </summary>
    public class GameRules : ScriptableObject
    {
        //================================       Public Setup       =================================
        [Header("View setup")]
        [Tooltip("Width of the playground in cells")]
        public int Width = 10;

        [Tooltip("Hight of the playground in cells")]
        public int Height;

        [Tooltip("Sprite for bricks drawing")]
        public Sprite BricksSprite;

        [Tooltip("Sprite for drawing bricks in GUI")]
        public Sprite BricksImage;

        [Header("Game rules")]

        [Tooltip("Level divider")]
        [Range(1, 100)]
        public int LevelDivider;

        [Tooltip("Game mode strategies")]
        public StrategySetup[] Strategies;

        [Tooltip("Score by deleted lines")]
        public int[] Score;


        [Header("Figures behaviour")]
        [Tooltip("Position of figure spawning")]
        public Vector2 SpawPosition;

        [Tooltip("Direction of a figure falling")]
        public VerticalDirection FallingDirection = VerticalDirection.Down;

        [Tooltip("Starting speed of the figure falling")]
        [Range(0.5F, 2F)]
        public float StartingSpeed;

        [Tooltip("Speed increasing parameter for the figure falling")]
        [Range(0.01F, 2F)]
        public float SpeedIncreaser;

        [Header("Figures builder setup")]
        [Tooltip("Seed for figure creation randomness.")]
        public int Seed;
        [Tooltip("List of glyphs for availble figures.")]
        public Glyph[] Figures;

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        //Score calculation
        /// <summary>
        /// Calculate score from game rules
        /// </summary>
        /// <param name="lines">Count of removed lines</param>
        /// <returns>Output of the calculated score</returns>
        /// <exception cref="ArgumentException">Out of the rules score index</exception>
        public int CalculateScore(int lines)
        {
            if (lines == 0) return 0;
            if (lines >= Score.Length)
                throw new ArgumentException("Out of the rules score index");
            return Score[lines - 1];
        }

        public int GetLevel(int lines)
        {
            return 1 + (int) Math.Floor((float)lines / LevelDivider);
        }

        public float GetSpeed(int level)
        {
            if (level > 0) level -= 1;
            return StartingSpeed + level * SpeedIncreaser;
        }

        public float GetSpeedByLines(int lines)
        {
            return GetSpeed(GetLevel(lines));
        }
        //================================ Private|Protected methods ================================
    }
}