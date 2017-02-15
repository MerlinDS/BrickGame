// <copyright file="RestoreGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 17:15</date>

using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// RestoreGameCommand 
    /// </summary>
    public class RestoreGameCommand : GameCommand<SessionDataProvider>
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Execute()
        {
            CacheModel cacheModel = Context.GetActor<CacheModel>();
            if (!cacheModel.HasSession(Data.Rules.name, Data.Name)) return; //Nothing to restore
            int score, lines;
            cacheModel.GetScore(Data.Rules.name, Data.Name, out score, out lines);
            int level = Data.Rules.GetLevelByLines(lines);
            Debug.LogFormat("Restore game {0} for {1}: score = {2}, lines = {3}, level = {4}",
                Data.Rules.name, Data.Name, score, lines, level);
            ScoreModel scoreModel = Context.GetActor<ScoreModel>();
            scoreModel.UpdateSocre(Data.Name, score, level, lines);
            Context.Notify(GameNotification.ScoreUpdated);
        }

        //================================ Private|Protected methods ================================
    }
}