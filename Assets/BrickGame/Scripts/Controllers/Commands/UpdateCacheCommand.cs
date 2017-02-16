// <copyright file="UpdateCacheCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:17</date>

using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playground;
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
            if (Notification == GameNotification.MuteSound)
            {
                //Update audio cache
                AudioController audioController = Context.GetActor<AudioController>();
                cacheModel.AudioMuted = audioController.Muted;
                return;
            }

            ScoreUpdate(cacheModel);
            if (Notification == GameNotification.ScoreUpdated) return;//Just update score
            if (Notification == PlaygroundNotification.End)
            {
                CleanCache(cacheModel);
                return;
            }
            SaveMatrices(cacheModel);

        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Save playgrounds matrices to cache for current session
        /// </summary>
        /// <param name="cacheModel"></param>
        private void SaveMatrices(CacheModel cacheModel)
        {
            int i, n = Playgrounds.Length;
            for (i = 0; i < n; i++)
            {
                PlaygroundController pc = Playgrounds[i];
                bool[] matrix = pc.Matrix;
                int[] figure = pc.GetComponent<FigureController>().FigureIndexes();
                string compressed = DataConverter.ToString(matrix, figure);
                cacheModel.UpdatePlayground(pc.Rules.name, pc.name, compressed);
            }
        }

        /// <summary>
        /// Update score
        /// </summary>
        private void ScoreUpdate(CacheModel cacheModel)
        {
            ScoreModel scoreModel = Context.GetActor<ScoreModel>();
            int i, n = Playgrounds.Length;
            for (i = 0; i < n; i++)
            {
                string name = Playgrounds[i].name;
                GameRules rules = Playgrounds[i].Rules;
                cacheModel.UpdateScore(rules.name, name,
                    scoreModel[ScoreModel.FieldName.Score, name],
                    scoreModel[ScoreModel.FieldName.Lines, name]);
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
                string name = Playgrounds[i].name;
                GameRules rules = Playgrounds[i].Rules;
                cacheModel.UpdateScore(rules.name, name, 0, 0);
                cacheModel.CleanPlayground(rules.name, name);
            }
        }

    }
}