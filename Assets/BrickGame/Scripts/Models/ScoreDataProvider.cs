// <copyright file="ScoreDataProvider.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 13:53</date>

using BrickGame.Scripts.Controllers.Commands;
using MiniMoca;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// ScoreDataProvider - data provider for score model.
    /// <seealso cref="ScoreModel"/>
    /// <seealso cref="UpdateScoreCommand"/>
    /// </summary>
    public class ScoreDataProvider : DataProvider
    {
        //================================       Public Setup       =================================
        /// <summary>
        /// If true score need to be replaced in score mode.
        /// </summary>
        public readonly bool Replacement;
        /// <summary>
        /// New progress of the game mode
        /// </summary>
        public readonly GameProgress Progress;
        /// <summary>
        /// Name of the playground instance
        /// </summary>
        public readonly string Session;
        //================================    Systems properties    =================================


        //================================      Public methods      =================================
        public ScoreDataProvider(string session, GameProgress progress)
        {
            Session = session;
            Progress = progress;
            Replacement = true;
        }

        public ScoreDataProvider(string session)
        {
            Session = session;
            Progress = new GameProgress {Level = 1};
        }

        public ScoreDataProvider(string session, int lines)
        {
            Session = session;
            Progress = new GameProgress {Lines = lines};
        }

        /// <inheritdoc />
        public override void Dispose()
        {

        }

        //================================ Private|Protected methods ================================
    }
}