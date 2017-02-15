// <copyright file="ScoreModel.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 19:14</date>

using System.Collections.Generic;
using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// ScoreModel - model of a game score
    /// </summary>
    public class ScoreModel : IMocaActor
    {
        /// <summary>
        /// Helper for GameIndicator
        /// </summary>
        public enum FieldName
        {
            Score,
            Lines,
            Level
        }

        private struct ScoreHolder
        {
            public readonly int Level;
            public readonly int Lines;
            public readonly int Score;

            /// <inheritdoc />
            public ScoreHolder(int level, int lines, int score)
            {
                Level = level;
                Lines = lines;
                Score = score;
            }
        }

        //================================       Public Setup       =================================
        /// <summary>
        /// Get galobal (sum of values) value by name of the filed in model
        /// </summary>
        /// <param name="name">Name of the field in model</param>
        public int this[FieldName name]
        {
            get
            {
                int value = 0;
                foreach (KeyValuePair<string,ScoreHolder> valuePair in _scores)
                    value += this[name, valuePair.Key];
                return value;
            }
        }

        /// <summary>
        /// Get value by name of the filed in model
        /// </summary>
        /// <param name="name">Name of the field in model</param>
        /// <param name="id">Id of playground</param>
        public int this[FieldName name, string id]
        {
            get
            {
                if (!_scores.ContainsKey(id)) return 0;

                switch (name)
                {
                    case FieldName.Level:
                        return _scores[id].Level;
                    case FieldName.Score:
                        return _scores[id].Score;
                    case FieldName.Lines:
                        return _scores[id].Lines;
                }
                Debug.LogWarning("Unknown score field name");
                return 0;
            }
        }

        //================================    Systems properties    =================================
        private readonly Dictionary<string, ScoreHolder> _scores;
        //================================      Public methods      =================================

        /// <inheritdoc />
        public ScoreModel()
        {
            _scores = new Dictionary<string, ScoreHolder>();
        }

        public void UpdateSocre(string id, int score, int level, int lines)
        {
            if (level <= 0) level = 1;
            ScoreHolder holder = new ScoreHolder(level, lines, score);
            if (!_scores.ContainsKey(id))
            {
                _scores.Add(id, holder);
                return;
            }
            _scores[id] = holder;
        }

        //================================ Private|Protected methods ================================
    }
}