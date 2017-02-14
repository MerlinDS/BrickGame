// <copyright file="MainMenuButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 12:08</date>

using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.UI.Components;
using UnityEngine;

namespace BrickGame.Scripts.UI
{
    //TODO:Rename
    /// <summary>
    /// MainMenuButton - Button for a game mode starting
    /// </summary>
    public class MainMenuButton : GameControlsButton
    {
        //================================       Public Setup       =================================
        [Tooltip("Scene to load on click")]
        public Object Scene;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        protected override void OnClickHandler()
        {
            if (Scene != null)
            {
                Context.GetActor<GameModesController>().StartMode(Scene.name);
                return;
            }
            Debug.LogWarning("Scene was not set yet! Default will be started");
            Context.GetActor<GameModesController>().StartMode(SRScenes.ClassicGameScene.name);
        }

        //================================ Private|Protected methods ================================
    }
}