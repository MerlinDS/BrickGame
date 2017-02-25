// <copyright file="UpdateScoreCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 14:05</date>

using System.Linq;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// UpdateScoreCommand - calculate score and update score model.
    /// Also set speed for figure depending on calculate level.
    /// </summary>
    public class UpdateScoreCommand : GameCommand<ScoreDataProvider>
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Execute()
        {
            if (Data == null) return;
            Playground playground = Playgrounds.FirstOrDefault(p => p.SessionName == Data.Session);
            if (playground == null)
            {
                Debug.LogErrorFormat("Playground with session {0} was not found", Data.Session);
                return;
            }

            GameRules rules = playground.Rules;
            if (rules == null)
            {
                Debug.LogWarning("Playground has no rules!");
                return;
            }

            ScoreModel model = Context.GetActor<ScoreModel>();
            //Replace score with new progress
            if (Data.Replacement)
            {
                model.UpdateSocre(Data.Session, Data.Progress);
                playground.BroadcastMessage(MessageReceiver.AccelerateFigure,
                    rules.GetSpeed(Data.Progress.Level));
                playground.TotalLines = Data.Progress.Lines;
                return;
            }
            //Calculate score
            int score = rules.CalculateScore(Data.Progress.Lines)
                        + model[ScoreModel.FieldName.Score, Data.Session];
            int level = rules.GetLevel(playground.TotalLines);
            model.UpdateSocre(Data.Session, score, level, playground.TotalLines);
        }
        //================================ Private|Protected methods ================================
    }
}