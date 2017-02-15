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
    /// StartGameCommand
    /// </summary>
    public class StartGameCommand : GameCommand
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Execute()
        {
            PlaygroundController[] findObjectsOfType = Object.FindObjectsOfType<PlaygroundController>();
            foreach (PlaygroundController controller in findObjectsOfType)
            {
                PlaygroundModel model = new PlaygroundModel(controller.Width, controller.Height);
                //TODO: Restore playground from cashe
                controller.SendMessage(PlaygroundMessage.UpdateModel, model, SendMessageOptions.DontRequireReceiver);
            }
        }

        //================================ Private|Protected methods ================================
    }
}