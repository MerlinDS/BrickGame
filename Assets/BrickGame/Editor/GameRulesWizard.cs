// <copyright file="GameRulesWizard.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 20:18</date>

using BrickGame.Scripts.Models;
using UnityEditor;
using UnityEngine;

namespace BrickGame.Editor
{
    /// <summary>
    /// GameRulesWizard
    /// </summary>
    public class GameRulesWizard : AssetWizard
    {
        //================================       Public Setup       =================================
        [Header("View setup")]
        [Tooltip("Width of the playground in cells")]
        public int Width = 10;

        [Tooltip("Hight of the playground in cells")]
        public int Height = 20;

        [Tooltip("Sprite for bricks drawing")]
        public Sprite BricksSprite;

        [Tooltip("Sprite for drawing bricks in GUI")]
        public Sprite BricksImage;

        [Header("Game rules")]
        [Tooltip("Level divider")]
        [Range(1, 100)]
        public int LevelDivider = 10;
        [Tooltip("Score by deleted lines")]
        public int[] Score;

        [Header("Figures setup")]
        [Tooltip("Position of figure spawning")]
        public Vector2 SpawPosition = new Vector2(5, 0);

        [Tooltip("Starting speed of the figure falling")]
        [Range(0.5F, 2F)]
        public float StartingSpeed = 1F;

        [Tooltip("Speed increasing parameter for the figure falling")]
        [Range(0.01F, 2F)]
        public float SpeedIncreaser = 0.5F;

        [Header("Figures builder setup")]
        [Tooltip("Seed for figure creation randomness.")]
        public int Seed = 5;
        [Tooltip("List of glyphs for availble figures.")]
        public Glyph[] Figures;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        [MenuItem("Assets/Create/BrickGame/Create game rules")]
        static void CreateWizard()
        {
            DisplayWizard<GameRulesWizard>("Create Rules", "Create");
        }

        void OnWizardCreate()
        {
            GameRules rules = ScriptableObject.CreateInstance<GameRules>();
            rules.Width = Width;
            rules.Height = Height;
            rules.BricksImage = BricksImage;
            rules.BricksSprite = BricksSprite;
            rules.LevelDivider = LevelDivider;
            rules.Score = Score;
            rules.SpawPosition = SpawPosition;
            rules.StartingSpeed = StartingSpeed;
            rules.SpeedIncreaser = SpeedIncreaser;
            rules.Seed = Seed;
            rules.Figures = Figures;
            CreateAsset(rules);
        }

        void OnWizardUpdate()
        {
            helpString = "Please create rules for a game!";
        }

        //================================ Private|Protected methods ================================
    }
}