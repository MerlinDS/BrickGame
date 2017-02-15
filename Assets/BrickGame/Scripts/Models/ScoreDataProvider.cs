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
        /// <summary>
        /// Name of the playground instance
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Count of removed lines
        /// </summary>
        public readonly int Count;

        /// <summary>
        /// Current rules
        /// </summary>
        public readonly GameRules Rules;
        //================================    Systems properties    =================================


        //================================      Public methods      =================================

        /// <inheritdoc />
        public ScoreDataProvider(GameRules rules, string name, int count)
        {
            Rules = rules;
            Name = name;
            Count = count;
        }

        /// <inheritdoc />
        public override void Dispose()
        {

        }

        //================================ Private|Protected methods ================================
    }
}