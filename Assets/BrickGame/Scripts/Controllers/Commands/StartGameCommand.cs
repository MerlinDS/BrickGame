// <copyright file="StartGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 13:07</date>

using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds;
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
            var matrix = new PlaygroundMatrix(10, 20);
            var playgrounds = Object.FindObjectsOfType<Playground>();
            foreach (var playground in playgrounds)
            {
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

            }
            /*if (Playgrounds == null || Playgrounds.Length == 0)
            {
                Debug.LogError("Playground controllers was not found!");
                return;
            }
            ScoreModel scoreModel = Context.GetActor<ScoreModel>();
            RestoreModel restoreModel = Context.GetActor<RestoreModel>();
            foreach (PlaygroundController controller in Playgrounds)
            {
                int[] figureMatrix = null;
                string name = controller.name;
                GameRules rules = controller.Rules;

                PlaygroundModel model;
                ScoreDataProvider scoreDataProvider = null;
                if (restoreModel.Has(name))
                {
                    //Restored start
                    RestoreModel.RestoredData data = restoreModel.Pop(controller.name);
                    figureMatrix = data.FigureMatrix;
                    model = new PlaygroundModel(controller.Width, controller.Height, data.Lines, data.Matrix);
                    scoreModel.UpdateSocre(controller.name, data.Score, data.Level, data.Lines);
                }
                else
                {
                    //Clean start
                    model = new PlaygroundModel(controller.Width, controller.Height);
                    scoreDataProvider = new ScoreDataProvider(rules, name, 0);
                }

                controller.SendMessage(PlaygroundMessage.UpdateModel, model, SendMessageOptions.DontRequireReceiver);
                Context.Notify(GameNotification.ScoreUpdated, scoreDataProvider);
                //Create first figureMatrix or restore old one
                if(figureMatrix == null)controller.SendMessage(PlaygroundMessage.CreateFigure);
                else controller.SendMessage(PlaygroundMessage.RestoreFigure, figureMatrix);
            }*/
        }

        //================================ Private|Protected methods ================================
    }
}