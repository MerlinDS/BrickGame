// <copyright file="ScoreModel.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 19:14</date>

using System;
using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// ScoreModel
    /// </summary>
    public class ScoreModel : IMocaActor
    {

        //================================       Public Setup       =================================
        /// <summary>
        /// Helper for GameIndicator
        /// </summary>
        public enum FieldName
        {
            Score,
            Lines,
            Level
        }

        /// <summary>
        /// Get value by name of the filed in model
        /// </summary>
        /// <param name="name">Name of the field in model</param>
        public int this[FieldName name]
        {
            get
            {
                switch (name)
                {
                    case FieldName.Level:
                        return Level;
                    case FieldName.Score:
                        return Score;
                    case FieldName.Lines:
                        return Lines;
                }
                Debug.LogWarning("Unknown score field name");
                return 0;
            }
        }

        public int Score { get; private set; }
        public int Lines { get; private set; }
        public int Level { get; private set; }

        //================================    Systems properties    =================================
        private GameRules _rules;
        //================================      Public methods      =================================
        public void SetRules(GameRules rules)
        {
            _rules = rules;
        }

        public void Reset()
        {
            Score = 0;
            Lines = 0;
            Level = 1;
        }

        public void AddLines(int count)
        {
            int score = _rules.CalculateScore(count);
            if (score == 0) return;
            Lines += count;
            Score += score;
            Level = 1 + (int) Math.Floor((float)Lines / _rules.LevelDivider);
        }
        //================================ Private|Protected methods ================================
    }
}