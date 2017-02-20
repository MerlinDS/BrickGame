// <copyright file="StartGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 13:07</date>

using BrickGame.Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// StartGameCommand - execute starting of the game mode.
    /// </summary>
    public class StartGameCommand : GameCommand
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Execute()
        {
            if (Playgrounds == null || Playgrounds.Length == 0)
            {
                Debug.LogError("Playground controllers was not found!");
                return;
            }
            RestoreModel restoreModel = Context.GetActor<RestoreModel>();

            foreach (var playground in Playgrounds)
            {
                string session = playground.SessionName;
                ScoreDataProvider scoreDataProvider;

                bool[] restored = null;
                //Has or hasn't restored data
                if (restoreModel.Has(session))
                {
                    //Restored start
                    RestoreModel.RestoredData data = restoreModel.Pop(session);
                    restored = data.Matrix;
                    scoreDataProvider = new ScoreDataProvider(session, data.Lines, data.Score, data.Level);
                }
                else
                    scoreDataProvider = new ScoreDataProvider(session, 0, 0, 1);
                //Send starting messages
                PlaygroundMatrix matrix = CreateMatrix(playground.Rules, ref restored);
                playground.SendMessage(MessageReceiver.UpdateMatix, matrix,
                    SendMessageOptions.DontRequireReceiver);
                //Send model to childrens
                int n = playground.transform.childCount;
                for (int i = 0; i < n; i++)
                {
                    playground.transform.GetChild(i)
                        .SendMessage(MessageReceiver.UpdateMatix, matrix,
                            SendMessageOptions.DontRequireReceiver);
                }
                //Clean score
                Context.Notify(GameNotification.ScoreUpdated, scoreDataProvider);
            }
        }

        //================================ Private|Protected methods ================================

        /// <summary>
        /// Create new matrix depended on game rules and restored data
        /// </summary>
        /// <param name="rules">Game rules</param>
        /// <param name="restored">Playground data restored from cache</param>
        /// <returns>Instance of new Playground matrix</returns>
        [NotNull]
        private PlaygroundMatrix CreateMatrix([CanBeNull]GameRules rules, ref bool[] restored)
        {
            int width = 0, height = 0;
            if (rules != null)
            {
                width = rules.Width;
                height = rules.Height;
            }
            //Set default values if has no rules, or rules ar broken
            if (width == 0 || height == 0)
            {
                width = 10;//Default
                height = 20;//Default
            }
            //Remove restored data if broken
            PlaygroundMatrix pm = null;
            if (restored != null)
            {
                int i = width * height;
                int delta = restored.Length - i;
                if (delta > 0)
                {
                    bool[] data = new bool[i];
                    for (i = i - 1; i >= delta; i--)
                        data[i] = restored[i];
                    pm = new PlaygroundMatrix(data, width, height);
                }
            }

            return pm ?? new PlaygroundMatrix(width, height);
        }
    }
}