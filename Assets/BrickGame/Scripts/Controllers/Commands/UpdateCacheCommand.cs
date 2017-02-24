// <copyright file="UpdateCacheCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:17</date>

using System.Text;
using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds;
using BrickGame.Scripts.Utils;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// UpdateCacheCommand - update cache of the game.
    /// <para>
    ///     Executed on GameNotification.End, GameNotification.MuteSound, GameNotification.ScoreUpdated
    /// </para>
    /// <para>
    ///     <see cref="GameNotification"/>
    ///     <see cref="CacheModel"/>
    /// </para>
    /// </summary>
    public class UpdateCacheCommand : GameCommand
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Execute()
        {
            CacheModel cacheModel = Context.GetActor<CacheModel>();
            if (Notification == GameNotification.ColorChanged)
            {
                ColorPalleteManager colorPalleteManager = Context.GetActor<ColorPalleteManager>();
                cacheModel.ColorPaletteIndex = colorPalleteManager.ColorPaletteIndex;
                return;
            }
            if (Notification == GameNotification.MuteSound)
            {
                //UpdateColors audio cache
                //AudioController audioController = Context.GetActor<AudioController>();
                cacheModel.AudioMuted = !cacheModel.AudioMuted;
                return;
            }

            ScoreUpdate(cacheModel);
            if (Notification == GameNotification.ScoreUpdated) return;//Just update score
            if (Notification == GameState.End)
            {
                CleanCache(cacheModel);
                return;
            }
            SaveMatrices(cacheModel);

        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// UpdateColors score
        /// </summary>
        private void ScoreUpdate(CacheModel cacheModel)
        {
            ScoreModel scoreModel = Context.GetActor<ScoreModel>();
            int i, n = Playgrounds.Length;
            for (i = 0; i < n; i++)
            {
                GameRules rules = Playgrounds[i].Rules;
                if (rules == null) continue; //Do not save score for game without rules
                string name = Playgrounds[i].SessionName;
                cacheModel.UpdateScore(rules.name, name,
                    scoreModel[ScoreModel.FieldName.Score, name],
                    scoreModel[ScoreModel.FieldName.Lines, name],
                    scoreModel[ScoreModel.FieldName.Level, name]);
            }
        }
        /// <summary>
        /// Save playgrounds matrices to cache for current session
        /// </summary>
        /// <param name="cacheModel"></param>
        private void SaveMatrices(CacheModel cacheModel)
        {
            StringBuilder sb = new StringBuilder();
            int i, n = Playgrounds.Length;
            for (i = 0; i < n; i++)
            {
                sb.Remove(0, sb.Length);
                Playground pc = Playgrounds[i];
                if(pc.Rules == null)continue;
                bool[] matrix = pc.Matrix;
                FigureMatrix figure = pc.GetComponentInChildren<Figure>().Matrix;
                sb.Append(DataConverter.ToString(new[] {figure.x, figure.y, figure.Width, figure.Height}));
                sb.Append(DataConverter.ToString(figure));
                sb.Append(DataConverter.ToString(matrix));
                cacheModel.UpdatePlayground(pc.Rules.name, pc.SessionName, sb.ToString());
            }
        }
        /// <summary>
        /// Clean cache from playground
        /// </summary>
        /// <param name="cacheModel"></param>
        private void CleanCache(CacheModel cacheModel)
        {
            int i, n = Playgrounds.Length;
            for (i = 0; i < n; i++)
            {
                string name = Playgrounds[i].SessionName;
                GameRules rules = Playgrounds[i].Rules;
                if(rules == null)continue;
                cacheModel.UpdateScore(rules.name, name, 0, 0, 1);
                cacheModel.CleanPlayground(rules.name, name);
            }
        }
    }
}