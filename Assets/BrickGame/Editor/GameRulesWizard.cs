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
        [Tooltip("Level divider")]
        public int LevelDivider;

        [Tooltip("Starting speed of the game")]
        public float StartingSpeed;

        [Tooltip("Score by deleted lines")]
        public int[] Score;
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
            rules.LevelDivider = LevelDivider;
            rules.StartingSpeed = StartingSpeed;
            rules.Score = Score;
            CreateAsset(rules);
        }

        void OnWizardUpdate()
        {
            helpString = "Please create rules for a game!";
        }

        //================================ Private|Protected methods ================================
    }
}