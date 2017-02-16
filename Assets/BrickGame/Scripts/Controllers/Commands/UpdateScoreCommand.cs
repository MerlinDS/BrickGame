// <copyright file="UpdateScoreCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 14:05</date>

using BrickGame.Scripts.Models;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// UpdateScoreCommand
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
            ScoreModel model = Context.GetActor<ScoreModel>();
            if (Data.Count == 0)
            {
                model.UpdateSocre(Data.Name, 0, 1, 0);
                return;
            }
            int score = model[ScoreModel.FieldName.Score, Data.Name] +
                        Data.Rules.CalculateScore(Data.Count);
            int lines = model[ScoreModel.FieldName.Lines, Data.Name] + Data.Count;
            int level = Data.Rules.GetLevelByLines(lines);
            model.UpdateSocre(Data.Name, score, level, lines);
        }
        //================================ Private|Protected methods ================================
    }
}