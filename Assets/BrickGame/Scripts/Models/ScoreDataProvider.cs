// <copyright file="ScoreDataProvider.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 13:53</date>

using MiniMoca;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// ScoreDataProvider
    /// </summary>
    public class ScoreDataProvider : DataProvider
    {
        //================================       Public Setup       =================================
        public readonly int Level;

        public readonly bool Replacement;

        public readonly int Score;
        /// <summary>
        /// Count of removed lines
        /// </summary>
        public readonly int Count;
        /// <summary>
        /// Name of the playground instance
        /// </summary>
        public readonly string Session;
        //================================    Systems properties    =================================


        //================================      Public methods      =================================
        public ScoreDataProvider(string session, int count = 0, int score = 0, int level = 1, bool replacement = false)
        {
            Session = session;
            Count = count;
            Score = score;
            Level = level;
            Replacement = replacement;
        }

        /// <inheritdoc />
        public override void Dispose()
        {

        }

        //================================ Private|Protected methods ================================
    }
}