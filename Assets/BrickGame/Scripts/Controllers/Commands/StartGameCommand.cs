// <copyright file="StartGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 13:07</date>

using BrickGame.Scripts.Playground;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// StartGameCommand - execute starting of the game mode.
    /// This command will create new model for playgrounds and tries to restore them from cache
    /// </summary>
    public class StartGameCommand : GameCommand
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Execute()
        {
            PlaygroundController[] controllers = Object.FindObjectsOfType<PlaygroundController>();
            if (controllers == null || controllers.Length == 0)
            {
                Debug.LogError("Playground controllers was not found!");
                return;
            }
            //
            foreach (PlaygroundController controller in controllers)
            {
                PlaygroundModel model = new PlaygroundModel(controller.Width, controller.Height);
                //TODO: Restore playground from cashe
                controller.SendMessage(PlaygroundMessage.UpdateModel, model, SendMessageOptions.DontRequireReceiver);
            }
        }

        //================================ Private|Protected methods ================================
    }
}