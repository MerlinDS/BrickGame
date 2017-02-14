// <copyright file="UpdateCacheCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:17</date>

using System.Text;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playground;
using BrickGame.Scripts.Utils;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// UpdateCacheCommand - update cache of the game.
    /// <para>
    ///     Executed on GameNotification.EndOfGame, GameNotification.MuteSound, GameNotification.ScoreUpdated
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

            //Update score
            ScoreModel scoreModel = Context.GetActor<ScoreModel>();
            cacheModel.UpdateModeScore(scoreModel.ModelName, scoreModel.Score, scoreModel.Lines);

            if (Notification == GameNotification.ScoreUpdated) return;//Just update score
            if (Notification == GameNotification.EndOfGame)
            {
                //Clean playground cache
                cacheModel.CleanPlayground(scoreModel.ModelName);
                return;
            }

            //Save current state of the playground for current mode
            StringBuilder sb = new StringBuilder();
            sb.Append("");//TODO: Add header
            ConvertToString(sb);
            cacheModel.UpdatePlayground(scoreModel.ModelName, sb.ToString());

        }
        //================================ Private|Protected methods ================================
        private void ConvertToString(StringBuilder sb)
        {
            PlaygroundController[] playgrounds = Object.FindObjectsOfType<PlaygroundController>();
            int i, n = playgrounds.Length;
            for (i = 0; i < n; i++)
            {
                PlaygroundModel model = playgrounds[i].Model;
                sb.Append(DataConverter.ToString(model));
            }
        }

    }
}