// <copyright file="UpdateCacheCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:17</date>

using System.Text;
using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playground;
using BrickGame.Scripts.Utils;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
            Debug.LogWarning("Not yet implemented!");
            if (Notification == GameNotification.ScoreUpdated) return;//Just update score

            /*cacheModel.UpdateScore(scoreModel.ModelName, scoreModel.Score, scoreModel.Lines);

            if (Notification == PlaygroundNotification.End)
            {
                //Clean playground cache
                cacheModel.CleanPlayground(scoreModel.ModelName);
                return;
            }

            //Save current state of the playground for current mode
            StringBuilder sb = new StringBuilder();
            //sb.Append("");//TODO: Add header
            ConvertToString(sb);
            cacheModel.UpdatePlayground(scoreModel.ModelName, sb.ToString());*/

        }
        //================================ Private|Protected methods ================================
        private void ConvertToString(StringBuilder sb)
        {
            /*PlaygroundController[] _controllers = Object.FindObjectsOfType<PlaygroundController>();
            int i, n = _controllers.Length;
            for (i = 0; i < n; i++)
            {
                PlaygroundModel model = _controllers[i].Model;
                FigureController controller = _controllers[i].GetComponent<FigureController>();
                sb.Append(DataConverter.ToString(model, controller.FigureIndexes()));
            }*/
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

    }
}