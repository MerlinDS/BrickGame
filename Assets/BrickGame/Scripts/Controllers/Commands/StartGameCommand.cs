// <copyright file="StartGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 13:07</date>

using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playground;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// StartGameCommand - execute starting of the game mode.
    /// This is clean start without cache restoring
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
            //
            foreach (PlaygroundController controller in Playgrounds)
            {
                string name = controller.name;
                GameRules rules = controller.Rules;
                PlaygroundModel model = new PlaygroundModel(controller.Width, controller.Height);
                //TODO: Restore playground from cashe
                controller.SendMessage(PlaygroundMessage.UpdateModel, model, SendMessageOptions.DontRequireReceiver);
                Context.Notify(GameNotification.ScoreUpdated, new ScoreDataProvider(rules, name, 0));
            }
        }

        //================================ Private|Protected methods ================================
    }
}