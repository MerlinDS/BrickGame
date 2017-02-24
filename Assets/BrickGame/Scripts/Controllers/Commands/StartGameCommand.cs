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

                Matrix<bool> fm, pm;
                //Has or hasn't restored data
                if (restoreModel.Has(session))
                {
                    //Restored start
                    RestoreModel.RestoredData data = restoreModel.Pop(session);
                    fm = data.Figure;
                    pm = data.Playground;
                    scoreDataProvider = new ScoreDataProvider(session, data.Lines,
                        data.Score, data.Level);
                }
                else
                {
                    //Clean start data
                    scoreDataProvider = new ScoreDataProvider(session, 0, 0, 1);
                    pm = new PlaygroundMatrix(playground.Rules.Width, playground.Rules.Height);
                    fm = new FigureMatrix();
                }
                Debug.LogFormat("Start new game session: {0}", playground.SessionName);
                //Send starting messages
                playground.SendMessage(MessageReceiver.UpdateMatix, pm,
                    SendMessageOptions.DontRequireReceiver);
                //Send model to childrens
                int n = playground.transform.childCount;
                for (int i = 0; i < n; i++)
                {
                    var child = playground.transform.GetChild(i);
                    child.SendMessage(MessageReceiver.UpdateMatix, pm,
                        SendMessageOptions.DontRequireReceiver);
                    child.SendMessage(MessageReceiver.UpdateFigure, fm,
                        SendMessageOptions.DontRequireReceiver);
                }
                //Clean score
                Context.Notify(GameNotification.ScoreUpdated, scoreDataProvider);
            }
        }
        //================================ Private|Protected methods ================================

    }
}